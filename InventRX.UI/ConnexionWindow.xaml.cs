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
using InventRX.UI.ViewModel;
using InventRX.UI.Views;

namespace InventRX.UI
{
    /// <summary>
    /// Logique d'interaction pour ConnexionWindow.xaml
    /// </summary>
    public partial class ConnexionWindow : Window
    {
        public ConnexionViewModel ViewModel { get { return (ConnexionViewModel)DataContext; } }

        public ConnexionWindow()
        {
            InitializeComponent();
            DataContext = new ConnexionViewModel();
            ViewModel.CurrentView = new ConnexionView();
        }

    }
}
