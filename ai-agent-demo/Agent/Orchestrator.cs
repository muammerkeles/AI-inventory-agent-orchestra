using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace ai_agent_demo.Agent
{
    public class InventoryOrchestrator
    {
        private readonly HttpClient _strapiClient;

        private readonly Kernel _kernel;

        public InventoryOrchestrator(Kernel kernel) => _kernel = kernel;

        public async Task<string> RunCampaignProcessAsync()
        {
            try
            {


                string goal = @"Stokları kontrol et, düşükse kampanya oluştur ve Strapi'ye kaydet.";
                // Modelin fonksiyonları otomatik çağırması için ayar:
                PromptExecutionSettings settings = new()
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                };
                var result = await _kernel.InvokePromptAsync(goal, new KernelArguments(settings));
                return result.ToString();
            }
            catch (Exception ex)
            {

                // 1. Hatanın en derinindeki asıl mesajı alalım
                var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                // 2. Eğer hata bir HTTP isteğinden geliyorsa (ki 400 budur), detayını loglayalım
                Console.WriteLine($"[SK Error]: {message}");

                // 3. Daha net bir hata fırlatmak
                throw new Exception($"Yapay zeka ajanı çalışırken bir hata oluştu: {message}", ex);
            }
        }

        /// <summary>
        ///  farklı goal ve adımlarla kampanya sürecini başlatan bir method. Modelin otomatik fonksiyon çağırma yeteneği sayesinde, bu method sadece hedefi tanımlar ve model gerekli adımları atar.
        /// </summary>
        /// <returns></returns>
        public async Task RunAsync()
        {
            string goal = @"
            1. Stok seviyesi 10'un altında olan ürünleri getir.
            2. Bu ürünler için 'Stokta Son [Sayı] Adet!' temalı kısa bir kampanya metni hazırla.
            3. Hazırladığın bu kampanyayı Strapi'ye taslak olarak kaydet.";

            // Ajan burada devreye giriyor ve planı işletiyor
            var result = await _kernel.InvokePromptAsync(goal);
            Console.WriteLine("İşlem Tamamlandı: " + result);
        }
    }
    public class StrapiPlugin
    {
        private readonly HttpClient _client;
        public StrapiPlugin(HttpClient client) => _client = client;

        [KernelFunction, Description("Stok verilerini Strapi'den çeker")]
        public async Task<string> GetProducts()
        {
            // Strapi'den ürünleri çekme mantığı
            var response = await _client.GetFromJsonAsync<dynamic>("products");
            return response?.ToString() ?? "Ürün bulunamadı";
        }

        [KernelFunction, Description("Yeni bir kampanya kaydı oluşturur")]
        public async Task SaveToStrapi([Description("Oluşturulan kampanya başlığı veya içeriği")] string title)
        {
            /// Ben koymadım ama buraya bir exception handler koymakta fayda var, çünkü dış bir servise istek atıyoruz ve bu işlem başarısız olabilir. Ayrıca, Strapi'nin beklediği veri formatına göre body'yi düzenlemek gerekebilir.
            await _client.PostAsJsonAsync("campaigns", new { data = new { Title = title, Statuss = "draft" } });
        }
    }
}
