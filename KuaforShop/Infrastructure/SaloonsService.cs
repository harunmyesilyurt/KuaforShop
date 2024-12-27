using KuaforShop.Application.Models;
using System.Net.Http;
using System.Text.Json;

namespace KuaforShop.Web.Infrastructure
{
    public class SaloonsService
    {
        private readonly HttpClient _httpClient;
        private readonly string serviceAddress = "https://localhost:7297";

        public SaloonsService()
        {
            _httpClient = new HttpClient();
        }

        public List<SaloonResponseModel> GetSaloons()
        {
            try
            {
                // GET isteğini senkron hale getiriyoruz
                var responseTask = _httpClient.GetAsync($"{serviceAddress}/getall");
                responseTask.Wait();
                var response = responseTask.Result;

                if (response.IsSuccessStatusCode)
                {
                    // Yanıtın içeriğini okuma işlemi senkron hale getiriliyor
                    var contentTask = response.Content.ReadAsStringAsync();
                    contentTask.Wait();
                    var jsonString = contentTask.Result;

                    var result = JsonSerializer.Deserialize<List<SaloonResponseModel>>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return result;
                }
                else
                {
                    Console.WriteLine($"Hata: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
            }

            return new();
        }

        public SaloonResponseModel GetSaloon(Guid id)
        {
            try
            {
                // GET isteğini senkron hale getiriyoruz
                var responseTask = _httpClient.GetAsync($"{serviceAddress}/get/{id}");
                responseTask.Wait();
                var response = responseTask.Result;

                if (response.IsSuccessStatusCode)
                {
                    // Yanıtın içeriğini okuma işlemi senkron hale getiriliyor
                    var contentTask = response.Content.ReadAsStringAsync();
                    contentTask.Wait();
                    var jsonString = contentTask.Result;

                    var result = JsonSerializer.Deserialize<SaloonResponseModel>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return result;
                }
                else
                {
                    Console.WriteLine($"Hata: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
            }

            return new SaloonResponseModel();
        }
    }
}

