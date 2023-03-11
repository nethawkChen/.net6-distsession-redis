/*
 *  使用 Redis 達到分散式 Session
 */
using DistSession.Lib;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Login Cookie
double LoginTimeout = builder.Configuration.GetValue<double>("LoginExpireMinute");

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.AccessDeniedPath = new PathString("/Home/AccessDeny");  //拒絕﹐不允許登入﹐會跳到這一頁
        options.LoginPath = new PathString("/Login/Login");   //登入頁
        options.LogoutPath = new PathString("/Home/Index");   //登出後轉到那一頁
        options.ExpireTimeSpan = TimeSpan.FromMinutes(LoginTimeout);  //如果沒有這一項設定﹐那麼預設為14天(cookie過期的時間)
    });
#endregion

#region 將數據保護的秘鑰存儲為分布式
//安裝 Microsoft.AspNetCore.DataProtection.StackExchangeRedis
builder.Services.AddDataProtection()
    .PersistKeysToStackExchangeRedis(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")), ".dist.Session");
#endregion

#region 加入 redis 作分散式
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");  //redis 連線
    options.InstanceName = ".dist.Session";
});
#endregion

#region use session
builder.Services.AddSession(options => {
    options.Cookie.Name = ".dist.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(300);     // 設定 Session 過期的時間, 300 sec
});
#endregion

#region DI
builder.Services.AddScoped<Utils>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  //驗證
app.UseAuthorization();   //授權

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
