using API.Authenticators;
using API.Extensions;
using API.Filters;
using DAL.DataContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HumanActivitiesDataContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("HumanActivitiesDB")));

builder.Services.AddSingleton<Authenticator>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(jwtBearerOptions =>
    {
        jwtBearerOptions.RequireHttpsMetadata = true;
        jwtBearerOptions.SaveToken = true;
        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
            ValidAudience = builder.Configuration["JWTSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWTSettings:AccessSecretKey"])),
            ClockSkew = TimeSpan.Zero
        };
    });
    //.AddGoogle(googleOptions =>
    //{
    //    googleOptions.ClientId = "1033380628767-gvmisaqq2gugi5seg21grb26id5kb894.apps.googleusercontent.com";//builder.Configuration["Authentication:Google:ClientId"]; // to mozna przeniesc do configa, ale poki co damy statycznie
    //    googleOptions.ClientSecret = "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"; //builder.Configuration["Authentication:Google:ClientSecret"]; // to mozna przeniesc do configa, ale poki co damy statycznie
    //});

builder.Services.AddDomainServices();

builder.Services
    .AddControllers(options => options.Filters.Add<ExceptionFilter>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
