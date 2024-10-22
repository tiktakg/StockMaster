using StockMaster.Entitys;
using StockMaster.Windows.Pages;
using System.Windows;

namespace StockMaster.Windows
{
    public partial class MainWindow : Window
    {
        public static int _userId { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработка нажатия кнопки "Все акции" для загрузки StocksPage
        private void AllStocksButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new StockPage());
        }

        private void AllSectorsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SectorPage());
        }

        private void AllCompaniesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CompanyPage());
        }

        private void PortfolioButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PortfolioPage());

        }

        private void WatchListButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
