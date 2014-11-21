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
    public partial class MainWindowClone : MetroWindow
    {
        PagePrincipal       PagePrincipal       {get; set;}
        PageProduits        PageProduits        {get; set;}
        PageAdministration  PageAdministration  {get; set;}


        public MainWindowClone()
        {
            
            InitializeComponent();
            DataContext = new MainViewModel();
            ServiceFactory.Instance.Register<IApplicationService, MainViewModel>((MainViewModel)this.DataContext);
            ServiceFactory.Instance.Register<IProvinceService, NHibernateProvinceService>(new NHibernateProvinceService());
            ServiceFactory.Instance.Register<IEmployeService, NHibernateEmployeService>(new NHibernateEmployeService());
            ServiceFactory.Instance.Register<IFactureService, NHibernateFactureService>(new NHibernateFactureService());
            ServiceFactory.Instance.Register<ITaxeService, NHibernateTaxeService>(new NHibernateTaxeService());

            PagePrincipal       = new PagePrincipal();
            PageProduits        = new PageProduits();
            PageAdministration  = new PageAdministration();
            MainFrame.NavigationService.Navigate(PagePrincipal);
        }

        private void btn_menuPrincipal_Click(object sender, RoutedEventArgs e)
        {
           MainFrame.NavigationService.Navigate(PagePrincipal);           
        }

        private void btn_menuProduit_Click(object sender, RoutedEventArgs e)
        {   MainFrame.NavigationService.Navigate(PageProduits);
        }

        private void btn_menuAdministration_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(PageAdministration);
        }
    }
}