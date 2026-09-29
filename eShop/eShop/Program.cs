using eShop.Components;
using eShop.DataStore.SQL.Dapper;
using eShop.UseCases.AdminPortal.OrderDetailScreen;
using eShop.UseCases.AdminPortal.OutstandingOrdersScreen;
using eShop.UseCases.OrderConfirmationScreen;
using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;
using eShop.UseCases.SearchProductScreen;
using eShop.UseCases.ShoppingCartScreen;
using eShop.UseCases.ViewProductScreen;
using eShop.UseCases.ViewProductScreen.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Authentication & Authorization Configuration
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "eShop.AuthCookie";
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.AccessDeniedPath = "/login";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                });
            builder.Services.AddAuthorization();
            builder.Services.AddCascadingAuthenticationState();

            // Cấu hình Database Connection & SQL Data Access (Dapper)
            var connectionString = builder.Configuration.GetConnectionString("eShopConnection") 
                ?? builder.Configuration.GetConnectionString("Default") 
                ?? "Server=localhost;Database=eShop;Trusted_Connection=True;TrustServerCertificate=True;";
            builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>(_ => new SqlDataAccess(connectionString));

            // DataStore Plugin Registration (SQL Dapper)
            builder.Services.AddTransient<IProductRepository, eShop.DataStore.SQL.Dapper.ProductRepository>();
            builder.Services.AddTransient<IOrderRepository, eShop.DataStore.SQL.Dapper.OrderRepository>();

            // UI State & Shopping Cart
            builder.Services.AddScoped<IShoppingCart, eShop.DataStore.HardCode.ShoppingCart>();
            builder.Services.AddScoped<IShoppingCartStateStore, eShop.DataStore.HardCode.ShoppingCartStateStore>();
            
            // Customer Portal Use Cases
            builder.Services.AddTransient<ISearchProductUseCase, SearchProductUseCase>();
            builder.Services.AddTransient<IViewProductUseCase, ViewProductUseCase>();
            builder.Services.AddTransient<IAddProductToCartUseCase, AddProductToCartUseCase>();
            builder.Services.AddTransient<IViewShoppingCartUseCase, ViewShoppingCartUseCase>();
            builder.Services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();
            builder.Services.AddTransient<IUpdateQuantityUseCase, UpdateQuantityUseCase>();
            builder.Services.AddTransient<IPlaceOrderUseCase, PlaceOrderUseCase>();
            builder.Services.AddTransient<IViewOrderConfirmationUseCase, ViewOrderConfirmationUseCase>();

            // Admin Portal Use Cases
            builder.Services.AddTransient<IViewOutstandingOrdersUseCase, ViewOutstandingOrdersUseCase>();
            builder.Services.AddTransient<IViewOrderDetailUseCase, ViewOrderDetailUseCase>();
            builder.Services.AddTransient<IProcessOrderUseCase, ProcessOrderUseCase>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.UseAuthentication();
            app.UseAuthorization();

            // Authentication Endpoints
            app.MapGet("/logout", async (HttpContext context) =>
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Results.Redirect("/login");
            });

            app.MapPost("/authenticate", async (HttpContext context, [FromForm] string username, [FromForm] string password) =>
            {
                if ((username == "admin" && password == "admin") || (username == "admin" && password == "admin123"))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, "Admin")
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return Results.Redirect("/outstandingorders");
                }
                return Results.Redirect("/login?error=invalid");
            });

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
