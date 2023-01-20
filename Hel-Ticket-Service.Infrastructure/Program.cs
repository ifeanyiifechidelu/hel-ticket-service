var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile(Path.GetFullPath(@"../appsettings.json"), optional: false, reloadOnChange: true);
var app = builder.Build();
app.Run();
