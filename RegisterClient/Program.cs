using System;
using static System.Console;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Net;

namespace RegisterClient
{
    class Program
    {
        private static string InputLogin { get; set; }
        private static string InputPassword { get; set; }
        static async Task Main(string[] args)
        {
            while (true)
            {
                WriteLine("1. Создать аккаунт");
                WriteLine("2. Войти");
                var userChoice = ReadLine();
                switch (userChoice)
                {
                    case "1":
                        InputUserData();
                        await SignUp();
                        break;
                    case "2":
                        InputUserData();
                        await Auth();
                        break;
                    default:
                        WriteLine("Выбран неправильный пункт");
                        break;
                }
            }
        }
        static void InputUserData()
        {
            WriteLine("Логин:");
            var InputLogin = ReadLine();
            WriteLine("Пароль:");
            var InputPassword = ReadLine();
            if (string.IsNullOrWhiteSpace(InputLogin) || string.IsNullOrWhiteSpace(InputPassword))
            {
                WriteLine("Введите данные!");
            }
        }
        static async Task Auth()
        {

        }
        static async Task SignUp()
        {

            var user = new User()
            {
                Login = InputLogin,
                Password = InputPassword
            };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "http://localhost/user/signup";
            using var client = new HttpClient();
            var response = await client.PostAsync(url, data);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    WriteLine("Данные о пользователе");
                    var userJson = await response.Content.ReadAsStringAsync();
                    var userData = JsonConvert.DeserializeObject<User>(userJson);
                    WriteLine($"Логин: {userData.Login}");
                    WriteLine($"Дата создания: {user.CreatedDate}");
                    break;
                case HttpStatusCode.Created:
                    WriteLine("Аккаунт создан");
                    break;
                case HttpStatusCode.Forbidden:
                    WriteLine("Пользователь существует. Войдите или введите другой логин.");
                    break;
                case HttpStatusCode.NotFound:
                    WriteLine("Пользователь не найден");
                    break;
            }
        }


    }
}
