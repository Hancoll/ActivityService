using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

const string key = "very-strong-secret";

builder.Services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    });
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

var images = new List<Guid>();

app.MapGet("/GetRandomImageId", () =>
{
    var imageId = Guid.NewGuid();
    images.Add(imageId);
    return imageId;
}).RequireAuthorization();

app.MapGet("/IsImageExists", (Guid imageId) => images.Contains(imageId)).RequireAuthorization();

app.Run();
