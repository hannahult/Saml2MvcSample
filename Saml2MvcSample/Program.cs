using Sustainsys.Saml2;
using Sustainsys.Saml2.AspNetCore2;
using Sustainsys.Saml2.Metadata;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = "Cookies";
    opt.DefaultChallengeScheme = Saml2Defaults.Scheme;
})
.AddCookie()
.AddSaml2(opt =>
{
    opt.SPOptions.EntityId = new EntityId("https://localhost:5080/Saml2");
    opt.IdentityProviders.Add(
        new IdentityProvider(
            new EntityId("https://stubidp.sustainsys.com/Metadata"),
            opt.SPOptions)
        {   
            LoadMetadata = true
        });
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
