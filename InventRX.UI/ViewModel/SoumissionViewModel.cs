﻿using Cstj.MvvmToolkit;
using Cstj.MvvmToolkit.Services;
using Cstj.MvvmToolkit.Services.Definitions;
using InventRX.Logic.Model.Args;
using InventRX.Logic.Model.Entities;
using InventRX.Services.Definitions;
using InventRX.UI.Views;
using Microsoft.TeamFoundation.MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace InventRX.UI.ViewModel
{
    public class SoumissionViewModel : BaseViewModel
    {
        private ISoumissionService _soumissionService;
        private Soumission _soumission;

        private IProduitService _produiService;
        public RetrieveProduitArgs RetrieveProduitArgs { get; set; }
        public IList<Produit> ListeProduits { get; set; }

        public SoumissionViewModel()
        {
            _soumissionService = ServiceFactory.Instance.GetService<ISoumissionService>();
            ItemsSoumission = new ObservableCollection<ItemSoumission>(ServiceFactory.Instance.GetService<IItemSoumissionService>().RetrieveAll());
            
            //Charge la liste des produits
            _produiService = ServiceFactory.Instance.GetService<IProduitService>();
            RetrieveProduitArgs = new RetrieveProduitArgs();
            ListeProduits = _produiService.RetrieveAll();
        }

        public Soumission Soumission
        {
            get
            {
                return _soumission;
            }

            set
            {
                if (_soumission == value)
                {
                    return;
                }
                _soumission = value;
            }
        }

        private ObservableCollection<ItemSoumission> _itemsSoumission = new ObservableCollection<ItemSoumission>();

        public ObservableCollection<ItemSoumission> ItemsSoumission
        {
            get
            {
                return _itemsSoumission;
            }

            set
            {
                if (_itemsSoumission == value)
                {
                    return;
                }

                _itemsSoumission = value;
            }
        }

        public void SauvegarderCommand()
        {
            _soumissionService.Update(Soumission);
            CloseCommand();
        }

        public void InsererCommand()
        {
            _soumissionService.Insert(Soumission);
            (Application.Current.MainWindow as MainWindow).PagePrincipal.ListeSoumissions.Add(Soumission);
            (Application.Current.MainWindow as MainWindow).PagePrincipal.datagridListeSoumissions.Items.Refresh();
        }

        public void CloseCommand()
        {
            (Application.Current.MainWindow as MainWindow).PagePrincipal.CloseCurrentTab();
        }
    }
}
