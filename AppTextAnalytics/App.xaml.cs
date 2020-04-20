using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppTextAnalytics.Views;
using AppTextAnalytics.ViewModels;
namespace AppTextAnalytics
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.TextAnalytics = new TextAnalyticsViewModel();

            this.MainPage = new NavigationPage(new TextAnalyticsPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
