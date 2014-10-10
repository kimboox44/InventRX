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
using System.Collections.ObjectModel;
using InventRX.Services.Definitions;
using InventRX.Logic.Model.Args;
using Cstj.MvvmToolkit.Services;

namespace InventRX.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour SoumissionView.xaml
    /// </summary>
    public partial class SoumissionView : UserControl
    {
        public SoumissionViewModel ViewModel { get { return (SoumissionViewModel)DataContext; } }

        private IClientService _clientService;
        public RetrieveClientArgs RetrieveClientArgs { get; set; }
        public Client Client { get; set; }

        public SoumissionView()
        {
            InitializeComponent();
            DataContext = new SoumissionViewModel();
        }

        public SoumissionView(IDictionary<string,object> parameters):this()
        {
            ViewModel.Soumission = parameters["Soumission"] as Soumission;
        }

        private void btnSauvegarder_Click(object sender, RoutedEventArgs e)
        {
            //Si aucun client, on va le chercher dans la base de données par son numéro de téléphone
            if (ViewModel.Soumission.Client == null)
            {
                _clientService = ServiceFactory.Instance.GetService<IClientService>();
                RetrieveClientArgs = new RetrieveClientArgs();
                RetrieveClientArgs.IdClient = 2;
                Client = _clientService.Retrieve(RetrieveClientArgs);
                MessageBox.Show(Client.Nom);
            }

            ViewModel.SauvegarderCommand();
        }

        private void datagridListeProduits_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Produit produitSelectionne = (datagridListeProduits.SelectedItem as Produit);

            if (produitSelectionne != null)
            {
                ItemSoumission newItemSoumission = new ItemSoumission();
                newItemSoumission.Soumission = ViewModel.Soumission;
                newItemSoumission.Produit = produitSelectionne;
                newItemSoumission.NomProduit = produitSelectionne.Nom;
                newItemSoumission.Quantite = 1;
                newItemSoumission.PrixUnitaire = produitSelectionne.Prix;

                if (datagridListeItemsSoumission.ItemsSource != null)
                {
                    ViewModel.Soumission.ItemsSoumission.Add(newItemSoumission);
                    datagridListeItemsSoumission.Items.Refresh();
                }
            }
        }
    }
}
