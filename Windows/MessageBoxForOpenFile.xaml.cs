using System;
using System.Collections.Generic;
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

namespace StockMaster.Windows
{
    /// <summary>
    /// Interaction logic for MessageBoxForOpenFile.xaml
    /// </summary>
    public partial class MessageBoxForOpenFile : Window
    {
        public bool IsOpenClicked { get; private set; } = false;

        public MessageBoxForOpenFile()
        {
            InitializeComponent();

            OpenButton.Click += (s, e) => { IsOpenClicked = true; Close(); };
            CancelButton.Click += (s, e) => Close();
        }
    }
}
