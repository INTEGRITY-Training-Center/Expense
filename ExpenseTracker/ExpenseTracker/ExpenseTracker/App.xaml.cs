using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker
{
    public partial class App : Application
    {
        public static INavigation GlobalNavigation { get; private set; }
        public App()
        {
            InitializeComponent();

            var rootPage = new NavigationPage(new MainPage());
            GlobalNavigation = rootPage.Navigation;
            MainPage = rootPage;
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios=b21ab3c2-c7a1-46ee-adff-30a83b05059a;", typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
