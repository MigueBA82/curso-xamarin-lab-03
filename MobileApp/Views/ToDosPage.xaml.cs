using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToDosPage : ContentPage
    {
        public ObservableCollection<object> ToDos { get; set; } = new ObservableCollection<object>();
        public ToDosPage()
        {
            Title = "My to Dos";

            InitializeComponent();

            ToDos.Add("Buy Cookies");
            ToDos.Add("testc 2");

            BindableLayout.SetItemsSource(ItemsContainer, ToDos);

            Root.Children.Add(new Button { Text = "Add",
                Command = new Command(() => AddItem() ),
                //VerticalOptions
            });
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ToDos.Add(DateTime.Now);

        }

        private void AddItem() {
            ToDos.Add(DateTime.Now);
        }
    }
}