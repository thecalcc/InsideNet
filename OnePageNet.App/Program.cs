using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.AutoMapper;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Services;
using OnePageNet.App.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<OnePageNetDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(o =>
    {
        o.Password.RequireDigit = false;
        o.Password.RequireLowercase = false;
        o.Password.RequireUppercase = false;
        o.Password.RequiredUniqueChars = 0;
        o.Password.RequireNonAlphanumeric = false;
        o.Password.RequiredLength = 3;
    })
    .AddEntityFrameworkStores<OnePageNetDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, OnePageNetDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddCors(c =>
    c.AddPolicy("AllowOrigin",
        options => options
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNameCaseInsensitive = true);

builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IDatabaseService<PostEntity>, PostEntityDatabaseService>();
builder.Services.AddScoped<IDatabaseService<CommentEntity>, CommentEntityDatabaseService>();
builder.Services.AddScoped<IDatabaseService<MessageEntity>, MessageEntityDatabaseService>();
builder.Services.AddScoped(typeof(IDatabaseService<>), typeof(DatabaseService<>));

var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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