using Npgsql;
using System;
using System.IO;
using Telegram.Bot;
using System.Windows;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using System.Collections.ObjectModel;

namespace chatBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static ObservableCollection<User> Users;
        public static TelegramBotClient bot;
        string token = File.ReadAllText("token.txt");

        private static NpgsqlConnection conn;
        static string connstring = String.Format("Host=localhost;Port=5432;Database=bot;Username=postgres;Password=1111");
        private static NpgsqlCommand cmd;

        public static string calories = "Unlimitted";
        public static string dish = "Vegetable dish";
        public static string height;
        public static string weight;
        public static string age;
        public static string sex;
        public static int result;
        public static Random random = new Random();



        public static async Task res(Telegram.Bot.Args.MessageEventArgs e)
        {
            int hei = 1;
            if (height == "1-130 cm") hei = 115;
            if (height == "131-145 cm") hei = 138;
            if (height == "146-160 cm") hei = 153;
            if (height == "161-175 cm") hei = 168;
            if (height == "176-190 cm") hei = 183;
            if (height == "191-220+ cm") hei = 200;
            int wei = 1;
            if (weight == "5-15 kg") wei = 10;
            if (weight == "16-30 kg") wei = 23;
            if (weight == "31-45 kg") wei = 38;
            if (weight == "46-60 kg") wei = 53;
            if (weight == "61-75 kg") wei = 68;
            if (weight == "76-90 kg") wei = 83;
            if (weight == "91-105 kg") wei = 98;
            if (weight == "106-120 kg") wei = 113;
            if (weight == "121-135+ kg") wei = 130;
            int yea = 1;
            if (age == "4-10 years") yea = 8;
            if (age == "11-16 years") yea = 13;
            if (age == "17-22 years") yea = 20;
            if (age == "23-35 years") yea = 29;
            if (age == "36-45 years") yea = 40;
            if (age == "46-55 years") yea = 50;
            if (age == "56-65 years") yea = 60;
            if (age == "66-75 years") yea = 70;
            if (age == "76-85+ years") yea = 80;

            if (sex == "male")
            {
                result = Convert.ToInt32(3.1 * (hei) + 9.2 * wei + 447.6 - 4.3 * yea);
            }
            else if (sex == "female")
            {
                result = Convert.ToInt32(4.8 * (hei) + 13.4 * wei + 88.36 - 5.7 * yea);
            }
            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Okay! Your norma of calories is near  *{result - 100}-{result + 100}*\n", ParseMode.Markdown);
        }


        public void SendMessage()
        {
            var concreteUser = Users[Users.IndexOf(usersList.SelectedItem as User)];
            string response = $"Support:{textBoxSendMessage.Text}";
            concreteUser.Message.Add(response);

            bot.SendTextMessageAsync(concreteUser.Id, textBoxSendMessage.Text);
            string logText = $"{ DateTime.Now}: >>{concreteUser.Id} {concreteUser.Nickname} {response} \n";
            File.AppendAllText("data.log", logText);

            textBoxSendMessage.Text = String.Empty;
        }
    }
}