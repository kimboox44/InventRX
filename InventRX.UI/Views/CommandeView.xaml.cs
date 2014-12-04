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
    /// Logique d'interaction pour CommandeView.xaml
    /// </summary>
    public partial class CommandeView : UserControl
    {
        public CommandeViewModel ViewModel { get { return (CommandeViewModel)DataContext; } }

        private IFournisseurService _fournisseurService;
        private IProvinceService _provinceService;
        public RetrieveFournisseurArgs RetrieveFournisseurArgs { get; set; }
        public Fournisseur Fournisseur { get; set; }
        public Employe Employe { get; set; }

        public CommandeView()
        {
            InitializeComponent();
            DataContext = new CommandeViewModel();
        }

        public CommandeView(IDictionary<string, object> parameters)
            : this()
        {
            btnSauvegarderCommande.IsEnabled = false;

            ViewModel.Commande = parameters["Commande"] as Commande;
            //Si c'est une nouvelle Commande, on affiche le bouton créer au lieu de sauvegarder
            if (ViewModel.Commande.IdCommande == null)
            {
                btnCreerCommande.Visibility = Visibility.Visible;
                if (ViewModel.Commande.Fournisseur == null)
                {
                    Fournisseur = new Fournisseur();
                    ViewModel.Commande.Fournisseur = Fournisseur;
                }
                if (ViewModel.Commande.ItemsCommande == null)
                {
                    //List<ItemCommande> liste = new List<ItemCommande>();
                    ViewModel.Commande.ItemsCommande = new List<ItemCommande>();
                }
            }

        }

        private void btnSauvegarder_Click(object sender, RoutedEventArgs e)
        {
            btnSauvegarderCommande.IsEnabled = false;
            //Si aucun client, on va le chercher dans la base de données par son numéro de téléphone
            if (ViewModel.Commande.Fournisseur == null)
            {
                _fournisseurService = ServiceFactory.Instance.GetService<IFournisseurService>();
                RetrieveFournisseurArgs = new RetrieveFournisseurArgs();
                RetrieveFournisseurArgs.IdFournisseur = Convert.ToInt32(ViewModel.Commande.Fournisseur.IdFournisseur);
                Fournisseur = _fournisseurService.Retrieve(RetrieveFournisseurArgs);
            }
            if (comboboxEtatCommande.SelectedItem.ToString() == "")
            {
                comboboxEtatCommande.SelectedItem = "En attente";
            }
            ViewModel.Commande.Etat = comboboxEtatCommande.SelectedItem.ToString();
            ViewModel.SauvegarderCommand();
        }

        private void datagridListeProduits_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnSauvegarderCommande.IsEnabled = true;
            bool exist = false;
            Produit produitSelectionne = (datagridListeProduits.SelectedItem as Produit);

            if (produitSelectionne != null)
            {
                //Si le produit existe déjà dans les produits de la Commande, on incrémente de 1 la quantité de celui-ci
                foreach (ItemCommande item in ViewModel.Commande.ItemsCommande)
                {
                    if (item.Produit.IdProduit == produitSelectionne.IdProduit)
                    {
                        item.Quantite = item.Quantite + 1;
                        exist = true;
                        break;
                    }
                }
                //Si le produit n'existe pas dans les produits de la Commande, on l'ajoute
                if (exist == false)
                {
                    ItemCommande newItemCommande = new ItemCommande();
                    newItemCommande.Commande = ViewModel.Commande;
                    newItemCommande.Produit = produitSelectionne;
                    newItemCommande.NomProduit = produitSelectionne.Nom;
                    newItemCommande.Quantite = 1;
                    newItemCommande.PrixUnitaire = produitSelectionne.Prix;
                    if (datagridListeItemsCommande.ItemsSource != null)
                    {
                        ViewModel.Commande.ItemsCommande.Add(newItemCommande);
                    }
                }
                datagridListeItemsCommande.Items.Refresh();
            }
        }

        private void btnSupprimerItem_Click(object sender, RoutedEventArgs e)
        {
            btnSauvegarderCommande.IsEnabled = true;
            ItemCommande item = (ItemCommande)((sender as Button).CommandParameter);
            if (item != null)
            {
                if (datagridListeItemsCommande.ItemsSource != null)
                {
                    ViewModel.Commande.ItemsCommande.Remove(item);
                    datagridListeItemsCommande.Items.Refresh();
                }
            }
        }

        private void btnCreer_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxClientNom.Text == "" || txtBoxClientTelephone.Text == "")
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
                ViewModel.Commande.Employe = Employe; // Employé Cruise

                //On met la date du jour
                DateTime date = new DateTime();
                ViewModel.Commande.Date = date;

                _fournisseurService = ServiceFactory.Instance.GetService<IFournisseurService>();
                try
                {
                    //Vérifier si le client dans la base de données grâce a son numéro de téléphone
                    RetrieveFournisseurArgs = new RetrieveFournisseurArgs();
                    RetrieveFournisseurArgs.Telephone = ViewModel.Commande.Fournisseur.Telephone;
                    //RetrieveClientArgs.Telephone = ViewModel.Commande.Client.Telephone;

                    Fournisseur fournisseur = _fournisseurService.Retrieve(RetrieveFournisseurArgs);
                    //Client = _clientService.Retrieve(RetrieveClientArgs);
                    if (fournisseur != null && fournisseur.IdFournisseur != null)
                    {
                        Fournisseur = fournisseur;
                        ViewModel.Commande.Fournisseur = Fournisseur;
                        ViewModel.InsererCommand();
                        labelNumeroCommande.Content = ViewModel.Commande.IdCommande;
                        btnCreerCommande.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        throw new Exception("Commande non trouvé");
                    }
                }
                catch (Exception)
                {
                    //Le client n'existe pas
                    _provinceService = ServiceFactory.Instance.GetService<IProvinceService>();
                    RetrieveProvinceArgs retrieveProvinceArgs = new RetrieveProvinceArgs();
                    retrieveProvinceArgs.IdProvince = 11;
                    Fournisseur.NumeroCivique = "-";
                    Fournisseur.Province = _provinceService.Retrieve(retrieveProvinceArgs);
                    Fournisseur.Rue = "-";
                    Fournisseur.Ville = "-";
                    Fournisseur.CodePostal = "-";
                    //Insérer le client dans la base de données.
                    _fournisseurService.Insert(Fournisseur);
                    //Insérer la Commande dans la base de données.
                    ViewModel.InsererCommand();
                    labelNumeroCommande.Content = ViewModel.Commande.IdCommande;
                    btnCreerCommande.Visibility = Visibility.Hidden;
                    //Changer le titre de la tab
                }
            }
        }

        private void buttonQuitter_Click(object sender, RoutedEventArgs e)
        {
            if (btnSauvegarderCommande.IsEnabled == true)
            {
                if (MessageBox.Show("Êtes-vous sûr de vouloir fermer l'onglet commande sans sauvegarder les modifications?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    ViewModel.CloseCommand();
                }
            }
            else
            {
                ViewModel.CloseCommand();
            }
        }
    }
}
