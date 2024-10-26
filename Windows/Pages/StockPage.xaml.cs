using StockMaster.Entitys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StockMaster.Windows
{
  
    public partial class StockPage : Page
    {
        public StockPage()
        {
            InitializeComponent();
            LoadStocks();
        }

        private void LoadStocks()
        {
            var Stocks = new ObservableCollection<StockViewModel>(Tools.GetAllStock());
            StocksDataGrid.ItemsSource = Stocks;
        }
        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dataGridRow = DataGridRow.GetRowContainingElement(button);
            var stock = dataGridRow.Item as StockViewModel; // Замените Stock на вашу модель данных

            if (stock != null)
            {
                BuyStockWindow buyStockWindow = new BuyStockWindow(stock.Name, stock.BuyPrice);
                buyStockWindow.ShowDialog();

            }
        }
        
    }
}
