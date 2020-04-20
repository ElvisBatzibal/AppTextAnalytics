using System;
using System.Collections.Generic;
using System.Text;

namespace AppTextAnalytics.ViewModels
{
    public class MainViewModel
    {
        //1
        private static MainViewModel instance;
        public TextAnalyticsViewModel TextAnalytics { get; set; }
        public MainViewModel()
        {
            //2
            instance = this;
            //this.LoadMenus();
        }
        //3
        public static MainViewModel GetInstance()
        {
            //4
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
    }
}
