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
using InventRX.Logic.Model.Args;
using InventRX.Logic.Model.Entities;
using InventRX.Services.Definitions;
using InventRX.UI.ViewModel;
using Cstj.MvvmToolkit.Services;

namespace InventRX.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour ClientView.xaml
    /// </summary>
    public partial class ClientView : UserControl
    {
        public ClientViewModel ViewModel { get { return (ClientViewModel)DataContext; } }
        private IClientService _clientService;
        public RetrieveClientArgs RetrieveClientArgs { get; set; }
        public Client Client { get; set; }
        
        public ClientView()
        {
            InitializeComponent();
            DataContext = new ClientViewModel();
        }
        public ClientView(IDictionary<string,object> parameters):this()
        {
            ViewModel.Client = parameters["Client"] as Client;
        }
    }
}
