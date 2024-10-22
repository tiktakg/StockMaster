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

namespace StockMaster.Windows.Pages
{
    /// <summary>
    /// Interaction logic for Companies.xaml
    /// </summary>
    public partial class CompanyPage : Page
    {
        public CompanyPage()
        {
            InitializeComponent();
            LoadStocks();
        }
        private void LoadStocks()
        {

            var Company = new ObservableCollection<CompanyViewModel>(Tools.GetAllCompany());
            StocksDataGrid.ItemsSource = Company;
        }
        public class CompanyViewModel
        {
            public string Name { get; set; }
            public string Sector { get; set; }
            public string Ceo { get; set; }
            public string Headquarters { get; set; }
        }
    }
}
