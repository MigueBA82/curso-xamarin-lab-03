using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToDosPage : ContentPage
    {
        public ObservableCollection<ToDoItem> ToDos { get; set; } = new ObservableCollection<ToDoItem>();

        public ToDoService Service { get; } = new ToDoService();

        public ToDosPage()
        {
            Title = "My to Dos";

            InitializeComponent();

            BindableLayout.SetItemsSource(ItemsContainer, ToDos);

            Root.Children.Add(new Button
            {
                Text = "Add",
                Command = new Command(() => AddItem()),
                //VerticalOptions
            });

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ToDoItem[] result = await Service.GetToDos();
            Debug.WriteLine(result);
            Debug.WriteLine(result.Length);
            foreach (var item in result)
            {
                ToDos.Add(item);
            }
        }

        private void AddItem()
        {
            ToDos.Add(new ToDoItem { Title= DateTime.Now.ToString()});
        }
    }

    public class ToDoService
    {
        public async Task<ToDoItem[]> GetToDos()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://2fe5-186-176-179-235.ngrok.io");
            string json = await httpClient.GetStringAsync("/api/todos");
            var result= JsonConvert.DeserializeObject<ToDoItem[]>(json);
            return result;
        }
    }

    public class ToDoItem
    {
        public int id { get; set; }
        public string Title { get; set; }
        public bool isComplete { get; set; }
    }

}