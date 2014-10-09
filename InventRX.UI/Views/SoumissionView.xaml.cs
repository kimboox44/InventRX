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
using System.Windows.Navigation;
using System.Windows.Shapes;
using InventRX.Logic.Model.Entities;
using InventRX.UI.ViewModel;

namespace InventRX.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour SoumissionView.xaml
    /// </summary>
    public partial class SoumissionView : UserControl
    {
        public SoumissionViewModel ViewModel { get { return (SoumissionViewModel)DataContext; } }

        public SoumissionView()
        {
            InitializeComponent();
            DataContext = new SoumissionViewModel();
        }

        public SoumissionView(IDictionary<string,object> parameters):this()
        {
            ViewModel.Soumission = parameters["Soumission"] as Soumission;
            datagridListeItemsSoumission.ItemsSource = ViewModel.Soumission.ItemsSoumission;
        }

        private void btnSauvegarder_Click(object sender, RoutedEventArgs e)
        {
            //ViewModel.SauvegarderCommand();
        }
    }
}
