//using Context;
using Microsoft.EntityFrameworkCore;
using WebApp.Hubs;
using Microsoft.AspNetCore.Identity;
using Entities;
using Context;
using Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDataContext>(options =>
                options.UseSqlServer(
                    "name=ConnectionStrings:DataConnection",
                    optionsBuilder => optionsBuilder.MigrationsAssembly("WebApp")));


builder.Services.AddTransient<DbContext, AppDataContext>();

builder.Services.AddTransient<IMessageRepository, MessageRepository>();
builder.Services.AddTransient<IChatRepository, ChatRepository>();



builder.Services.AddDefaultIdentity<AppUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

})
    .AddRoles<AppRole>()
    .AddEntityFrameworkStores<AppDataContext>();



builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();
/*
builder.Services.AddDbContext<AppDataContext>(options =>
                options.UseSqlServer(
                    "name=ConnectionStrings:DataConnection",
                    optionsBuilder => optionsBuilder.MigrationsAssembly("WebApp")));
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    AppDbInitializer.SeedUsers(userManager);
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();



app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chatHub");

    endpoints.MapRazorPages();
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});



app.Run();
