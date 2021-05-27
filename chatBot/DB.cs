using Npgsql;
using System.Windows;
using System.Threading.Tasks;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace chatBot
{
    public partial class MainWindow : Window
    {
        public static async Task recommended_dish(Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;
            conn = new NpgsqlConnection(connstring);
            try
            {
                conn.Open();
                {
                    if (dish == "Meat" && calories == "1-150")
                    {
                        cmd = new NpgsqlCommand($@"select * from meat_dish where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Meat" && calories == "151-250")
                    {
                        cmd = new NpgsqlCommand($@"select * from meat_dish where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Meat" && calories == "251-350")
                    {
                        cmd = new NpgsqlCommand($@"select * from meat_dish where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Meat" && calories == "Unlimited")
                    {
                        cmd = new NpgsqlCommand($@"select * from meat_dish where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Meat" && calories == "Don't worry about it")
                    {
                        cmd = new NpgsqlCommand($@"select * from meat_dish where id=(SELECT floor(random() * (select count (*) from meat_dish) + 1)::int);", conn);
                    }
                }
                {
                    if (dish == "Fish" && calories == "1-150")
                    {
                        cmd = new NpgsqlCommand($@"select * from fish_dish where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Fish" && calories == "151-250")
                    {
                        cmd = new NpgsqlCommand($@"select * from fish_dish where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Fish" && calories == "251-350")
                    {
                        cmd = new NpgsqlCommand($@"select * from fish_dish where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Fish" && calories == "Unlimited")
                    {
                        cmd = new NpgsqlCommand($@"select * from fish_dish where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Fish" && calories == "Don't worry about it")
                    {
                        cmd = new NpgsqlCommand($@"select * from fish_dish where id=(SELECT floor(random() * (select count (*) from fish_dish) + 1)::int);", conn);
                    }
                }
                {
                    if (dish == "Vegetable dish" && calories == "1-150")
                    {
                        cmd = new NpgsqlCommand($@"select * from vegetable_dish  where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Vegetable dish" && calories == "151-250")
                    {
                        cmd = new NpgsqlCommand($@"select * from vegetable_dish  where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Vegetable dish" && calories == "251-350")
                    {
                        cmd = new NpgsqlCommand($@"select * from vegetable_dish  where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Vegetable dish" && calories == "Unlimited")
                    {
                        cmd = new NpgsqlCommand($@"select * from vegetable_dish  where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Vegetable dish" && calories == "Don't worry about it")
                    {
                        cmd = new NpgsqlCommand($@"select * from vegetable_dish where id=(SELECT floor(random() * (select count (*) from vegetable_dish) + 1)::int);", conn);
                    }
                }
                {
                    if (dish == "Snack" && calories == "1-150")
                    {
                        cmd = new NpgsqlCommand($@"select * from snack_dish where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Snack" && calories == "151-250")
                    {
                        cmd = new NpgsqlCommand($@"select * from snack_dish where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Snack" && calories == "251-350")
                    {
                        cmd = new NpgsqlCommand($@"select * from snack_dish where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Snack" && calories == "Unlimited")
                    {
                        cmd = new NpgsqlCommand($@"select * from snack_dish where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Snack" && calories == "Don't worry about it")
                    {
                        cmd = new NpgsqlCommand($@"select * from snack_dish where id=(SELECT floor(random() * (select count (*) from snack_dish) + 1)::int);", conn);
                    }
                }
                {
                    if (dish == "Dessert" && calories == "1-150")
                    {
                        cmd = new NpgsqlCommand($@"select * from dessert_dish where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Dessert" && calories == "151-250")
                    {
                        cmd = new NpgsqlCommand($@"select * from dessert_dish where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Dessert" && calories == "251-350")
                    {
                        cmd = new NpgsqlCommand($@"select * from dessert_dish where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Dessert" && calories == "Unlimited")
                    {
                        cmd = new NpgsqlCommand($@"select * from dessert_dish where calories_content = '{calories}' order by random();", conn);
                    }
                    else if (dish == "Dessert" && calories == "Don't worry about it")
                    {
                        cmd = new NpgsqlCommand($@"select * from dessert_dish where id=(SELECT floor(random() * (select count (*) from dessert_dish) + 1)::int);", conn);
                    }
                }

                NpgsqlDataReader reader = cmd.ExecuteReader();
                List<string[]> data = new List<string[]>();
                data.Add(new string[4]);

                while (reader.Read())
                {
                    data[data.Count - 1][0] = reader[0].ToString();
                    data[data.Count - 1][1] = reader[1].ToString();
                    data[data.Count - 1][2] = reader[2].ToString();
                    data[data.Count - 1][3] = reader[3].ToString();
                }
                string a = "";
                string b = "";
                a = data[data.Count - 1][1];
                b = data[data.Count - 1][3];
                var keyboard_dish = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl($"{a}", $"{b}")
                    }
                });
                await bot.SendTextMessageAsync(message.From.Id, $"Try to cook:", replyMarkup: keyboard_dish);
                reader.Close();
                conn.Close();
            }
            catch { }
        }
    }
}