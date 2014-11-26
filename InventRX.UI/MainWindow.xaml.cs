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
using System;
using System.Text.RegularExpressions;
using MahApps.Metro.Controls;
using System.Windows.Media;

namespace InventRX.UI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public ConnexionView ConnexionView { get; set; }
        public PagePrincipal PagePrincipal { get; set; }
        public PageProduits PageProduits { get; set; }
        public PageAdministration PageAdministration { get; set; }
        public Employe Employe { get; set; }
        public bool AutoConnect = true;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            ServiceFactory.Instance.Register<IApplicationService, MainViewModel>((MainViewModel)this.DataContext);
            ServiceFactory.Instance.Register<IProvinceService, NHibernateProvinceService>(new NHibernateProvinceService());
            ServiceFactory.Instance.Register<IEmployeService, NHibernateEmployeService>(new NHibernateEmployeService());
            ServiceFactory.Instance.Register<IFactureService, NHibernateFactureService>(new NHibernateFactureService());
            ServiceFactory.Instance.Register<IFournisseurService, NHibernateFournisseurService>(new NHibernateFournisseurService());
            ServiceFactory.Instance.Register<IItemFactureService, NHibernateItemFactureService>(new NHibernateItemFactureService());
            ServiceFactory.Instance.Register<ITaxeService, NHibernateTaxeService>(new NHibernateTaxeService());
            ServiceFactory.Instance.Register<IClientService, NHibernateClientService>(new NHibernateClientService());
            ServiceFactory.Instance.Register<ICommandeService, NHibernateCommandeService>(new NHibernateCommandeService());
            ServiceFactory.Instance.Register<IItemCommandeService, NHibernateItemCommandeService>(new NHibernateItemCommandeService());
            ServiceFactory.Instance.Register<ISoumissionService, NHibernateSoumissionService>(new NHibernateSoumissionService());
            ServiceFactory.Instance.Register<IItemSoumissionService, NHibernateItemSoumissionService>(new NHibernateItemSoumissionService());
            ServiceFactory.Instance.Register<IPaiementService, NHibernatePaiementService>(new NHibernatePaiementService());

            ConnexionView = new ConnexionView();
            PagePrincipal = new PagePrincipal();
            PageProduits = new PageProduits();
            PageAdministration = new PageAdministration();

            if (AutoConnect == false)
            {
                IsLogged(false);
            }
            else
            {
                IsLogged(true);
            }
        }

        public void IsLogged(bool connecte)
        {
            if (connecte == false)
            {
                btn_deconnecter.Visibility = Visibility.Hidden;
                btn_menuAdministration.Visibility = Visibility.Hidden;
                btn_menuPrincipal.Visibility = Visibility.Hidden;
                btn_menuProduit.Visibility = Visibility.Hidden;
                MainFrame.NavigationService.Navigate(ConnexionView);
            }
            else
            {
                btn_deconnecter.Visibility = Visibility.Visible;
                btn_menuAdministration.Visibility = Visibility.Visible;
                btn_menuPrincipal.Visibility = Visibility.Visible;
                btn_menuProduit.Visibility = Visibility.Visible;
                MainFrame.NavigationService.Navigate(PagePrincipal);
            }
        }

        private void btn_menuPrincipal_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(PagePrincipal);
        }

        private void btn_menuProduit_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(PageProduits);
        }

        private void btn_menuAdministration_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(PageAdministration);
        }

        private void btn_deconnecter_Click(object sender, RoutedEventArgs e)
        {
            IsLogged(false);
        }
    }
}