using Cstj.MvvmToolkit.Services;
using Cstj.MvvmToolkit.Services.Definitions;
using InventRX.Logic.Model.Args;
using InventRX.Logic.Model.Entities;
using InventRX.Logic.Services.NHibernate;
using InventRX.Services;
using InventRX.Services.Definitions;
using InventRX.UI.ViewModel;
using InventRX.UI.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;

namespace InventRX.UI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get { return (MainViewModel)DataContext; } }

        #region Soumission

        private ISoumissionService _soumissionService;
        public RetrieveSoumissionArgs RetrieveSoumissionArgs { get; set; }
        public IList<Soumission> ListeSoumissions { get; set; }

        private void ChargerSoumissions()
        {
            ServiceFactory.Instance.Register<ISoumissionService, NHibernateSoumissionService>(new NHibernateSoumissionService());
            ServiceFactory.Instance.Register<IItemSoumissionService, NHibernateItemSoumissionService>(new NHibernateItemSoumissionService());
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

            if(soumissionSelectionnee != null)
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
                    newScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                    newScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
                    newScrollViewer.Content = contentPresenter;

                    //Création d'un nouveau item
                    TabItem nouvelleTab = new TabItem();
                    nouvelleTab.Header = "Soumission #" + soumissionSelectionnee.IdSoumission;
                    nouvelleTab.Content = newScrollViewer;

                    //Sans scrollviewer
                    //nouvelleTab.Content = contentPresenter;

                    nouvelleTab.DataContext = nouveauViewModel;

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
            newScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            newScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            newScrollViewer.Content = contentPresenter;

            //Création d'un nouveau item
            TabItem nouvelleTab = new TabItem();
            nouvelleTab.Header = "Nouvelle soumission";
            nouvelleTab.Content = newScrollViewer;

            //Sans scrollviewer
            //nouvelleTab.Content = contentPresenter;

            nouvelleTab.DataContext = nouveauViewModel;

            //Ajout de l'item à la tab control
            TabControlPrincipalDetails.Items.Add(nouvelleTab);
            TabControlPrincipalDetails.SelectedItem = nouvelleTab;
        }

        #endregion

        #region Produit

        private IProduitService _produiService;
        public RetrieveProduitArgs RetrieveProduitArgs { get; set; }
        public IList<Produit> ListeProduits { get; set; }

        private void ChargerProduits()
        {
            ServiceFactory.Instance.Register<IProduitService, NHibernateProduitService>(new NHibernateProduitService());
            //Charge la liste de tous les produits
            _produiService = ServiceFactory.Instance.GetService<IProduitService>();
            RetrieveProduitArgs = new RetrieveProduitArgs();
            ListeProduits = _produiService.RetrieveAll();
            datagridListeProduits.ItemsSource = ListeProduits;
        }

        #endregion

        #region Commande
        private ICommandeService _commandeService;
        public RetrieveCommandeArgs RetrieveCommandeArgs { get; set; }
        public IList<Commande> ListeCommandes { get; set; }

        private void ChargerCommandes()
        {
            ServiceFactory.Instance.Register<ICommandeService, NHibernateCommandeService>(new NHibernateCommandeService());
            //Charge la liste de toutes les commandes
            _commandeService = ServiceFactory.Instance.GetService<ICommandeService>();
            RetrieveCommandeArgs = new RetrieveCommandeArgs();
            ListeCommandes = _commandeService.RetrieveAll();
            datagridListeCommandes.ItemsSource = ListeCommandes;
        }


        private void datagridListeCommandes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Commande commandeSelectionnee = (datagridListeCommandes.SelectedItem as Commande);

            MainViewModel nouveauViewModel = new MainViewModel();
            nouveauViewModel.CurrentView = new CommandeView();

            ContentPresenter contentPresenter = new ContentPresenter();

            Binding myBinding = new Binding("commande" + commandeSelectionnee.IdCommande + "Data");
            myBinding.Source = nouveauViewModel.CurrentView;
            contentPresenter.Content = myBinding.Source;

            TabItem nouvelleTab = new TabItem();
            nouvelleTab.Header = "Commande #" + commandeSelectionnee.IdCommande;
            nouvelleTab.Content = contentPresenter;
            nouvelleTab.DataContext = nouveauViewModel;
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
            ServiceFactory.Instance.Register<IClientService, NHibernateClientService>(new NHibernateClientService());

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
            if(clientSelectionnee != null)
            {
                datagridListeClients.SelectedItem=null;

            }
            Dictionary<string, object> parameters = new Dictionary<string,object>() {{"Client", clientSelectionnee}};
            //TODO : Enlever la creation de new mainviewmodel et client view
            MainViewModel nouveauViewModel = new MainViewModel();
            nouveauViewModel.CurrentView = new ClientView(parameters);
            ContentPresenter contentPresenter = new ContentPresenter();

            Binding myBinding = new Binding("client" + clientSelectionnee.IdClient + "Data");
            myBinding.Source = nouveauViewModel.CurrentView;
            contentPresenter.Content = myBinding.Source;

            TabItem nouvelleTab = new TabItem();
            nouvelleTab.Header = "client #" + clientSelectionnee.IdClient;
            nouvelleTab.Content = contentPresenter;
            nouvelleTab.DataContext = nouveauViewModel;
            TabControlPrincipalDetails.Items.Add(nouvelleTab);
            TabControlPrincipalDetails.SelectedItem = nouvelleTab;
        }

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            ServiceFactory.Instance.Register<IApplicationService, MainViewModel>((MainViewModel)this.DataContext);
            ChargerSoumissions();
            ChargerClients();
            ChargerCommandes();
            ChargerProduits();
            ViewModel.CurrentView = new ConnexionView();
            ServiceFactory.Instance.Register<IProvinceService, NHibernateProvinceService>(new NHibernateProvinceService());
            ServiceFactory.Instance.Register<IEmployeService, NHibernateEmployeService>(new NHibernateEmployeService());
        }

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

    }
}