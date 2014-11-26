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
    public class FactureViewModel : BaseViewModel
    {
        private IFactureService _factureService;
        private Facture _facture;

        private IProduitService _produiService;
        public RetrieveProduitArgs RetrieveProduitArgs { get; set; }
        public IList<Produit> ListeProduits { get; set; }
        


        public FactureViewModel()
        {
            _factureService = ServiceFactory.Instance.GetService<IFactureService>();
            ItemsFacture = new ObservableCollection<ItemFacture>(ServiceFactory.Instance.GetService<IItemFactureService>().RetrieveAll());
            Paiements = new ObservableCollection<Paiement>(ServiceFactory.Instance.GetService<IPaiementService>().RetrieveAll());
           
             //Charge la liste des produits
            _produiService = ServiceFactory.Instance.GetService<IProduitService>();
            RetrieveProduitArgs = new RetrieveProduitArgs();
            ListeProduits = _produiService.RetrieveAll();


      
        }

        public Facture Facture
        {
            get
            {
                return _facture;
            }

            set
            {
                if (_facture == value)
                {
                    return;
                }
                _facture = value;
            }
        }

        private ObservableCollection<ItemFacture> _itemsFacture = new ObservableCollection<ItemFacture>();

        public ObservableCollection<ItemFacture> ItemsFacture
        {
            get
            {
                return _itemsFacture;
            }

            set
            {
                if (_itemsFacture == value)
                {
                    return;
                }

                _itemsFacture = value;
            }
        }

        private ObservableCollection<Paiement> _paiement = new ObservableCollection<Paiement>();

        public ObservableCollection<Paiement> Paiements
        {
            get
            {
                return _paiement;
            }

            set
            {
                if (_paiement == value)
                {
                    return;
                }

                _paiement = value;
            }
        }



        public void SauvegarderCommand()
        {
            _factureService.Update(Facture);
        }

        public void InsererCommand()
        {
            _factureService.Insert(Facture);
            (Application.Current.MainWindow as MainWindow).PagePrincipal.ListeFactures.Add(Facture);
            (Application.Current.MainWindow as MainWindow).PagePrincipal.datagridListeFactures.Items.Refresh();
        }

        public void CloseCommand()
        {
            (Application.Current.MainWindow as MainWindow).PagePrincipal.CloseCurrentTab();
        }
    }
}
