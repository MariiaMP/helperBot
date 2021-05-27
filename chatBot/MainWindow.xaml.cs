using System;
using System.IO;
using Telegram.Bot;
using System.Windows;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Controls;
using Telegram.Bot.Types.Enums;
using System.Collections.ObjectModel;
using Telegram.Bot.Types.ReplyMarkups;

namespace chatBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Users = new ObservableCollection<User>();
            usersList.ItemsSource = Users;
            bot = new TelegramBotClient(token);

            bot.OnMessage += BotOnMessageReceiving;
            bot.OnMessage += delegate (object sender, Telegram.Bot.Args.MessageEventArgs e)
             {
                 string msg = $"{DateTime.Now} : {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";
                 File.AppendAllText("data.log", $"{msg}\n");

                 Debug.WriteLine(msg);

                 this.Dispatcher.Invoke(() =>
                 {
                     var person = new User(e.Message.Chat.Id, e.Message.Chat.FirstName);
                     if (!Users.Contains(person))
                     {
                         Users.Add(person);
                     }
                     Users[Users.IndexOf(person)].AddNewMessage($"{person.Nickname}: {e.Message.Text}");
                 });
             };
            bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            bot.StartReceiving();

            ButtonSentMessage.Click += delegate { SendMessage(); };
            textBoxSendMessage.KeyDown += (s, e) =>
            {
                if (e.Key == Key.Return)
                {
                    SendMessage();
                }
            };
        }

        public static async void BotOnMessageReceiving(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;
            switch (message.Text)
            {
                case "/start":
                    var keyboard_start = new ReplyKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            new KeyboardButton("/contacts"),//{ RequestContact = true},
                            new KeyboardButton("/dish_wish"),
                            new KeyboardButton("/help_to_calculate_calories_norm")
                        }
                    });
                    await bot.SendTextMessageAsync(message.From.Id, "Menu", replyMarkup: keyboard_start);
                    break;
                    
                case "/contacts":
                    var keyboard_contacts = new InlineKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("Telegram", "https://t.me/mariaa_p12"),
                            InlineKeyboardButton.WithUrl("Email","mailto:mpisotska12@gmail.com"),
                            InlineKeyboardButton.WithUrl("Insta","https://www.instagram.com/mariaa_p12/?hl=uk")
                        }
                    });
                    await bot.SendTextMessageAsync(message.From.Id, "If you want to contact me, please, choose in  which way", replyMarkup: keyboard_contacts);
                    break;
                    
                default:
                    break;
            }

            {
                if (message.Text.Contains("/dish_wish"))
                {
                    var keyboard_cook = new ReplyKeyboardMarkup(new[]
                        {
                        new[]
                        {
                            new KeyboardButton("Meat"),
                            new KeyboardButton("Fish"),
                            new KeyboardButton("Vegetable dish")
                        },
                        new[]
                        {
                            new KeyboardButton("Snack"),
                            new KeyboardButton("Dessert")
                        }
                    });
                    await bot.SendTextMessageAsync(message.From.Id, "Please, choose what you plan to cook:", replyMarkup: keyboard_cook);
                    return;
                }
                if (message.Text.Contains("Meat") || message.Text.Contains("Fish") || message.Text.Contains("Vegetable") || message.Text.Contains("Snack") || message.Text.Contains("Dessert"))
                {
                    dish = message.Text;
                    var keyboard_cal = new ReplyKeyboardMarkup(new[]
                        {
                        new[]
                        {
                            new KeyboardButton("1-150"),
                            new KeyboardButton("151-250"),
                            new KeyboardButton("251-350")
                        },
                        new[]
                        {
                            new KeyboardButton("Unlimited"),
                            new KeyboardButton("Don't worry about it")
                        }
                    });
                    await bot.SendTextMessageAsync(message.From.Id, "Please, choose caloric content:", replyMarkup: keyboard_cal);
                    return;
                }
                if (message.Text.Contains("1-150") || message.Text.Contains("151-250") || message.Text.Contains("251-350") || message.Text.Contains("Unlimited") || message.Text.Contains("Don't worry about it"))
                {
                    calories = message.Text;
                    await recommended_dish(e);
                }
            }
            {
                if (message.Text.Contains("/help_to_calculate_calories_norm"))
                {
                    var keyboard_height = new ReplyKeyboardMarkup(new[]
                    {
                    new[]
                    {
                        new KeyboardButton("1-130 cm"),
                        new KeyboardButton("131-145 cm"),
                        new KeyboardButton("146-160 cm")
                    },
                    new[]
                    {
                        new KeyboardButton("161-175 cm"),
                        new KeyboardButton("176-190 cm"),
                        new KeyboardButton("191-220+ cm")
                    }
                });
                    await bot.SendTextMessageAsync(message.From.Id, "Please, choose your height:", replyMarkup: keyboard_height);
                    return;
                }
                if (message.Text.Contains("cm"))
                {
                    height = message.Text;
                    var keyboard_weight = new ReplyKeyboardMarkup(new[]
                    {
                    new[]
                    {
                        new KeyboardButton("5-15 kg"),
                        new KeyboardButton("16-30 kg"),
                        new KeyboardButton("31-45 kg")
                    },
                    new[]
                    {
                        new KeyboardButton("46-60 kg"),
                        new KeyboardButton("61-75 kg"),
                        new KeyboardButton("76-90 kg")
                    },
                    new[]
                    {
                        new KeyboardButton("91-105 kg"),
                        new KeyboardButton("106-120 kg"),
                        new KeyboardButton("121-135+ kg")
                    }
                });
                    await bot.SendTextMessageAsync(message.From.Id, "Please, choose your weight:", replyMarkup: keyboard_weight);
                    weight = keyboard_weight.ToString();
                    return;
                }
                if (message.Text.Contains("kg"))
                {
                    weight = message.Text;
                    var keyboard_age = new ReplyKeyboardMarkup(new[]
                    {
                    new[]
                    {
                        new KeyboardButton("4-10 years"),
                        new KeyboardButton("11-16 years"),
                        new KeyboardButton("17-22 years")
                    },
                    new[]
                    {
                        new KeyboardButton("23-35 years"),
                        new KeyboardButton("36-45 years"),
                        new KeyboardButton("46-55 years")
                    },
                    new[]
                    {
                        new KeyboardButton("56-65 years"),
                        new KeyboardButton("66-75 years"),
                        new KeyboardButton("76-85+ years")
                    }
                });
                    await bot.SendTextMessageAsync(message.From.Id, "Please, choose your age:", replyMarkup: keyboard_age);
                    age = keyboard_age.ToString();
                    return;
                }
                if (message.Text.Contains("years"))
                {
                    age = message.Text;
                    var keyboard_sex = new ReplyKeyboardMarkup(new[]
                    {
                    new[]
                    {
                        new KeyboardButton("male"),
                        new KeyboardButton("female")
                    }
                });
                    await bot.SendTextMessageAsync(message.From.Id, "Please, choose your sex:", replyMarkup: keyboard_sex);
                    return;
                }
                if (message.Text.Contains("male"))
                {
                    sex = message.Text;
                    await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Okay! Your entered informations:\n\nYour sex: *{sex}*\n" + $"Your Age: *" +
                        $"{age}*\nYour height: *{height}*\n" + $"Weight *{weight}*", ParseMode.Markdown);
                    await res(e);
                    message.Text = "/start";
                }
            }
        }

        public static async void BotOnCallbackQueryReceived(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {

        }

        private void UsersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ConcreteUsersChat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonSentMessage_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}