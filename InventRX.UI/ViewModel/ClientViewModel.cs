using Cstj.MvvmToolkit;
using Cstj.MvvmToolkit.Services;
using Cstj.MvvmToolkit.Services.Definitions;
using InventRX.Logic.Model.Args;
using InventRX.Logic.Model.Entities;
using InventRX.Services.Definitions;
using InventRX.UI.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace InventRX.UI.ViewModel
{
    public class ClientViewModel : BaseViewModel
    {
        private IClientService _clientService;
        private IProvinceService _provinceService;
        public IList<Province> Provinces { get; set; }
        private Client _client;
        public RetrieveProduitArgs RetrieveProduitArgs { get; set; }

        public ClientViewModel()
        {
            _clientService = ServiceFactory.Instance.GetService<IClientService>();
            _provinceService = ServiceFactory.Instance.GetService<IProvinceService>();
            Provinces = _provinceService.RetrieveAll();
        }

        public Client Client
        {
            get
            {
                return _client; 
            }
            set 
            {
                if(_client == value)
                    return;
                _client = value ;
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
