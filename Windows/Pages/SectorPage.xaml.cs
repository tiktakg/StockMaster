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

namespace StockMaster.Windows
{
    /// <summary>
    /// Interaction logic for test.xaml
    /// </summary>
    public partial class SectorPage : Page
    {
        public SectorPage()
        {
            InitializeComponent();
            LoadStocks();
        }
        private void LoadStocks()
        {

            var Sectors = new ObservableCollection<SectorViewModel>(Tools.GetAllSector());
            StocksDataGrid.ItemsSource = Sectors;
        }
        public class SectorViewModel
        {
            public string Name { get; set; }
            public string Desription { get; set; }
            public decimal? CountCompanyis { get; set; }
        }

    }
}

 

