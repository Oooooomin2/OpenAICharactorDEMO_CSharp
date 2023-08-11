using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp1
{
    internal class Program
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        static async Task Main(string[] args)
        {
            const string apiKey = "APIKEY";
            const string url = "https://api.openai.com/v1/chat/completions";
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            var content = InitializeContent();

            while (true)
            {
                Console.Write("おれ: ");
                var myContent = Console.ReadLine();
                if (string.IsNullOrEmpty(myContent))
                {
                    break;
                }

                content.Add(new Messages() { Role = "user", Content = myContent });
                var response = await GetResponseFromAI(url, content);
                Console.WriteLine($"AIヤンキー: {response.Choices[0].Message.Content}");
            }
        }

        private static List<Messages> InitializeContent()
        {
            return new List<Messages>
            {
                new Messages() { Role = "system", Content = "あなたはヤンキーであり、不良です。私が言うことに対して全部喧嘩をするような感じで言い返してください。敬語は使わないでください。" }
            };
        }

        private static async Task<Response> GetResponseFromAI(string url, List<Messages> content)
        {
            var requestBody = new RequestBody
            {
                Model = "gpt-3.5-turbo",
                Messages = content
            };
            var json = JsonSerializer.Serialize(requestBody);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await (await _httpClient.PostAsync(url, stringContent)).Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Response>(result);
        }
    }

    class RequestBody
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }
        [JsonPropertyName("messages")]
        public List<Messages> Messages { get; set; }
    }

    class Messages
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }

    class Response
    {
        [JsonPropertyName("choices")]
        public Choices[] Choices { get; set; }
    }

    class Choices
    {
        [JsonPropertyName("message")]
        public Message Message { get; set; }
    }

    class Message
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}