using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ExpenseTracker
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
             Navigation.PushModalAsync(new loadingpage(), false);
            totalexp();
            Navigation.PopModalAsync(false);
        }

        public async void totalexp()
        {
            var htcli = new HttpClient();
            var d = await htcli.GetStringAsync("http://htw123-001-site1.gtempurl.com/exp/totalexp");
            await SecureStorage.SetAsync("TotalExpense", d.ToString());
            lblexp.Text ="Total Expense : "+  d.ToString()+ " Ks";
        }

        public static class Constants
        {
            // The iOS simulator can connect to localhost. However, Android emulators must use the 10.0.2.2 special alias to your host loopback interface.
            public static string BaseAddress = Device.RuntimePlatform == Device.Android ? "http://htw123-001-site1.gtempurl.com" : "http://htw123-001-site1.gtempurl.com/";
            public static string TodoItemsUrl = BaseAddress + "/exp/record/{0}";
        }

        public async void SaveExpenseRecord(string it,string pr,DateTime dat)
        {
            var htcli = new HttpClient();
            var uri = new Uri(string.Format(Constants.TodoItemsUrl, string.Empty));
            HttpResponseMessage list = await htcli.GetAsync("http://htw123-001-site1.gtempurl.com/exp/check");
            
            var digit = 0;
            if (list.IsSuccessStatusCode)
            {
                var last = await htcli.GetStringAsync("http://htw123-001-site1.gtempurl.com/exp/last");
                digit =Convert.ToInt32( last) + 1;
            }
            else
            {
                digit = 1;
            }
            try
            {
                if (txtitem.Text != string.Empty && txtprice.Text != string.Empty)
                {
                    Expense ex = new Expense();
                    ex.ExpenseID = Convert.ToInt32(digit);
                    ex.ExpenseDate = Convert.ToDateTime(startDatePicker.Date);
                    ex.ExpenseItem = it;
                    ex.Quantity = 0;
                    ex.Price = 0;
                    ex.Amount = Convert.ToDecimal(pr);
                    var json = JsonConvert.SerializeObject(ex);


                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    await htcli.PostAsync(uri, content);
                    
                    await DisplayAlert("Result", "Successfully", "OK");
                    txtitem.Text = string.Empty;
                    txtprice.Text = string.Empty;
                    totalexp();
                    //InitializeComponent();
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Result", "Something Wrong! \n" + ex.Message, "Ok");
            }
        }

        private void btnRegister_Clicked(object sender, EventArgs e)
        {

            SaveExpenseRecord(txtitem.Text.Trim(),txtprice.Text.Trim(),startDatePicker.Date.AddDays(1));
        }

        private void startDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            
        }

        private void btnexpview_Clicked(object sender, EventArgs e)
        {
             Navigation.PushAsync(new ExpenseView(), true);
           
        }
    }
}
