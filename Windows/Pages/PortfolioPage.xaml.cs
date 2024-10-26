using StockMaster.Entitys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static StockMaster.Windows.StockPage;

namespace StockMaster.Windows.Pages
{
    /// <summary>
    /// Interaction logic for PortfolioPage.xaml
    /// </summary>
    public partial class PortfolioPage : Page
    {
        int idPortfolio;
        public PortfolioPage(int IdPortfolio)
        {
            InitializeComponent();
            idPortfolio = IdPortfolio;
            LoadStocks();
        }

        private void LoadStocks()
        {

            var Portfolio = new ObservableCollection<PortfolioViewModel>(Tools.GetCurrentPortfolio(Tools.GetIdPortfolio(idPortfolio)));
            StocksDataGrid.ItemsSource = Portfolio;

        }
    }
}
