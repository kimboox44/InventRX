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
using InventRX.Logic.Models.Entities;
using InventRX.UI.ViewModel;
using System.Collections.ObjectModel;
using InventRX.Logic.Services.Definitions;
using InventRX.Logic.Models.Args;
using Cstj.MvvmToolkit.Services;

namespace InventRX.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour SoumissionView.xaml
    /// </summary>
    public partial class SoumissionDetailsView : UserControl
    {
        public SoumissionDetailsViewModel ViewModel { get { return (SoumissionDetailsViewModel)DataContext; } }

        private IClientService _clientService;
        public RetrieveClientArgs RetrieveClientArgs { get; set; }
        public Client Client { get; set; }

        public SoumissionDetailsView()
        {
            InitializeComponent();
            DataContext = new SoumissionDetailsViewModel();
        }

        public SoumissionDetailsView(IDictionary<string,object> parameters):this()
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
            bool exist = false;
            Produit produitSelectionne = (datagridListeProduits.SelectedItem as Produit);

            if (produitSelectionne != null)
            {
                //Si le produit existe déjà dans les produits de la soumission, on incrémente de 1 la quantité de celui-ci
                foreach (ItemSoumission item in ViewModel.Soumission.ItemsSoumission)
                {
                    if(item.Produit.IdProduit == produitSelectionne.IdProduit)
                    {
                        item.Quantite = item.Quantite + 1;
                        exist = true;
                        break;
                    }
                }
                //Si le produit n'existe pas dans les produits de la soumission, on l'ajoute
                if (exist == false)
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
                    }
                }
                datagridListeItemsSoumission.Items.Refresh();
            }
        }

        private void btnSupprimerItem_Click(object sender, RoutedEventArgs e)
        {
            ItemSoumission item = (ItemSoumission)((sender as Button).CommandParameter);
            if (item != null)
            {
                if (datagridListeItemsSoumission.ItemsSource != null)
                {
                    ViewModel.Soumission.ItemsSoumission.Remove(item);
                    datagridListeItemsSoumission.Items.Refresh();
                }
            }
        }
    }
}
