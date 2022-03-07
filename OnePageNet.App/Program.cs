using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnePageNet.Data.Data;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Helpers.AutoMapper;
using OnePageNet.Services.Services;
using OnePageNet.Services.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OnePageNetDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddEntityFrameworkStores<OnePageNetDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, OnePageNetDbContext>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSession();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new
            SymmetricSecurityKey
            (Encoding.UTF8.GetBytes
                (builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddCors(c =>
    {
        c.AddPolicy("AllowOrigin",
            options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        c.AddPolicy("FrontendOrigin",
            opt =>
                opt.WithOrigins(" https://localhost:44476/")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
    }
);

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<DatabaseService<PostEntity, PostDto>, PostEntityDatabaseService>();
builder.Services.AddScoped<DatabaseService<CommentEntity, CommentDto>, CommentEntityDatabaseService>();
builder.Services.AddScoped<DatabaseService<MessageEntity, MessageDto>, MessageEntityDatabaseService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRelationsService, UserRelationsService>();
builder.Services.AddScoped(typeof(IDatabaseService<,>), typeof(DatabaseService<,>));

builder.Services.AddMvc();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseSession();
app.Use(async (context, next) =>
{
    var token = context.Session.GetString("Token");
    if (!string.IsNullOrEmpty(token))
    {
        context.Request.Headers.Add("Authorization", "Bearer " + token);
    }

    await next();
});

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(corsPolicyBuilder => corsPolicyBuilder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{Id?}");

app.MapFallbackToFile("index.html");

app.Run();