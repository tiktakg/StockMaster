using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StockMaster.Windows
{
    public partial class BuyStockWindow : Window
    {
        private readonly PortfolioManager _portfolioManager;
        private readonly int _userId;

        public BuyStockWindow(string Name, decimal? BuyPrice)
        {
            InitializeComponent();

            StockNameTextBlock.Text = Name;
            StockPriceTextBlock.Text = BuyPrice.ToString();

            _userId = MainWindow._userId;
        }

        // Обработчик для проверки ввода только положительных чисел
        private void QuantityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(QuantityTextBox.Text, out int quantity) && quantity > 0)
            {
                // Парсим стоимость акции
                if (decimal.TryParse(StockPriceTextBlock.Text, out decimal buyPrice))
                {
                    // Обновляем значение общей суммы
                    TotalAmountTextBlock.Text = (quantity * buyPrice).ToString(); // Пример форматирования суммы
                }
            }
            else
            {
                // Очищаем поле общей суммы, если введенные данные некорректны
                TotalAmountTextBlock.Text = string.Empty;
            }


        }

        // Обработчик нажатия кнопки "Купить"
        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(QuantityTextBox.Text, out int quantity) && quantity > 0)
            {
                // Парсим стоимость акции
                if (decimal.TryParse(StockPriceTextBlock.Text, out decimal purchasePrice))
                {
                    // Получаем или создаем портфолио
                    var portfolio = Tools.GetUserPortfolio(_userId);
                    int stockId = Tools.GetStockId(StockNameTextBlock.Text);

                    PortfolioManager.AddOrUpdateStock(portfolio.PortfolioId, stockId, quantity, purchasePrice);

                    MessageBox.Show("Акции успешно куплены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное количество акций (положительное целое число)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик нажатия кнопки "Отмена"
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрываем окно
            this.Close();
        }
    }
}
