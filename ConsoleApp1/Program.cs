using OpenAI_API;

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
}