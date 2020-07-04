using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker
{
    class EXPDetailView
    {

        public string ExpItem { get; set; }
        public decimal ExpAmount { get; set; }
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpenseDetailView : ContentPage
    {
        public ExpenseDetailView()
        {
            InitializeComponent();
            
            var s = SecureStorage.GetAsync("ExpDate");
            Title = s.Result;
            //ExpenseDate.Text = s.Result;

            expview( Convert.ToDateTime(s.Result));
            //DisplayAlert("Clicked !!! ", "really" + s.Result, "OK");
        }

        async void expview(DateTime da)
        {
            await Navigation.PushModalAsync(new loadingpage(), false);
            var htcli = new HttpClient();
            var contact = await htcli.GetStringAsync("http://htw123-001-site1.gtempurl.com/exp/bydate?date=" + da);
            var data = JsonConvert.DeserializeObject<List<EXPDetailView>>(contact);

            List<EXPDetailView> lstexpview = new List<EXPDetailView>();
            foreach (var obj in data)
            {
                EXPDetailView expv = new EXPDetailView();
                expv.ExpItem = obj.ExpItem;
                expv.ExpAmount = obj.ExpAmount;

                lstexpview.Add(expv);
            }

            gvExpenseList.ItemsSource = lstexpview;
            await Navigation.PopModalAsync(false);
        }


    }
}