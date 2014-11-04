using Cstj.MvvmToolkit.Services;
using Cstj.MvvmToolkit.Services.Definitions;
using InventRX.Logic.Models.Args;
using InventRX.Logic.Models.Entities;
using InventRX.Logic.Services.NHibernate;
using InventRX.Services;
using InventRX.Logic.Services.Definitions;
using InventRX.UI.ViewModels;
using InventRX.UI.Views;
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
using System.Windows.Shapes;
using InventRX.UI.Views.Principale.Details;
using InventRX.UI.ViewModels.Principale.Details;
using InventRX.UI.Views.Principale.Menu;

namespace InventRX.UI.Views
{
    /// <summary>
    /// Interaction logic for TabPrincipaleView.xaml
    /// </summary>
    public partial class PrincipaleView : UserControl
    {
       /* public PrincipaleViewModel ViewModelDetails { get { return (PrincipaleViewModel)DataContext; } }
        public PrincipaleViewModel ViewModelMenu { get { return (PrincipaleViewModel)DataContext; } }*/
        public PrincipaleView()
        {
            InitializeComponent();
            DataContext = new PrincipaleViewModel();
            ServiceFactory.Instance.Register<IApplicationService, PrincipaleViewModel>((PrincipaleViewModel)this.DataContext);
            /*ViewModelDetails.CurrentView = new AjoutDetailsView();
            ViewModelMenu.CurrentView = new CaisseMenuView();*/
        }
    }

}