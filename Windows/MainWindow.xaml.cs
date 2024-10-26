using StockMaster.Entitys;
using StockMaster.Windows.Pages;
using System.Windows;
using System.Windows.Controls;

namespace StockMaster.Windows
{
    public partial class MainWindow : Window
    {
        public static int userId { get; set; }

        public MainWindow(int UserId)
        {
            InitializeComponent();
           
            userId = UserId;
            LoadPage();
        }
        private void LoadPage()
        {
            User currentUser = Tools.GetCurrentUserById(userId);
            UserName_TextBlock.Text = "Пользователь: " + currentUser.Username;

            if(currentUser.Role == 1)
                AllUser_Button.Visibility= Visibility.Visible;
        }

        // Обработка нажатия кнопки "Все акции" для загрузки StocksPage
        private void AllStocksButton_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new StockPage());
        private void AllSectorsButton_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new SectorPage());
        private void AllCompaniesButton_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new CompanyPage());
        private void PortfolioButton_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new PortfolioPage(userId));
        private void CreateReport_Click(object sender, RoutedEventArgs e) => ReportGenerator.GenerateReport(userId);
        private void AllUserButton_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new UserPage(userId));
        private void TextBlock_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UserProfileWindow userProfileWindow = new UserProfileWindow(userId);
            userProfileWindow.ShowDialog();

            LoadPage();
        }

       
        
    }
}
