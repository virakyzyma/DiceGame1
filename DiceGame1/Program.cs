using System.Net.Http.Json;
using System.Text.Json;

namespace DiceGame1
{
    internal class Program
    {
        static HttpClient client = new HttpClient();
        static string apiKey = "693551f6-af68-4633-8bcd-4eb7a960ae20";
        static async Task Main(string[] args)
        {
            PlayerVsComputer();
        }
        public static void PlayerVsPlayer()
        {
            Console.WriteLine("Press \"Enter\" to throw dice");
            Console.ReadLine();
            int res1 = RollDice().Result;
            ShowDice(res1);
            Console.WriteLine("Press \"Enter\" to throw dice");
            Console.ReadLine();
            int res2 = RollDice().Result;

            ShowDice(res2);
            if (res1 > res2)
            {
                Console.WriteLine("Player1 Wins!");
            }
            else if (res2 > res1)
            {
                Console.WriteLine("Player2 Wins!");

            }
            else
            {
                Console.WriteLine("Draw");
            }
        }

        public static void PlayerVsComputer()
        {
            Console.WriteLine("Press \"Enter\" to throw dice");
            Console.ReadLine();
            int res1 = RollDice().Result;
            ShowDice(res1);
            Thread.Sleep(1000);
            Console.WriteLine("Computers result: ");
            int res2 = RollDice().Result;

            ShowDice(res2);
            if (res1 > res2)
            {
                Console.WriteLine("Player wins");
            }
            else if (res2 > res1)
            {
                Console.WriteLine("Computer wins");

            }
            else
            {
                Console.WriteLine("Draw");
            }

        }

        public static void ShowDice(int number)
        {
            switch (number)
            {
                case 1:
                    Console.WriteLine("---------");
                    Console.WriteLine("-       -");
                    Console.WriteLine("-   *   -");
                    Console.WriteLine("-       -");
                    Console.WriteLine("---------");

                    break;
                case 2:
                    Console.WriteLine("---------");
                    Console.WriteLine("-     * -");
                    Console.WriteLine("-   *   -");
                    Console.WriteLine("-       -");
                    Console.WriteLine("---------");
                    break;
                case 3:
                    Console.WriteLine("---------");
                    Console.WriteLine("-     * -");
                    Console.WriteLine("-   *   -");
                    Console.WriteLine("- *     -");
                    Console.WriteLine("---------");
                    break;
                case 4:
                    Console.WriteLine("---------");
                    Console.WriteLine("-  * *  -");
                    Console.WriteLine("-  * *  -");
                    Console.WriteLine("-       -");
                    Console.WriteLine("---------");
                    break;
                case 5:
                    Console.WriteLine("---------");
                    Console.WriteLine("- *   * -");
                    Console.WriteLine("-   *   -");
                    Console.WriteLine("- *   * -");
                    Console.WriteLine("---------");
                    break;
                case 6:
                    Console.WriteLine("---------");
                    Console.WriteLine("-  * *  -");
                    Console.WriteLine("-  * *  -");
                    Console.WriteLine("-  * *  -");
                    Console.WriteLine("---------");
                    break;
            }
        }
        static async Task<int> RollDice()
        {
            string url = $"https://api.random.org/json-rpc/4/invoke";
            var requestBody = new
            {
                jsonrpc = "2.0",
                method = "generateIntegers",
                @params = new
                {
                    apiKey = apiKey,
                    n = 1,
                    min = 1,
                    max = 6
                },
                id = 42
            };

            var response = await client.PostAsJsonAsync(url, requestBody);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonDocument.Parse(responseString);
            int diceRoll = result.RootElement.GetProperty("result").GetProperty("random").GetProperty("data")[0].GetInt32();

            return diceRoll;
        }
    }
}
