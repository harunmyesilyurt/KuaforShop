using KuaforShop.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaforShop.Application.Utils
{
    public class AIHelper
    {
        public async Task<AIResponse> CreateHair(string path) 
        {
            string apiUrl = "http://node-js-faceshape.onrender.com/api/ai";
            string imagePath = path;

            using (HttpClient client = new HttpClient())
            {
                using (var formData = new MultipartFormDataContent())
                {
                    byte[] imageData = await File.ReadAllBytesAsync(imagePath);
                    var imageContent = new ByteArrayContent(imageData);
                    imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

                    formData.Add(imageContent, "image", Path.GetFileName(imagePath));

                    HttpResponseMessage response = await client.PostAsync(apiUrl, formData);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Response from API:");
                        Console.WriteLine(result);
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(errorResponse);
                    }
                }
            }

            return new AIResponse();
        }
    }
}
