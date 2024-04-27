using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using BTCViewer.Services;
using BTCViewer.Models.ViewModels;
using ScottPlot;
using static OpenTK.Graphics.OpenGL.GL;
using System.Windows.Markup;
using ScottPlot.TickGenerators.TimeUnits;
using ScottPlot.Plottables;

namespace BTCViewer
{
    public partial class MainWindow : Window
    {
        private const string UrlApi = "https://api.coindesk.com/v1/bpi/currentprice.json";
        private const string UrlApiCNB = "https://api.cnb.cz/cnbapi/exrates/daily?lang=EN";

        DatabaseService DBService;
        GraphService graphService;
        BTCInfo btcData;
        decimal czkRate;

        public MainWindow()
        {
            InitializeComponent();

            getCZKRate();

            dataGridLive.DataContext = this;
            BTCInfo = getLiveData();

            DBService = new DatabaseService();
            List<DbBTCViewModel> dbData = DBService.Load();
            dataGridDB.ItemsSource = dbData;

            graphService = new GraphService();
            updateGraph(dbData);
            BtcGraph.Plot.Axes.DateTimeTicksBottom();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(30000);
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            BTCInfo.Clear();

            foreach (Currency Item in getLiveData())
                BTCInfo.Add(Item);
        }

        private ObservableCollection<Currency> getLiveData()
        {
            string json = (new WebClient()).DownloadString(UrlApi);
            btcData = JsonConvert.DeserializeObject<BTCInfo>(json);

            updated.Content = btcData.time.updated;
            updatedISO.Content = btcData.time.updatedISO;
            updatedUK.Content = btcData.time.updateduk;

            czkRateLabel.Content = Math.Round(czkRate * btcData.bpi.EUR.rate_float, 4) + " CZK";

            return new ObservableCollection<Currency>
            {
                btcData.bpi.EUR,
                btcData.bpi.USD,
                btcData.bpi.GBP
            };
        }

        private void saveDB_Click(object sender, RoutedEventArgs e)
        {
            DBService.Save(btcData);

            List<DbBTCViewModel> dbData = DBService.Load();
            dataGridDB.ItemsSource = DBService.Load();

            updateGraph(dbData);
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            DBService.Delete(dataGridDB.SelectedItems.Cast<DbBTCViewModel>().ToList());

            List<DbBTCViewModel> dbData = DBService.Load();
            dataGridDB.ItemsSource = dbData;

            updateGraph(dbData);
        }

        private void updateDB_Click(object sender, RoutedEventArgs e)
        {
            DBService.Update(dataGridDB.ItemsSource.Cast<DbBTCViewModel>().ToList());
        }

        private void getCZKRate()
        {
            string json = (new WebClient()).DownloadString(UrlApiCNB);
            Rates eRates = JsonConvert.DeserializeObject<Rates>(json);
            czkRate = eRates.rates.FirstOrDefault(x => x.currencyCode == "EUR").rate;
        }

        private void updateGraph(List<DbBTCViewModel> dbData)
        {
            BtcGraph.Plot.Clear();
            foreach (KeyValuePair<string, KeyValuePair<DateTime[], decimal[]>> axis in graphService.LoadScatter(dbData))
            {
                Scatter scatter = BtcGraph.Plot.Add.Scatter(axis.Value.Key, axis.Value.Value);
                scatter.LegendText = axis.Key;
            }
        }

        public ObservableCollection<Currency> BTCInfo { get; set; }
    }
}
