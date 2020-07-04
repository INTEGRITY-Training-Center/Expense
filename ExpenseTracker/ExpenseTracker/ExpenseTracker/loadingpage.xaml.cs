using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class loadingpage : ContentPage
    {
        bool isTaskRunning=true;
        public loadingpage()
        {
            InitializeComponent();
            UpdateUiState();
        }

        void UpdateUiState()
        {
            runningStatusLabel.Text = isTaskRunning ? "Loading..." : "All tasks complete!";
           // defaultActivityIndicator.IsRunning = isTaskRunning;
            styledActivityIndicator.IsRunning = isTaskRunning;
        }
    }
}