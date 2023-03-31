using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SC.Internship.Common.ScResult;

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

app.MapGet("/", () =>
{
    var imageId = Guid.NewGuid();
    images.Add(imageId);
    return new ScResult<Guid>(imageId);
}).RequireAuthorization();

app.MapGet("/existence", (Guid imageId) =>
{
    var result = images.Contains(imageId);
    return new ScResult<bool>(result);
}).RequireAuthorization();

app.Run();
