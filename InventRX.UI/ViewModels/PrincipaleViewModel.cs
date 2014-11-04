using Cstj.MvvmToolkit;
using Cstj.MvvmToolkit.Services;
using Cstj.MvvmToolkit.Services.Definitions;
using InventRX.Logic.Models.Args;
using InventRX.Logic.Services.Definitions;
using InventRX.UI.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using InventRX.Logic.Models.Entities;


namespace InventRX.UI.ViewModels
{
    public class PrincipaleViewModel : BaseViewModel, IApplicationService
    {
        private IPrincipaleService _principaleService;
        private List<Details> ListeDetails;
        private Details _details;

        public PrincipaleViewModel()
        {
            _principaleService = ServiceFactory.Instance.GetService<IPrincipaleService>();
            ItemsPrincipale = new ObservableCollection<Details>(ListeDetails);
        }

        private ObservableCollection<Details> _itemsPrincipale = new ObservableCollection<Details>();

        public ObservableCollection<Details> ItemsPrincipale
        {
            get
            {
                return _itemsPrincipale;
            }

            set
            {
                if (_itemsPrincipale == value)
                {
                    return;
                }

                _itemsPrincipale = value;
            }
        }

        private UserControl _currentView;

        public UserControl CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                if (_currentView == value)
                {
                    return;
                }
                RaisePropertyChanging();
                _currentView = value;
                RaisePropertyChanged();
            }
        }
        public void ChangeView<T>(T view)
        {
            CurrentView = view as UserControl;
        }
    }
}
