using DemoStyles.Models;
using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace DemoStyles.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _content;

        public string Greeting => "Welcome to Avalonia!";

        public ObservableCollection<Contact> Contacts { get; set; }

        public Contact SelectedContact { get; set; }

        public async Task UpdateUsers()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://jsonplaceholder.typicode.com/users");
                var users = JsonConvert.DeserializeObject<List<Contact>>(await response.Content.ReadAsStringAsync());

                await Task.Delay(2000);
                foreach (var item in users)
                {
                    Contacts?.Add(item);
                }
            }
        }

        //public IObservable<string> Now { get; } = Observable
        //    .Timer(DateTimeOffset.Now, TimeSpan.FromSeconds(1))
        //    .Select(_ => DateTime.Now.ToString());

        public string SearchText
        {
            get => _content;
            private set => this.RaiseAndSetIfChanged(ref _content, value);
        }

        public MainWindowViewModel()
        {
            Contacts = new ObservableCollection<Contact>
            {
                new Contact
                {
                    Name = "Alex",
                    Phone = "+37529 134 51 8124"
                },
                new Contact
                {
                    Name = "Dimitry",
                    Phone = "+37529 5 51 81232"
                },
                new Contact
                {
                    Name = "Natalya",
                    Phone = "+37529 213 51 3134"
                },
                new Contact
                {
                    Name = "Sergei",
                    Phone = "+37529 547 51 8124"
                }
            };

            var a = this.WhenAnyValue(vm => vm.SearchText)
                .Throttle(TimeSpan.FromSeconds(1))
                .Select(query => { Console.WriteLine(query); return query; })
                .Subscribe();
        }

        public void DeleteAll()
        {
            Contacts?.Clear();
        }

        public void DeleteContact()
        {
            Contacts?.Remove(SelectedContact);
        }
    }
}
