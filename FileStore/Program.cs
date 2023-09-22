using FileStore;
using FileStore.Models;
using FileStore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Context>(option =>
{
    //option.UseSqlServer("Data Source=DESKTOP-4AVR96K;Initial Catalog=FileStore;Integrated Security=True;TrustServerCertificate=True");
    option.UseSqlServer("Data Source=MSI;Initial Catalog=FileStore;Integrated Security=True;TrustServerCertificate=True");
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description="ok",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddTransient<IManageImage,ManageImage>();
builder.Services.AddScoped<DbContext, Context>();
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

//regist repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IManageImage), typeof(ManageImage));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(IQuoteRepository), typeof(QuoteRepository));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

//var _authkey = builder.Configuration.GetValue<string>("AppSettings:SecretKey");
//builder.Services.AddAuthentication(item =>
//{
//    item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(item =>
//{
//    item.RequireHttpsMetadata = true;
//    item.SaveToken = true;
//    item.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authkey)),
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ClockSkew = TimeSpan.Zero
//    };
//});

//var _jwtsetting = builder.Configuration.GetSection("AppSettings");
//builder.Services.Configure<AppSetting>(_jwtsetting);

builder.Services.AddAuthorization();
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSettings"));

var secretKey = builder.Configuration["AppSettings:SecretKey"];
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            //tự cấp token
            ValidateIssuer = false,
            ValidateAudience = false,

            //ký vào token
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

            ClockSkew = TimeSpan.Zero
        };
    });


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};
webSocketOptions.AllowedOrigins.Add("*");
//app.UseMiddleware<WebSocketMiddleware>();
app.UseWebSockets(webSocketOptions);
app.Run();
