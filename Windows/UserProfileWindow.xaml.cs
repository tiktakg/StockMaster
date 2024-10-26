using Microsoft.VisualBasic.Logging;
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
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace StockMaster.Windows
{
    /// <summary>
    /// Interaction logic for UserProfileWindow.xaml
    /// </summary>
    public partial class UserProfileWindow : Window
    {
        private int _userId;
        public UserProfileWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
            LoadUserProfile();
        }
        private void LoadUserProfile()
        {
            User currentUser = Tools.GetCurrentUserById(_userId);

            UsernameTextBox.Text = currentUser.Username;
            EmailTextBox.Text = currentUser.Email;
            PasswordTextBox.Text = currentUser.Password;

            if (currentUser.Role == 1)
                RoleTextBox.Text = "Админ";
            else
                RoleTextBox.Text = "Пользователь";



        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordTextBox.Text;
            string login  = UsernameTextBox.Text;


            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Tools.isDataForUserCorrect(login, password, email))
                return;

            StockContext.UpdateUserById(_userId,login,email,password);

            MessageBox.Show("Данные успешно обновлены !", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
