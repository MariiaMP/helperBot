using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace chatBot
{
    public class User : INotifyPropertyChanged, IEquatable<User>
    {
        private long id;
        public long Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Id)));
            }
        }

        private string nickname;
        public string Nickname
        {
            get
            {
                return this.nickname;
            }
            set
            {
                this.nickname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Nickname)));
            }
        }

        public User(long id, string name)
        {
            this.id = id;
            this.nickname = name;
            Message = new ObservableCollection<string>();
        }


        public ObservableCollection<string> Message { get; set; }
                
        public event PropertyChangedEventHandler PropertyChanged;

        public bool Equals(User u) => u.id == this.id;

        public void AddNewMessage(string message) => Message.Add(message);

    }
}
