using Himchistka.Api;
using Himchistka.Api.JWT;
using Himchistka.Api.Mapper;
using Himchistka.Data;
using Himchistka.Data.Entities;
using Himchistka.Data.Identity;
using Himchistka.Services.Interfaces;
using Himchistka.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseNpgsql(connectionString, b => b.MigrationsAssembly("Himchistka.Api")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;
});



builder.Services.AddCors();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // укзывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = AuthOptions.ISSUER,
            // будет ли валидироваться потребитель токена
            ValidateAudience = false,
            // установка потребителя токена
            //ValidAudience = AuthTokenOptions.AUDIENCE,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true
        };
    });

// Add services to the container.
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IPlaceService, PlaceService>();
builder.Services.AddScoped<IServicesService, ServicesService>();
builder.Services.AddScoped<ISpendingService, SpendingService>();
builder.Services.AddScoped<DbContext, ApplicationDbContext>();


builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
    var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();


    DataSeeder.SeedRoles(roleManager).Wait();
    DataSeeder.SeedSadmin(userManager, context).Wait();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
