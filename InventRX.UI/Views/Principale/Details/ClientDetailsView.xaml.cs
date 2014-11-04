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
using InventRX.Logic.Models.Args;
using InventRX.Logic.Models.Entities;
using InventRX.Logic.Services.Definitions;
using InventRX.UI.ViewModel;

namespace InventRX.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour ClientDetailsView.xaml
    /// </summary>
    public partial class ClientDetailsView : UserControl
    {
        public ClientDetailsViewModel ViewModel { get { return (ClientDetailsViewModel)DataContext; } }
        //private IClientService _clientService;
        public RetrieveClientArgs RetrieveClientArgs { get; set; }
        public Client Client { get; set; }

        public ClientDetailsView()
        {
            InitializeComponent();
            DataContext = new ClientDetailsViewModel();
        }
        public ClientDetailsView(IDictionary<string,object> parameters):this()
        {
            ViewModel.Client = parameters["Client"] as Client;
        }
    }
}
