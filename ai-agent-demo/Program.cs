
using ai_agent_demo.Agent;
using Microsoft.SemanticKernel;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
// Add services to the container.
builder.Services.AddRazorPages();





// 1. HttpClient Kaydı (Strapi için)
builder.Services.AddHttpClient("StrapiClient", client => {
    client.BaseAddress = new Uri("http://localhost:1337/api/");
});

// 2. Semantic Kernel Kaydı
builder.Services.AddTransient<Kernel>(sp => {
    var builder = Kernel.CreateBuilder();

    // Model ayarları (OpenAI veya Ollama da kullanılabilir. Kısıtlı ama ücretsiz olduğu için Geminiyi aktif ettim)
    //builder.AddOpenAIChatCompletion("gpt-4", "API_KEY_BURAYA"); // veya apsettings.json'den çekebilirsin. Prod için en doğru yöntem secrets manager kullanmak. Server Env variable'ları da olabilir.
    builder.AddGoogleAIGeminiChatCompletion("gemini-3.1-flash-lite-preview", "API_KEY_BURAYA"); // veya apsettings.json'den çekebilirsin. Prod için en doğru yöntem secrets manager kullanmak. Server Env variable'ları da olabilir.
    var kernel = builder.Build();

    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var strapiClient = httpClientFactory.CreateClient("StrapiClient");

    kernel.ImportPluginFromObject(new StrapiPlugin(strapiClient), "StrapiTool");

    //builder.Plugins.AddFromObject(new StrapiPlugin(strapiClient), "StrapiTool");
    //builder.Services.AddTransient<Kernel>(sp => builder.Build());

    return kernel;
});

// Orkestratör Servisini Kaydettik
builder.Services.AddScoped<InventoryOrchestrator>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.MapControllers();

app.Run();
