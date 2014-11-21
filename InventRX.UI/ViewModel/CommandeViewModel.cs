using Cstj.MvvmToolkit;
using Cstj.MvvmToolkit.Services;
using Cstj.MvvmToolkit.Services.Definitions;
using InventRX.Logic.Model.Args;
using InventRX.Logic.Model.Entities;
using InventRX.Services.Definitions;
using InventRX.UI.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace InventRX.UI.ViewModel
{
    public class CommandeViewModel : BaseViewModel
    {
        private ICommandeService _commandeService;
        private Commande _commande;

        private IProduitService _produiService;
        public RetrieveProduitArgs RetrieveProduitArgs { get; set; }
        public IList<Produit> ListeProduits { get; set; }
        public List<string> Etats { get; set; }

        public CommandeViewModel()
        {
            Etats = new List<string>();
            Etats.Add("En attente");
            Etats.Add("En cours");
            Etats.Add("Incomplète");
            Etats.Add("Complétée");

            _commandeService = ServiceFactory.Instance.GetService<ICommandeService>();
            ItemsCommande = new ObservableCollection<ItemCommande>(ServiceFactory.Instance.GetService<IItemCommandeService>().RetrieveAll());

            //Charge la liste des produits
            _produiService = ServiceFactory.Instance.GetService<IProduitService>();
            RetrieveProduitArgs = new RetrieveProduitArgs();
            ListeProduits = _produiService.RetrieveAll();

        }

        public Commande Commande
        {
            get
            {
                return _commande;
            }

            set
            {
                if (_commande == value)
                {
                    return;
                }
                _commande = value;
            }
        }

        private ObservableCollection<ItemCommande> _itemsCommande = new ObservableCollection<ItemCommande>();

        public ObservableCollection<ItemCommande> ItemsCommande
        {
            get
            {
                return _itemsCommande;
            }

            set
            {
                if (_itemsCommande == value)
                {
                    return;
                }

                _itemsCommande = value;
            }
        }


        public void SauvegarderCommand()
        {
            _commandeService.Update(Commande);
        }

        public void InsererCommand()
        {
            _commandeService.Insert(Commande);
        }

        public void CloseCommand()
        {
            MainWindow win = (Application.Current.MainWindow as MainWindow);
            win.PagePrincipal.CloseCurrentTab();
        }
    }
}
