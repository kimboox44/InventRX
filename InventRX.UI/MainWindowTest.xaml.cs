using Cstj.MvvmToolkit.Services;
using Cstj.MvvmToolkit.Services.Definitions;
using InventRX.Logic.Models.Args;
using InventRX.Logic.Models.Entities;
using InventRX.Logic.Services.NHibernate;
using InventRX.Logic.Services.Definitions;
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
    /// Logique d'interaction pour MainWindowTest.xaml
    /// </summary>
    public partial class MainWindowTest : Window
    {
        public MainViewModel ViewModel { get { return (MainViewModel)DataContext; } }

        public MainWindowTest()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            ViewModel.CurrentView = new PrincipaleView();
        }
    }
}
