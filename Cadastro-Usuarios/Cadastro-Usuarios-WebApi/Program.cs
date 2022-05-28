using Cadastro_Usuarios_WebApi;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Environment, builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment, builder.Services);

app.Run();
