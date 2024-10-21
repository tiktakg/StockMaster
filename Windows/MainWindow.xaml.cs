using StockMaster.Entitys;
using System.Windows;

namespace StockMaster.Windows
{
    public partial class MainWindow : Window
    {
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
    }
}
