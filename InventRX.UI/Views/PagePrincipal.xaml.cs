using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Cstj.MvvmToolkit.Services;
using InventRX.Logic.Model.Args;
using InventRX.Logic.Model.Entities;
using InventRX.Logic.Services.NHibernate;
using InventRX.Services.Definitions;
using InventRX.UI.ViewModel;

namespace InventRX.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour PagePrincipal.xaml
    /// </summary>
    public partial class PagePrincipal : Page
    {
        public static List<MethodePaiement> ListeMethodesPaiement = new List<MethodePaiement>();
        private Soumission ActiveSoumission { get; set; }

        public PagePrincipal()
        {
            InitializeComponent();
            ChargerSoumissions();
            ChargerClients();
            ChargerCommandes();
            ChargerFactures();

            //Charge la liste des méthodes de paiement
            foreach (string smp in Properties.Settings.Default.MethodesPaiement)
            {
                MethodePaiement mp = new MethodePaiement();
                mp.Nom = smp;
                ListeMethodesPaiement.Add(mp);
            }
            comboboxMethodePaiement.ItemsSource = ListeMethodesPaiement;
        }

        #region Soumission

        private ISoumissionService _soumissionService;
        public RetrieveSoumissionArgs RetrieveSoumissionArgs { get; set; }
        public IList<Soumission> ListeSoumissions { get; set; }

        public RetrieveSoumissionArgs RetrieveRechercheSoumissionArgs = new RetrieveSoumissionArgs();

        private void ChargerSoumissions()
        {
            //Charge la liste de toutes les soumissions
            _soumissionService = ServiceFactory.Instance.GetService<ISoumissionService>();
            RetrieveSoumissionArgs = new RetrieveSoumissionArgs();
            ListeSoumissions = _soumissionService.RetrieveAll();
            datagridListeSoumissions.ItemsSource = ListeSoumissions;
        }

        private void datagridListeSoumissions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            bool exist = false;
            Soumission soumissionSelectionnee = (datagridListeSoumissions.SelectedItem as Soumission);

            if (soumissionSelectionnee != null)
            {
                //Desélectionne la soumission
                datagridListeSoumissions.SelectedItem = null;

                //Si la soumission est déjà ouverte dans une tab, on focus dessus
                foreach (TabItem tab in TabControlPrincipalDetails.Items)
                {
                    if (tab.Header.ToString() == ("Soumission #" + soumissionSelectionnee.IdSoumission).ToString())
                    {
                        TabControlPrincipalDetails.SelectedItem = tab;
                        exist = true;
                        break;
                    }
                }

                //Si la soumission n'est pas déjà ouverte, on l'ouvre
                if (exist == false)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>() { { "Soumission", soumissionSelectionnee } };

                    MainViewModel nouveauViewModel = new MainViewModel();
                    nouveauViewModel.CurrentView = new SoumissionView(parameters);

                    ContentPresenter contentPresenter = new ContentPresenter();

                    Binding myBinding = new Binding("soumission" + soumissionSelectionnee.IdSoumission + "Data");
                    myBinding.Source = nouveauViewModel.CurrentView;
                    contentPresenter.Content = myBinding.Source;

                    //Ajout du cont
                    ScrollViewer newScrollViewer = new ScrollViewer();
                    newScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                    newScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                    newScrollViewer.Content = contentPresenter;

                    //Création d'un nouveau item
                    TabItem nouvelleTab = new TabItem();
                    nouvelleTab.Header = "Soumission #" + soumissionSelectionnee.IdSoumission;
                    nouvelleTab.Content = newScrollViewer;

                    //Sans scrollviewer
                    //nouvelleTab.Content = contentPresenter;

                    nouvelleTab.DataContext = nouveauViewModel;
                    //nouvelleTab.Background = Brushes.LightSkyBlue;
                    nouvelleTab.BorderBrush = Brushes.LightSkyBlue;

                    //Ajout de l'item à la tab control
                    TabControlPrincipalDetails.Items.Add(nouvelleTab);
                    TabControlPrincipalDetails.SelectedItem = nouvelleTab;
                }
            }
        }

        private void boutonNouvelleSoumission_Click(object sender, RoutedEventArgs e)
        {
            Soumission newSoumission = new Soumission();
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "Soumission", newSoumission } };

            MainViewModel nouveauViewModel = new MainViewModel();
            nouveauViewModel.CurrentView = new SoumissionView(parameters);

            ContentPresenter contentPresenter = new ContentPresenter();

            Binding myBinding = new Binding("soumission" + newSoumission.IdSoumission + "Data");
            myBinding.Source = nouveauViewModel.CurrentView;
            contentPresenter.Content = myBinding.Source;

            //Ajout du contentPresenter dans un scrollviewer pour pouvoir scroller à l'interieur
            ScrollViewer newScrollViewer = new ScrollViewer();
            newScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            newScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            newScrollViewer.Content = contentPresenter;

            //Création d'un nouveau item
            TabItem nouvelleTab = new TabItem();
            nouvelleTab.Header = "Nouvelle soumission";
            nouvelleTab.Content = newScrollViewer;

            //Sans scrollviewer
            //nouvelleTab.Content = contentPresenter;

            nouvelleTab.DataContext = nouveauViewModel;
            //nouvelleTab.Background = Brushes.LightSkyBlue;
            nouvelleTab.BorderBrush = Brushes.LightSkyBlue;

            //Ajout de l'item à la tab control
            TabControlPrincipalDetails.Items.Add(nouvelleTab);
            TabControlPrincipalDetails.SelectedItem = nouvelleTab;
        }

        #endregion

        #region Commande
        private ICommandeService _commandeService;
        public RetrieveCommandeArgs RetrieveCommandeArgs { get; set; }
        public IList<Commande> ListeCommandes { get; set; }

        private void ChargerCommandes()
        {
            //Charge la liste de toutes les Commandes
            _commandeService = ServiceFactory.Instance.GetService<ICommandeService>();
            RetrieveCommandeArgs = new RetrieveCommandeArgs();
            ListeCommandes = _commandeService.RetrieveAll();
            datagridListeCommandes.ItemsSource = ListeCommandes;
        }

        private void datagridListeCommandes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            bool exist = false;
            Commande commandeSelectionnee = (datagridListeCommandes.SelectedItem as Commande);

            if (commandeSelectionnee != null)
            {
                //Desélectionne la Commande
                datagridListeCommandes.SelectedItem = null;

                //Si la Commande est déjà ouverte dans une tab, on focus dessus
                foreach (TabItem tab in TabControlPrincipalDetails.Items)
                {
                    if (tab.Header.ToString() == ("Commande #" + commandeSelectionnee.IdCommande).ToString())
                    {
                        TabControlPrincipalDetails.SelectedItem = tab;
                        exist = true;
                        break;
                    }
                }

                //Si la Commande n'est pas déjà ouverte, on l'ouvre
                if (exist == false)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>() { { "Commande", commandeSelectionnee } };

                    MainViewModel nouveauViewModel = new MainViewModel();
                    nouveauViewModel.CurrentView = new CommandeView(parameters);

                    ContentPresenter contentPresenter = new ContentPresenter();

                    Binding myBinding = new Binding("commande" + commandeSelectionnee.IdCommande + "Data");
                    myBinding.Source = nouveauViewModel.CurrentView;
                    contentPresenter.Content = myBinding.Source;

                    //Ajout du cont
                    ScrollViewer newScrollViewer = new ScrollViewer();
                    newScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                    newScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                    newScrollViewer.Content = contentPresenter;

                    //Création d'un nouveau item
                    TabItem nouvelleTab = new TabItem();
                    nouvelleTab.Header = "Commande #" + commandeSelectionnee.IdCommande;
                    nouvelleTab.Content = newScrollViewer;

                    //Sans scrollviewer
                    //nouvelleTab.Content = contentPresenter;

                    nouvelleTab.DataContext = nouveauViewModel;
                    //nouvelleTab.Background = Brushes.LightGreen;
                    nouvelleTab.BorderBrush = Brushes.LightGreen;

                    //Ajout de l'item à la tab control
                    TabControlPrincipalDetails.Items.Add(nouvelleTab);
                    TabControlPrincipalDetails.SelectedItem = nouvelleTab;
                }
            }
        }

        private void boutonNouvelleCommande_Click(object sender, RoutedEventArgs e)
        {
            Commande newCommande = new Commande();
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "Commande", newCommande } };

            MainViewModel nouveauViewModel = new MainViewModel();
            nouveauViewModel.CurrentView = new CommandeView(parameters);

            ContentPresenter contentPresenter = new ContentPresenter();

            Binding myBinding = new Binding("commande" + newCommande.IdCommande + "Data");
            myBinding.Source = nouveauViewModel.CurrentView;
            contentPresenter.Content = myBinding.Source;

            //Ajout du contentPresenter dans un scrollviewer pour pouvoir scroller à l'interieur
            ScrollViewer newScrollViewer = new ScrollViewer();
            newScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            newScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            newScrollViewer.Content = contentPresenter;

            //Création d'un nouveau item
            TabItem nouvelleTab = new TabItem();
            nouvelleTab.Header = "Nouvelle Commande";
            nouvelleTab.Content = newScrollViewer;

            //Sans scrollviewer
            //nouvelleTab.Content = contentPresenter;

            nouvelleTab.DataContext = nouveauViewModel;
            //nouvelleTab.Background = Brushes.LightGreen;
            nouvelleTab.BorderBrush = Brushes.LightGreen;

            //Ajout de l'item à la tab control
            TabControlPrincipalDetails.Items.Add(nouvelleTab);
            TabControlPrincipalDetails.SelectedItem = nouvelleTab;
        }

        #endregion

        #region Clients

        private IClientService _clientService;
        public RetrieveClientArgs RetrieveClientArgs { get; set; }
        public IList<Client> ListeClients { get; set; }

        private void ChargerClients()
        {
            //Charge la liste de tous les clients
            _clientService = ServiceFactory.Instance.GetService<IClientService>();
            RetrieveClientArgs = new RetrieveClientArgs();
            ListeClients = _clientService.RetrieveAll();
            datagridListeClients.ItemsSource = ListeClients;
        }

        private void datagridListeClients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //TODO : Gerer si la tabulation est deja ouverte pour ce client
            Client clientSelectionnee = (datagridListeClients.SelectedItem as Client);
            if (clientSelectionnee != null)
            {
                datagridListeClients.SelectedItem = null;

            }
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "Client", clientSelectionnee } };
            //TODO : Enlever la creation de new mainviewmodel et client view
            MainViewModel nouveauViewModel = new MainViewModel();
            nouveauViewModel.CurrentView = new ClientView(parameters);
            ContentPresenter contentPresenter = new ContentPresenter();

            Binding myBinding = new Binding("client" + clientSelectionnee.IdClient + "Data");
            myBinding.Source = nouveauViewModel.CurrentView;
            contentPresenter.Content = myBinding.Source;

            //Ajout du contentPresenter dans un scrollviewer pour pouvoir scroller à l'interieur
            ScrollViewer newScrollViewer = new ScrollViewer();
            newScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            newScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            newScrollViewer.Content = contentPresenter;

            //Création d'un nouveau item
            TabItem nouvelleTab = new TabItem();
            nouvelleTab.Header = "client #" + clientSelectionnee.IdClient;
            nouvelleTab.Content = newScrollViewer;

            //Sans scrollviewer
            //nouvelleTab.Content = contentPresenter;

            nouvelleTab.DataContext = nouveauViewModel;
            nouvelleTab.BorderBrush = Brushes.Orange;

            //Ajout de l'item à la tab control
            TabControlPrincipalDetails.Items.Add(nouvelleTab);
            TabControlPrincipalDetails.SelectedItem = nouvelleTab;
        }

        private void btnNouveauClient_Click(object sender, RoutedEventArgs e)
        {
            Client newClient = new Client();
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "Client", newClient } };

            MainViewModel nouveauViewModel = new MainViewModel();
            nouveauViewModel.CurrentView = new ClientView(parameters);

            ContentPresenter contentPresenter = new ContentPresenter();

            Binding myBinding = new Binding("client" + newClient.IdClient + "Data");
            myBinding.Source = nouveauViewModel.CurrentView;
            contentPresenter.Content = myBinding.Source;

            //Ajout du contentPresenter dans un scrollviewer pour pouvoir scroller à l'interieur
            ScrollViewer newScrollViewer = new ScrollViewer();
            newScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            newScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            newScrollViewer.Content = contentPresenter;

            //Création d'un nouveau item
            TabItem nouvelleTab = new TabItem();
            nouvelleTab.Header = "Nouveau Client";
            nouvelleTab.Content = newScrollViewer;

            //Sans scrollviewer
            //nouvelleTab.Content = contentPresenter;

            nouvelleTab.DataContext = nouveauViewModel;
            nouvelleTab.BorderBrush = Brushes.Orange;

            //Ajout de l'item à la tab control
            TabControlPrincipalDetails.Items.Add(nouvelleTab);
            TabControlPrincipalDetails.SelectedItem = nouvelleTab;
        }


        #endregion

        #region Factures



        private IFactureService _factureService;
        public RetrieveFactureArgs RetrieveFactureArgs { get; set; }
        public IList<Facture> ListeFactures { get; set; }

        public RetrieveFactureArgs RetrieveRechercheFactureArgs = new RetrieveFactureArgs();

        private void ChargerFactures()
        {
            //Charge la liste de toutes les Factures
            _factureService = ServiceFactory.Instance.GetService<IFactureService>();
            RetrieveFactureArgs = new RetrieveFactureArgs();
            ListeFactures = _factureService.RetrieveAll();
            datagridListeFactures.ItemsSource = ListeFactures;
        }


        private void datagridListeFactures_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            bool exist = false;
            Facture factureSelectionnee = (datagridListeFactures.SelectedItem as Facture);

            if (factureSelectionnee != null)
            {
                //Desélectionne la facture
                datagridListeFactures.SelectedItem = null;

                //Si la Facture est déjà ouverte dans une tab, on focus dessus
                foreach (TabItem tab in TabControlPrincipalDetails.Items)
                {
                    if (tab.Header.ToString() == ("Facture #" + factureSelectionnee.IdFacture).ToString())
                    {
                        TabControlPrincipalDetails.SelectedItem = tab;
                        exist = true;
                        break;
                    }
                }

                //Si la facture n'est pas déjà ouverte, on l'ouvre
                if (exist == false)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>() { { "Facture", factureSelectionnee } };

                    MainViewModel nouveauViewModel = new MainViewModel();
                    nouveauViewModel.CurrentView = new FactureView(parameters);

                    ContentPresenter contentPresenter = new ContentPresenter();

                    Binding myBinding = new Binding("facture" + factureSelectionnee.IdFacture + "Data");
                    myBinding.Source = nouveauViewModel.CurrentView;
                    contentPresenter.Content = myBinding.Source;

                    //Ajout du cont
                    ScrollViewer newScrollViewer = new ScrollViewer();
                    newScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                    newScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                    newScrollViewer.Content = contentPresenter;

                    //Création d'un nouveau item
                    TabItem nouvelleTab = new TabItem();
                    nouvelleTab.Header = "Facture #" + factureSelectionnee.IdFacture;
                    nouvelleTab.Content = newScrollViewer;

                    //Sans scrollviewer
                    //nouvelleTab.Content = contentPresenter;

                    nouvelleTab.DataContext = nouveauViewModel;
                    //nouvelleTab.Background = Brushes.LightSkyBlue;
                    nouvelleTab.BorderBrush = Brushes.BlueViolet;

                    //Ajout de l'item à la tab control
                    TabControlPrincipalDetails.Items.Add(nouvelleTab);
                    TabControlPrincipalDetails.SelectedItem = nouvelleTab;
                }
            }
        }


        private void boutonNouvelleFacture_Click(object sender, RoutedEventArgs e)
        {
            Facture newFacture = new Facture();
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "Facture", newFacture } };

            MainViewModel nouveauViewModel = new MainViewModel();
            nouveauViewModel.CurrentView = new FactureView(parameters);

            ContentPresenter contentPresenter = new ContentPresenter();

            Binding myBinding = new Binding("Facture" + newFacture.IdFacture + "Data");
            myBinding.Source = nouveauViewModel.CurrentView;
            contentPresenter.Content = myBinding.Source;

            //Ajout du contentPresenter dans un scrollviewer pour pouvoir scroller à l'interieur
            ScrollViewer newScrollViewer = new ScrollViewer();
            newScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            newScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            newScrollViewer.Content = contentPresenter;

            //Création d'un nouveau item
            TabItem nouvelleTab = new TabItem();
            nouvelleTab.Header = "Nouvelle Facture";
            nouvelleTab.Content = newScrollViewer;

            //Sans scrollviewer
            //nouvelleTab.Content = contentPresenter;

            nouvelleTab.DataContext = nouveauViewModel;
            //nouvelleTab.Background = Brushes.LightSkyBlue;
            nouvelleTab.BorderBrush = Brushes.LightSkyBlue;

            //Ajout de l'item à la tab control
            TabControlPrincipalDetails.Items.Add(nouvelleTab);
            TabControlPrincipalDetails.SelectedItem = nouvelleTab;
        }


        #endregion

        #region Tabs Config
        /*
         * http://social.msdn.microsoft.com/Forums/vstudio/en-US/ed077477-a742-4c3d-bd4e-3efdd5dd6ba2/dragdrop-tabitem?forum=wpf
         * Pour déplacer les tabs dans l'ordre que l'on veut.
         */
        private void TabItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var tabItem = e.Source as TabItem;

            if (tabItem == null)
                return;

            if (Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(tabItem, tabItem, DragDropEffects.All);
            }
        }

        private void TabItem_Drop(object sender, DragEventArgs e)
        {
            var tabItemTarget = e.Source as TabItem;

            var tabItemSource = e.Data.GetData(typeof(TabItem)) as TabItem;

            if (!tabItemTarget.Equals(tabItemSource))
            {
                var tabControl = tabItemTarget.Parent as TabControl;
                int sourceIndex = tabControl.Items.IndexOf(tabItemSource);
                int targetIndex = tabControl.Items.IndexOf(tabItemTarget);

                tabControl.Items.Remove(tabItemSource);
                tabControl.Items.Insert(targetIndex, tabItemSource);

                tabControl.Items.Remove(tabItemTarget);
                tabControl.Items.Insert(sourceIndex, tabItemTarget);
            }
        }
        #endregion

        private void btnRechercherSoumission_Click(object sender, RoutedEventArgs e)
        {
            datagridListeSoumissions.ItemsSource = null;
            if (RetrieveRechercheSoumissionArgs == null)
            {
                RetrieveRechercheSoumissionArgs = new RetrieveSoumissionArgs();
            }
            if (RetrieveRechercheSoumissionArgs.Client == null)
            {
                RetrieveRechercheSoumissionArgs.Client = new Client();
            }
            if (textboxNumeroSoumission.Text == "")
            {
                RetrieveRechercheSoumissionArgs.IdSoumission = 0;
            }
            else
            {
                int errorCounter = 0;
                errorCounter = Regex.Matches(textboxNumeroSoumission.Text, @"[a-zA-Z]").Count;
                if (errorCounter <= 0)
                {
                    int numero;
                    string snumero = textboxNumeroSoumission.Text;
                    if (int.TryParse(snumero, out numero))
                    {
                        RetrieveRechercheSoumissionArgs.IdSoumission = numero;
                    }
                }
                else
                {
                    RetrieveRechercheSoumissionArgs.IdSoumission = 0;
                }
            }

            if (textboxTelephoneClientSoumission.Text != "Téléphone" && textboxTelephoneClientSoumission.Text != "")
            {
                RetrieveRechercheSoumissionArgs.Client.Telephone = textboxTelephoneClientSoumission.Text;
            }
            else
            {
                RetrieveRechercheSoumissionArgs.Client.Telephone = null;
            }

            if (textboxNomClientSoumission.Text != "Nom" && textboxNomClientSoumission.Text != "")
            {
                RetrieveRechercheSoumissionArgs.Client.Nom = textboxNomClientSoumission.Text;
            }
            else
            {
                RetrieveRechercheSoumissionArgs.Client.Nom = null;
            }

            if (textboxPrenomClientSoumission.Text != "Prénom" && textboxPrenomClientSoumission.Text != "")
            {
                RetrieveRechercheSoumissionArgs.Client.Prenom = textboxPrenomClientSoumission.Text;
            }
            else
            {
                RetrieveRechercheSoumissionArgs.Client.Prenom = null;
            }

            if (checkboxSoumissionDateDu.IsChecked == true)
            {
                if (datepickerSoumissionDu.ToString() != "")
                {
                    RetrieveRechercheSoumissionArgs.DateDebut = datepickerSoumissionDu.SelectedDate;
                }
                else
                {
                    RetrieveRechercheSoumissionArgs.DateDebut = null;
                }
            }
            else
            {
                RetrieveRechercheSoumissionArgs.DateDebut = null;
            }

            if (checkboxSoumissionDateAu.IsChecked == true)
            {
                if (datepickerSoumissionAu.ToString() != "")
                {
                    RetrieveRechercheSoumissionArgs.DateFin = datepickerSoumissionAu.SelectedDate;
                }
                else
                {
                    RetrieveRechercheSoumissionArgs.DateFin = null;
                }
            }
            else
            {
                RetrieveRechercheSoumissionArgs.DateFin = null;
            }

            //MessageBox.Show("From: " + datepickerSoumissionDu.ToString() + " To: " + datepickerSoumissionAu.ToString());
            ListeSoumissions = _soumissionService.RetrieveBy(RetrieveRechercheSoumissionArgs);
            datagridListeSoumissions.ItemsSource = ListeSoumissions;
        }

        private void btnResetRechercherSoumission_Click(object sender, RoutedEventArgs e)
        {
            datagridListeSoumissions.ItemsSource = null;

            textboxNumeroSoumission.Clear();
            textboxNomClientSoumission.Clear();
            textboxTelephoneClientSoumission.Clear();
            textboxPrenomClientSoumission.Clear();
            datepickerSoumissionDu.SelectedDate = null;
            datepickerSoumissionAu.SelectedDate = null;
            checkboxSoumissionDateDu.IsChecked = false;
            checkboxSoumissionDateAu.IsChecked = false;

            ListeSoumissions = _soumissionService.RetrieveAll();
            datagridListeSoumissions.ItemsSource = ListeSoumissions;
        }

        private Soumission FindActiveSoumission()
        {
            Soumission soumission;
            TabItem item = TabControlPrincipalDetails.SelectedItem as TabItem;
            if (item != null && item.Header.ToString() != "+")
            {
                ScrollViewer s = item.Content as ScrollViewer;
                ContentPresenter c = s.Content as ContentPresenter;
                Binding b = new Binding();
                b.Source = c.Content;

                switch (b.Source.ToString())
                {
                    case "InventRX.UI.Views.SoumissionView":
                        {
                            datagridPaiement.IsEnabled = true;
                            SoumissionView sv = b.Source as SoumissionView;
                            soumission = sv.ViewModel.Soumission as Soumission;
                            if (soumission.IdSoumission == null)
                            {
                                soumission = null;
                            }
                            else
                            {
                                //Prépare la facture en vue de la commit plus tard
                                soumission.ConstruireFacture();
                            }
                            break;
                        }
                    default:
                        {
                            soumission = null;
                            break;
                        }
                }
            }
            else
            {
                soumission = null;
            }
            return soumission;
        }

        private void UpdateSoumissionCaisse(Soumission soumission)
        {
            //Sélectionner la tab caisse
            foreach (TabItem ti in TabControlPrincipalMC.Items)
            {
                if (ti.Header.ToString() == "Caisse")
                {
                    TabControlPrincipalMC.SelectedItem = ti;
                    break;
                }
            }

            double totalCaisse = 0;
            foreach (Paiement paiement in ActiveSoumission.Facture.Paiements)
            {
                totalCaisse += paiement.Montant;
            }
            labelTabCaisseTotal.Content = totalCaisse.ToString("C2") + "/" + soumission.Price().ToString("C2");

            if (totalCaisse != soumission.Price())
            {
                if (totalCaisse < soumission.Price())
                {
                    labelTabCaisseTotalRestant.Foreground = Brushes.Red;
                    labelTabCaisseTotalRestant.Content = "-";
                }
                else
                {
                    labelTabCaisseTotalRestant.Foreground = Brushes.Green;
                    labelTabCaisseTotalRestant.Content = "+";
                }
                labelTabCaisseTotalRestant.Content = labelTabCaisseTotalRestant.Content + Math.Abs(totalCaisse - soumission.Price()).ToString("C2");
                labelTabCaisseTotalRestant.Visibility = Visibility.Visible;
            }
            else
            {
                labelTabCaisseTotalRestant.Content = "";
                labelTabCaisseTotalRestant.Visibility = Visibility.Hidden;
            }
            datagridPaiement.ItemsSource = ActiveSoumission.Facture.Paiements;

            //datagridPaiement.Items.Refresh();
        }

        private void TabControlPrincipalDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActiveSoumission = FindActiveSoumission();
            TabItem tabCaisse = null;

            foreach (TabItem ti in TabControlPrincipalMC.Items)
            {
                if (ti.Header.ToString() == "Caisse")
                {
                    tabCaisse = ti;
                    break;
                }
            }

            if (ActiveSoumission != null)
            {
                UpdateSoumissionCaisse(ActiveSoumission);
                if (tabCaisse != null)
                {
                    TabControlPrincipalMC.SelectedItem = tabCaisse;
                    datagridPaiement.IsEnabled = true;
                    buttonCaissePayer.IsEnabled = true;
                    buttonCaisseAnnuler.IsEnabled = true;
                }
            }
            else
            {
                if (tabCaisse != null)
                {
                    datagridPaiement.IsEnabled = false;
                    buttonCaissePayer.IsEnabled = false;
                    buttonCaisseAnnuler.IsEnabled = false;
                    labelTabCaisseTotal.Content = (0.00).ToString("C2");
                }
            }
        }

        private void btnSupprimerPaiementCaisse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Paiement paiement = (Paiement)((sender as Button).CommandParameter);
                if (paiement != null)
                {
                    if (datagridPaiement.ItemsSource != null)
                    {
                        ActiveSoumission.Facture.Paiements.Remove(paiement);
                        datagridPaiement.Items.Refresh();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void datagridPaiement_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            /* TabControlPrincipalDetails.IsEnabled = false;
             buttonCaissePayer.IsEnabled = true;
             buttonCaisseAnnuler.IsEnabled = true;*/

            if (ActiveSoumission == null)
            {
                ActiveSoumission = FindActiveSoumission();
            }

            if (ActiveSoumission != null)
            {
                UpdateSoumissionCaisse(ActiveSoumission);
            }
        }

        private void buttonCaisseAnnuler_Click(object sender, RoutedEventArgs e)
        {
            /*buttonCaissePayer.IsEnabled = false;
            buttonCaisseAnnuler.IsEnabled = false;*/

            ActiveSoumission.Facture.Paiements.Clear();
            datagridPaiement.Items.Refresh();
            // TabControlPrincipalDetails.IsEnabled = true;
        }

        private void buttonCaissePayer_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveSoumission == null)
            {
                ActiveSoumission = FindActiveSoumission();
            }

            if (ActiveSoumission != null && ActiveSoumission.Facture != null)
            {
                //Persiter la facture
                Taxe taxe;
                ITaxeService _taxeService = ServiceFactory.Instance.GetService<ITaxeService>();
                taxe = _taxeService.RetrieveNow();

                if (taxe == null)
                {
                    MessageBox.Show("Impossible de récuperer la taxe.");
                }
                else
                {
                    ActiveSoumission.Facture.Taxe = taxe;

                    foreach (Paiement p in ActiveSoumission.Facture.Paiements)
                    {
                        p.Facture = ActiveSoumission.Facture;
                        p.Client = ActiveSoumission.Client;
                        p.Employe = ActiveSoumission.Employe;
                    }

                    IFactureService _factureService = ServiceFactory.Instance.GetService<IFactureService>();
                    _factureService.Insert(ActiveSoumission.Facture);

                    MessageBox.Show("La facture a bien été créée.");

                    buttonCaissePayer.IsEnabled = false;
                    buttonCaisseAnnuler.IsEnabled = false;
                }
            }

        }

        public void CloseCurrentTab()
        {
            TabItem item = TabControlPrincipalDetails.SelectedItem as TabItem;

            if (item != null)
            {
                TabControlPrincipalDetails.Items.Remove(item);
            }
        }
    }
}
