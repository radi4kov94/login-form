using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Plamen
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //Get All Persons
            var personList = await App.SQLiteDb.GetItemsAsync();
            if (personList != null)
            {
                lstPersons.ItemsSource = personList;
            }
        }
        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtPassword.Text))
            {
                Person person = new Person()
                {
                    Name = txtName.Text,
                    Password = txtPassword.Text
                };

                //Add New Person
                await App.SQLiteDb.SaveItemAsync(person);
                txtName.Text = string.Empty;
                txtPassword.Text = string.Empty;

                await DisplayAlert("Success", "Person added Successfully", "OK");
                //Get All Persons
                var personList = await App.SQLiteDb.GetItemsAsync();
                if (personList != null)
                {
                    lstPersons.ItemsSource = personList;
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter Name or Password", "OK");
            }
        }

        private async void BtnRead_Clicked(object sender, EventArgs e)
        {
            var id = sender.GetType().GetProperty("Text").GetValue(sender, null);

            //Get Person
            var person = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(id));
                if (person != null)
                {
                    txtName.Text = person.Name;
                    await DisplayAlert("Success", "Person Name: " + person.Name + " Password: " + person.Password + " Id: " + person.PersonID, "OK");
                }
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPersonId.Text))
            {
                Person person = new Person()
                {
                    PersonID = Convert.ToInt32(txtPersonId.Text),
                    Name = txtName.Text
                };

                //Update Person
                await App.SQLiteDb.SaveItemAsync(person);

                txtPersonId.Text = string.Empty;
                txtName.Text = string.Empty;
                await DisplayAlert("Success", "Person Updated Successfully", "OK");
                //Get All Persons
                var personList = await App.SQLiteDb.GetItemsAsync();
                if (personList != null)
                {
                    lstPersons.ItemsSource = personList;
                }

            }
            else
            {
                await DisplayAlert("Required", "Please Enter PersonID", "OK");
            }
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPersonId.Text))
            {
                //Get Person
                var person = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(txtPersonId.Text));
                if (person != null)
                {
                    //Delete Person
                    await App.SQLiteDb.DeleteItemAsync(person);
                    txtPersonId.Text = string.Empty;
                    await DisplayAlert("Success", "Person Deleted", "OK");

                    //Get All Persons
                    var personList = await App.SQLiteDb.GetItemsAsync();
                    if (personList != null)
                    {
                        lstPersons.ItemsSource = personList;
                    }
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter PersonID", "OK");
            }
        }

        private async void BtnLogIn_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtPassword.Text))
            {
                var person = await App.SQLiteDb.LogInAsync(txtName.Text, txtPassword.Text);
                if (person != null)
                {
                    Application.Current.MainPage = new Logged(person);
                }
                else
                {
                   await DisplayAlert("Error", "Invalid Login, try again", "OK");
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter Name or Password", "OK");
            }
        }
    }
}
