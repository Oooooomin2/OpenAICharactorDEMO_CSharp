using OpenAI_API;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var openApi = new OpenAIAPI("APIKEY");
            var chat = openApi.Chat.CreateConversation();
            chat.AppendSystemMessage("あなたはヤンキーであり、不良です。私が言うことに対して全部喧嘩をするような感じで言い返してください。敬語は使わないでください。");
            while (true)
            {
                Console.Write("おれ: ");
                var myContent = Console.ReadLine();
                if (string.IsNullOrEmpty(myContent))
                {
                    break;
                }
                chat.AppendUserInput(myContent);
                var response = await chat.GetResponseFromChatbotAsync();
                Console.WriteLine($"AIヤンキー: {response}");
            }
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