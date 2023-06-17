using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RR.QrManage.DataAccess;
using RR.QrManage.DataAccess.BD_QRMANAGE.V1;
using RR.QrManage.Log;
using RR.QrManage.WebApi;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

//Logs del servicio.
string PathServiceLog = configuration.GetValue<string>("Logs:PathServiceLog");
string LogLevel = configuration.GetValue<string>("Logs:Level");
int LogLimit = configuration.GetValue<int>("Logs:Limit").Equals(0) ? 30 : configuration.GetValue<int>("Logs:Limit");
//string PathTableLog = configuration.GetValue<string>("Logs:PathTableLog");
_ = new Logger(PathServiceLog, LogLimit, LogLevel);



//Parámetros para la conexión a la Base de Datos dbGatewayPagoServicio
string dataBase = configuration.GetValue<string>("ConnectionStrings_BD_QRMANAGE:Database");
string user = configuration.GetValue<string>("ConnectionStrings_BD_QRMANAGE:User");
string password = configuration.GetValue<string>("ConnectionStrings_BD_QRMANAGE:Password");
string server = configuration.GetValue<string>("ConnectionStrings_BD_QRMANAGE:Server");
int timeOut = configuration.GetValue<int>("ConnectionStrings_BD_QRMANAGE:Timeout");

//Genera cadena de conexión
string connection = Connection.DomainUser(server, dataBase);
//string connection = Connection.SqlUser(server, dataBase, user, password);

//Lamado a las clases DataAccess
builder.Services.AddSingleton<IUser, User>(f => new User(connection, timeOut));
builder.Services.AddSingleton<IItem, Item>(f => new Item(connection, timeOut));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning();
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, RR.QrManage.WebApi.SwaggerGenConfiguration>();



builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<AddHeaderParameter>();
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme{Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme,Id = "basic"}},
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    foreach (var description in app.Services.GetService<IApiVersionDescriptionProvider>()!.ApiVersionDescriptions)
    {
        c.SwaggerEndpoint($"../swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
    }
});
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
