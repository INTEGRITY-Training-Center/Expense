using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


     class EXPView
    {

        public DateTime ExpDate { get; set; }
        public decimal ExpAmount { get; set; }
    }
    public partial class ExpenseView : ContentPage
    {
        public ExpenseView()
        {
            InitializeComponent();
            var s = SecureStorage.GetAsync("TotalExpense");
            Title = "Expenses : "+s.Result+ "Ks";
            expview();
        }
        public ICommand TapCommand { get; set; }
        private void CmdTap()
        {
            //CCListDatabaseOperations objcclOp = new CCListDatabaseOperations();
            //Teams = objcclOp.GetSearchList(OTML_ID_Number, Name);

        }

        //private void Grid_Tap(object sender, DataGridGestureEventArgs e)
        //{
        //    if (e.Item != null)
        //    {
        //        var editForm = new EditFormPage(grid, grid.GetRow(e.RowHandle).Item);
        //        Navigation.PushAsync(editForm);
        //    }
        //}

        async void expview()
        {
            await Navigation.PushModalAsync(new loadingpage(), false);
            var htcli = new HttpClient();
            var contact = await htcli.GetStringAsync("http://htw123-001-site1.gtempurl.com/exp/allexp");
            var data = JsonConvert.DeserializeObject<List<EXPView>>(contact);

            List<EXPView> lstexpview = new List<EXPView>();
            foreach(var obj in data)
            {
                EXPView expv = new EXPView();
                expv.ExpDate =DateTime.Parse( obj.ExpDate.ToShortDateString()) ;
                expv.ExpAmount = obj.ExpAmount;

                lstexpview.Add(expv);
            }

            gvExpenseList.ItemsSource = lstexpview;
            await Navigation.PopModalAsync(false);
        }

        private async  void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            var myListView = (Label)sender;
            var myItem = myListView.Text;

            //await DisplayAlert("Clicked !!! ","really"+myItem,"OK");

            if (!string.IsNullOrEmpty(myItem))
            {
                await SecureStorage.SetAsync("ExpDate", myItem);
                var secondPage = new ExpenseDetailView();
                //secondPage.BindingContext = data;
                await Navigation.PushAsync(secondPage);

            }
        }




        //public class GridLabel : Label
        //{
        //    public GridLabel()
        //    {
        //        HorizontalOptions = LayoutOptions.CenterAndExpand;
        //        VerticalOptions = LayoutOptions.CenterAndExpand;
        //    }

        //    public static readonly BindableProperty ItemProperty = BindableProperty.Create(nameof(Item), typeof(object), typeof(EXPView), null, BindingMode.OneWay, null, OnItemPropertyChanged);


        //    private static void OnItemPropertyChanged(BindableObject bindableObject, object oldValue, object newValue)
        //    {
        //        var gridLabel = (GridLabel)bindableObject;
        //        gridLabel.Item = newValue;
        //    }

        //    public object Item
        //    {
        //        get => GetValue(ItemProperty);
        //        set => SetValue(ItemProperty, value);
        //    }
        //}


    }
}