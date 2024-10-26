using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using StockMaster;
using StockMaster.Entitys;

namespace StockMaster.windows
{

    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string login = LoginTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if(!Tools.isDataForUserCorrect(login, password, email))
                return;

       
            Tools.RegisterUser(login, email, password);

            MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
       
       


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow  = new AuthWindow();
            authWindow.Show();
            this.Close();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Прячем placeholder, если поле получает фокус
                var placeholder = FindPlaceholder(textBox);
                if (placeholder != null)
                    placeholder.Visibility = Visibility.Collapsed; // Скрываем placeholder
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Показываем placeholder, если поле пустое и теряет фокус
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    var placeholder = FindPlaceholder(textBox);
                    if (placeholder != null)
                        placeholder.Visibility = Visibility.Visible; // Показываем placeholder
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Скрываем placeholder, если есть текст
                var placeholder = FindPlaceholder(textBox);
                if (placeholder != null)
                {
                    placeholder.Visibility = string.IsNullOrEmpty(textBox.Text) ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            // Прячем placeholder, если поле получает фокус
            var placeholder = FindPlaceholder(passwordBox);
            if (placeholder != null)
                placeholder.Visibility = Visibility.Collapsed; // Скрываем placeholder
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            // Показываем placeholder, если поле пустое и теряет фокус
            if (string.IsNullOrEmpty(passwordBox.Password))
            {
                var placeholder = FindPlaceholder(passwordBox);
                if (placeholder != null)
                    placeholder.Visibility = Visibility.Visible; // Показываем placeholder
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            // Скрываем placeholder, если есть текст
            var placeholder = FindPlaceholder(passwordBox);
            if (placeholder != null)
            {
                placeholder.Visibility = string.IsNullOrEmpty(passwordBox.Password) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private TextBlock FindPlaceholder(Control control)
        {
            // Находим соответствующий TextBlock для placeholder
            return control.Parent is Grid grid ? (TextBlock)grid.Children
                .OfType<TextBlock>()
                .FirstOrDefault(tb => tb.Name == $"{control.Name}Placeholder") : null;
        }
    }
}
