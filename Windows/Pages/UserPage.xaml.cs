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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace StockMaster.Windows.Pages
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private int userId;
        public UserPage(int userId)
        {
            InitializeComponent();
            LoadUsers();
            this.userId = userId;   
        }
        private void LoadUsers()
        {

            var Users = new ObservableCollection<UserViewModel>(Tools.GetAllUsers());
            UsersDataGrid.ItemsSource = Users;

            UsersDataGrid.LoadingRow += StocksDataGrid_LoadingRow;

        }
        private void StocksDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            User currentUser = Tools.GetCurrentUserById(userId);

            var userViewModel = e.Row.Item as UserViewModel;

            if (userViewModel != null && userViewModel.Email == currentUser.Email)
            {
                e.Row.IsEnabled = false;
                e.Row.Background = Brushes.WhiteSmoke;

            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string email)
            {
                int userId = Tools.GetUserIdByEmail(email);

                StockContext.DeleteUserById(userId);
                MessageBox.Show($"Пользователь с email: {email} удален.");
            }

            LoadUsers();

        }

        private void MakeAdminButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string email)
            {
                int userId = Tools.GetUserIdByEmail(email);
                StockContext.MakeAdminUserRoleById(userId);
                MessageBox.Show($"Пользователь с  email: {email} был сделан админом.");
            }

            LoadUsers();
        }

    }
}
