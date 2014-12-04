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

        public SoumissionView(IDictionary<string, object> parameters)
            : this()
        {
            btnSauvegarderSoumission.IsEnabled = false;

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

            double sousTotal = 0;
            double sousTotalTPS;
            double sousTotalTVQ;
            double total;
            const double TPS = 5;
            const double TVQ = 9.975;

            foreach (ItemSoumission item in ViewModel.Soumission.ItemsSoumission)
            {
                sousTotal += item.PrixUnitaire * item.Quantite;
            }

            sousTotalTPS = Math.Round(sousTotal * (TPS / 100), 2);
            sousTotalTVQ = Math.Round(sousTotal * (TVQ / 100), 2);
            total = (sousTotal + sousTotalTPS + sousTotalTVQ);

            labelSousTotal.Content = sousTotal.ToString("C2");
            labelTPS.Content = sousTotalTPS.ToString("C2");
            labelTVQ.Content = sousTotalTVQ.ToString("C2");
            labelTotal.Content = total.ToString("C2");
        }

        private void btnSauvegarder_Click(object sender, RoutedEventArgs e)
        {
            btnSauvegarderSoumission.IsEnabled = false;

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
            btnSauvegarderSoumission.IsEnabled = true;
            bool exist = false;
            Produit produitSelectionne = (datagridListeProduits.SelectedItem as Produit);

            if (produitSelectionne != null)
            {
                //Si le produit existe déjà dans les produits de la soumission, on incrémente de 1 la quantité de celui-ci
                foreach (ItemSoumission item in ViewModel.Soumission.ItemsSoumission)
                {
                    if (item.Produit.IdProduit == produitSelectionne.IdProduit)
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

            double sousTotal = 0;
            double sousTotalTPS;
            double sousTotalTVQ;
            double total;
            const double TPS = 5;
            const double TVQ = 9.975;

            foreach (ItemSoumission item in ViewModel.Soumission.ItemsSoumission)
            {
                sousTotal += item.PrixUnitaire * item.Quantite;
            }

            sousTotalTPS = Math.Round(sousTotal * (TPS / 100), 2);
            sousTotalTVQ = Math.Round(sousTotal * (TVQ / 100), 2);
            total = (sousTotal + sousTotalTPS + sousTotalTVQ);

            labelSousTotal.Content = sousTotal.ToString("C2");
            labelTPS.Content = sousTotalTPS.ToString("C2");
            labelTVQ.Content = sousTotalTVQ.ToString("C2");
            labelTotal.Content = total.ToString("C2");
        }

        private void btnSupprimerItem_Click(object sender, RoutedEventArgs e)
        {
            btnSauvegarderSoumission.IsEnabled = true;
            ItemSoumission item = (ItemSoumission)((sender as Button).CommandParameter);
            if (item != null)
            {
                if (datagridListeItemsSoumission.ItemsSource != null)
                {
                    ViewModel.Soumission.ItemsSoumission.Remove(item);
                    datagridListeItemsSoumission.Items.Refresh();
                }
            }

            double sousTotal = 0;
            double sousTotalTPS;
            double sousTotalTVQ;
            double total;
            const double TPS = 5;
            const double TVQ = 9.975;

            foreach (ItemSoumission itemS in ViewModel.Soumission.ItemsSoumission)
            {
                sousTotal += itemS.PrixUnitaire * itemS.Quantite;
            }

            sousTotalTPS = Math.Round(sousTotal * (TPS / 100), 2);
            sousTotalTVQ = Math.Round(sousTotal * (TVQ / 100), 2);
            total = (sousTotal + sousTotalTPS + sousTotalTVQ);

            labelSousTotal.Content = sousTotal.ToString("C2");
            labelTPS.Content = sousTotalTPS.ToString("C2");
            labelTVQ.Content = sousTotalTVQ.ToString("C2");
            labelTotal.Content = total.ToString("C2");
        }

        private void btnCreer_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxClientNom.Text == "" || txtBoxClientPrenom.Text == "" || txtBoxClientTelephone.Text == "")
            {
                MessageBox.Show("Certains champs du client sont manquants.");
            }
            else
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
                    //Pour le moment on créér un nouveau client si le numéro ne match pas dans la BD
                    _clientService = ServiceFactory.Instance.GetService<IClientService>();
                    RetrieveClientArgs = new RetrieveClientArgs();
                    RetrieveClientArgs.Telephone = ViewModel.Soumission.Client.Telephone;
                    Client client = _clientService.RetrieveByPhone(RetrieveClientArgs);
                    //Client = _clientService.Retrieve(RetrieveClientArgs);
                    if (client != null && client.IdClient != null)
                    {
                        Client = client;
                        ViewModel.Soumission.Client = Client;
                        ViewModel.InsererCommand();
                        labelNumeroSoumission.Content = ViewModel.Soumission.IdSoumission;
                        btnCreerSoumission.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        throw new Exception("Client non trouvé");
                    }
                }
                catch (Exception)
                {
                    //Le client n'existe pas
                    _provinceService = ServiceFactory.Instance.GetService<IProvinceService>();
                    RetrieveProvinceArgs retrieveProvinceArgs = new RetrieveProvinceArgs();
                    retrieveProvinceArgs.Abreviation = "QC";
                    Province province = _provinceService.RetrieveByAbreviation(retrieveProvinceArgs);
                    Client.NumeroCivique = "-";
                    Client.Province = province;
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

        private void buttonQuitter_Click(object sender, RoutedEventArgs e)
        {
            if (btnSauvegarderSoumission.IsEnabled == true)
            {
                if (MessageBox.Show("Êtes-vous sûr de vouloir fermer l'onglet soumission sans sauvegarder les modifications?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    ViewModel.CloseCommand();
                }
            }
            else
            {
                ViewModel.CloseCommand();
            }
        }

        private void datagridListeItemsSoumission_CurrentCellChanged(object sender, EventArgs e)
        {
            double sousTotal = 0;
            double sousTotalTPS;
            double sousTotalTVQ;
            double total;
            const double TPS = 5;
            const double TVQ = 9.975;

            foreach (ItemSoumission itemS in ViewModel.Soumission.ItemsSoumission)
            {
                sousTotal += itemS.PrixUnitaire * itemS.Quantite;
            }

            sousTotalTPS = Math.Round(sousTotal * (TPS / 100), 2);
            sousTotalTVQ = Math.Round(sousTotal * (TVQ / 100), 2);
            total = (sousTotal + sousTotalTPS + sousTotalTVQ);

            labelSousTotal.Content = sousTotal.ToString("C2");
            labelTPS.Content = sousTotalTPS.ToString("C2");
            labelTVQ.Content = sousTotalTVQ.ToString("C2");
            labelTotal.Content = total.ToString("C2");
        }
    }
}
