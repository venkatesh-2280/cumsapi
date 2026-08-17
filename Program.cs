using Serilog;
using STAapi;
using STAapi.Controllers;
using STAapi.Middleware;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

// 🔹 Decrypt JWT values ONCE
var issuer = AuthController.Decrypt(builder.Configuration["Jwt:Issuer"]);
var audience = AuthController.Decrypt(builder.Configuration["Jwt:Audience"]);

if (issuer != "Gnsa~Flexicode")
    throw new Exception("Invalid JWT Issuer after decryption");
if (audience != "GnsaFlexiSTA-app")
    throw new Exception("Invalid JWT Audience after decryption");

// ?? Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error()
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/error-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30)
    .CreateLogger();

// ?? Replace default logger
builder.Host.UseSerilog();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// 🔹 CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCors", policy =>
    {
        policy.WithOrigins("https://localhost:5025", "http://localhost:5025")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              .WithExposedHeaders("X-New-Token");
    });
});



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//app.UseHttpsRedirection();


app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseRouting();
app.UseCors("DefaultCors");
app.UseMiddleware<JwtAuthorizationMiddleware>();
//app.UseCors(x => x
//				.AllowAnyOrigin()
//				.AllowAnyMethod()
//				.AllowAnyHeader());

app.UseAuthorization(); 
app.MapControllers();

app.Run();

