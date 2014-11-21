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
using Cstj.MvvmToolkit.Services;
using InventRX.Logic.Model.Args;
using InventRX.Logic.Model.Entities;
using InventRX.Logic.Services.NHibernate;
using InventRX.Services.Definitions;

namespace InventRX.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour PageProduits.xaml
    /// </summary>
    public partial class PageProduits : Page
    {
        public PageProduits()
        {
            InitializeComponent();
            ChargerProduits();
        }

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
    }
}
