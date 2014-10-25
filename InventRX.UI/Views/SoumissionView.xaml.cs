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
        private IProvinceService _provinceService;
        public RetrieveClientArgs RetrieveClientArgs { get; set; }
        public Client Client { get; set; }
        public Employe Employe { get; set; }

        public SoumissionView()
        {
            InitializeComponent();
            DataContext = new SoumissionViewModel();
        }

        public SoumissionView(IDictionary<string,object> parameters):this()
        {
            ViewModel.Soumission = parameters["Soumission"] as Soumission;
            //Si c'est une nouvelle soumission, on affiche le bouton créer au lieu de sauvegarder
            if (ViewModel.Soumission.IdSoumission == null)
            {
                btnCreerSoumission.Visibility = Visibility.Visible;
                if (ViewModel.Soumission.Client == null)
                {
                    Client = new Client();
                    ViewModel.Soumission.Client = Client;
                }
                if (ViewModel.Soumission.ItemsSoumission == null)
                { 
                    //List<ItemSoumission> liste = new List<ItemSoumission>();
                    ViewModel.Soumission.ItemsSoumission = new List<ItemSoumission>();
                }
            }
        }

        private void btnSauvegarder_Click(object sender, RoutedEventArgs e)
        {
            //Si aucun client, on va le chercher dans la base de données par son numéro de téléphone
            if (ViewModel.Soumission.Client == null)
            {
                _clientService = ServiceFactory.Instance.GetService<IClientService>();
                RetrieveClientArgs = new RetrieveClientArgs();
                RetrieveClientArgs.IdClient = Convert.ToInt32(ViewModel.Soumission.Client.IdClient);
                Client = _clientService.Retrieve(RetrieveClientArgs);
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

        private void btnCreer_Click(object sender, RoutedEventArgs e)
        {
            //Vérifier si le client dans la base de données grâce a son numéro de téléphone
            IEmployeService _employeService = ServiceFactory.Instance.GetService<IEmployeService>();
            RetrieveEmployeArgs retrieveEmployeArgs = new RetrieveEmployeArgs();
            retrieveEmployeArgs.IdEmploye = 1;
            Employe = _employeService.Retrieve(retrieveEmployeArgs);
            /*Employe employe = new Employe();
            employe.IdEmploye = 1;*/
            ViewModel.Soumission.Employe = Employe; // Employé Cruise
            
            //On met la date du jour
            DateTime date = new DateTime();
            ViewModel.Soumission.Date = date;

            try
            {
                 //Vérifier si le client dans la base de données grâce a son numéro de téléphone
                _clientService = ServiceFactory.Instance.GetService<IClientService>();
                RetrieveClientArgs = new RetrieveClientArgs();
                //RetrieveClientArgs.Telephone = ViewModel.Soumission.Client.Telephone;
                Client = _clientService.Retrieve(RetrieveClientArgs);
            }
            catch (Exception)
            {
                //Le client n'existe pas
                _provinceService = ServiceFactory.Instance.GetService<IProvinceService>();
                RetrieveProvinceArgs retrieveProvinceArgs = new RetrieveProvinceArgs();
                retrieveProvinceArgs.IdProvince = 24;
                Client.NumeroCivique = "-";
                Client.Province = _provinceService.Retrieve(retrieveProvinceArgs);
                Client.Rue = "-";
                Client.Solde = 0;
                Client.Telephone2 = "-";
                Client.Ville = "-";
                Client.CodePostal = "-";
                //Insérer le client dans la base de données.
                _clientService.Insert(Client);
                //Insérer la soumission dans la base de données.
                ViewModel.InsererCommand();
                labelNumeroSoumission.Content = ViewModel.Soumission.IdSoumission;
                btnCreerSoumission.Visibility = Visibility.Hidden;
                //Changer le titre de la tab
            }
        }
    }
}
