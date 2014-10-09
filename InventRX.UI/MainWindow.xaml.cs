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
using InventRX.Logic.Services.Definitions;

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

        #endregion

        #region Produit

        private IProduitService _produiService;
        public RetrieveProduitArgs RetrieveProduitArgs { get; set; }

        public IList<Produit> ListeProduits { get; set; }

        #endregion

        #region Commande
        private ICommandeService _commandeService;
        public RetrieveCommandeArgs RetrieveCommandeArgs { get; set; }

        public IList<Commande> ListeCommandes { get; set; }
        
        #endregion


        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            Configure();

            ViewModel.CurrentView = new ConnexionView();
        }

        private void Configure()
        {
            ServiceFactory.Instance.Register<ISoumissionService, NHibernateSoumissionService>(new NHibernateSoumissionService());
            ServiceFactory.Instance.Register<IProduitService, NHibernateProduitService>(new NHibernateProduitService());
            ServiceFactory.Instance.Register<ICommandeService, NHibernateCommandeService>(new NHibernateCommandeService());
            ServiceFactory.Instance.Register<IApplicationService, MainViewModel>((MainViewModel)this.DataContext);


            //Charge la liste de toutes les soumissions
            _soumissionService = ServiceFactory.Instance.GetService<ISoumissionService>();
            RetrieveSoumissionArgs = new RetrieveSoumissionArgs();
            ListeSoumissions = _soumissionService.RetrieveAll();
            datagridListeSoumissions.ItemsSource = ListeSoumissions;


            //Charge la liste de tous les produits
            _produiService = ServiceFactory.Instance.GetService<IProduitService>();
            RetrieveProduitArgs = new RetrieveProduitArgs();
            ListeProduits = _produiService.RetrieveAll();
            datagridListeProduits.ItemsSource = ListeProduits;

            //Charge la liste de toutes les commandes
            _commandeService = ServiceFactory.Instance.GetService<ICommandeService>();
            RetrieveCommandeArgs = new RetrieveCommandeArgs();
            ListeCommandes = _commandeService.RetrieveAll();
            datagridListeCommandes.ItemsSource = ListeCommandes;
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


        private void datagridListeSoumissions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Soumission soumissionSelectionnee = (datagridListeSoumissions.SelectedItem as Soumission);

            MainViewModel nouveauViewModel = new MainViewModel();
            nouveauViewModel.CurrentView = new SoumissionView();
            
            ContentPresenter contentPresenter = new ContentPresenter();

            Binding myBinding = new Binding("soumission" + soumissionSelectionnee .IdSoumission+ "Data");
            myBinding.Source = nouveauViewModel.CurrentView;
            contentPresenter.Content = myBinding.Source;

            TabItem nouvelleTab = new TabItem();
            nouvelleTab.Header = "Soumission #" + soumissionSelectionnee.IdSoumission;
            nouvelleTab.Content = contentPresenter;
            nouvelleTab.DataContext = nouveauViewModel;
            TabControlPrincipalDetails.Items.Add(nouvelleTab);
            TabControlPrincipalDetails.SelectedItem = nouvelleTab;
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





    }
}