
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Hel_Ticket_Service.Domain;
using Hel_Ticket_Service.Infrastructure;
using Serilog;
using Serilog.Events;
using System.Reflection;
using serviceProvider = Hel_Ticket_Service.Infrastructure.ServiceProvider;

var builder = WebApplication.CreateBuilder(args);




//Suppress automatic model validation
builder.Services.Configure<ApiBehaviorOptions>(options => {options.SuppressModelStateInvalidFilter = true;});
//Read in the config env file into a static model

if (serviceProvider.ReadConfiguration(@"../hel-ticket-service.env") == false) return;
//Add environment variables to the global config
builder.Configuration.AddEnvironmentVariables();
//Map configuration to global class
serviceProvider.MapConfiguration(builder.Configuration);

var applicationVersion =  Assembly.GetEntryAssembly()?
.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;


builder.Host.UseSerilog((ctx, lc) => lc
                .WriteTo.Console()
                .WriteTo.File(@"../logs/hel-ticket-service.log",
                restrictedToMinimumLevel: LogEventLevel.Verbose,
                outputTemplate: ConstProvider.SerilogOutPutTemplate)
                .Enrich.FromLogContext()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                // Filter out ASP.NET Core infrastructre logs that are Information and below
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information) 
                .Enrich.WithCorrelationIdHeader("X-Correlation-Id")
                .Enrich.WithClientAgent()
                .Enrich.WithClientIp()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentName()
                .Enrich.WithProperty("Version",applicationVersion??"")
                .Enrich.WithProperty("ApplicationName",Service.Name)
                .WriteTo.Seq(Service.Url));

builder.WebHost.UseUrls(Service.Url);

builder.Services.AddSingleton<Hel_Ticket_Service.Infrastructure.ServiceProvider>();
builder.Services.AddScoped<ICacheProvider,CacheProvider>();
builder.Services.AddTransient<ITicketRepository,TicketRepository>();
builder.Services.AddTransient<ITicketService, TicketService>();
builder.Services.AddTransient<IEBProvider, EBProvider>();
//builder.Services.AddSingleton<IADProvider,ADProvider>();
builder.Services.AddSingleton<IDBProvider,DBProvider>();
//builder.Services.AddSingleton<ADConnection>();
builder.Services.AddSingleton<DBConnection>();
builder.Services.AddSingleton<EBProvider>();
builder.Services.AddSingleton<EBConnection>();


//builder.Services.AddValidatorsFromAssemblyContaining<AuthSignInValidator>();
builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = Service.EventBusUrl; options.InstanceName = "redis-server";});
builder.Services.AddSession(o =>
{
    o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    o.Cookie.Name = "BNT.Session";
    o.Cookie.HttpOnly = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHeaderPropagation(options => options.Headers.Add("X-Correlation-Id"));

builder.Services.AddHttpClient("Hel_Ticket_Service", c =>
{
    c.BaseAddress = new Uri(Service.Url);
}).AddHeaderPropagation();

var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseMiddleware<AppExceptionHandlerMiddleware>();
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
});
app.UseHeaderPropagation();
app.UseSession();
app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();