using AppTextAnalytics.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace AppTextAnalytics.Helpers
{
    public class DataAccess
    {
        public SQLiteAsyncConnection connection;
        public SQLiteConnection connectionNotAsync;

        public DataAccess()
        {
            var config = DependencyService.Get<IConfigSQL>();
            this.connection = new SQLiteAsyncConnection(
                Path.Combine(config.DirectoryDB, "apptextanalytics.db3")
                );

            connection.CreateTableAsync<Models.KeyAplicationDTO>().Wait();          
            connection.CreateTableAsync<Models.TextAnalyticsDTO>().Wait();

            connectionNotAsync = new SQLiteConnection(Path.Combine(config.DirectoryDB, "apptextanalytics.db3"));
        }
    }
}
