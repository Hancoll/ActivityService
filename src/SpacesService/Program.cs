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

var spaces = new List<Guid>();

app.MapGet("/GetRandomSpaceId", () =>
{
    var spaceId = Guid.NewGuid();
    spaces.Add(spaceId);
    return spaceId;
}).RequireAuthorization();

app.MapGet("/IsSpaceExists", (Guid spaceId) => spaces.Contains(spaceId)).RequireAuthorization();

app.Run();