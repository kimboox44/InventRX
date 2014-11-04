using Cstj.MvvmToolkit;
using Cstj.MvvmToolkit.Services;
using Cstj.MvvmToolkit.Services.Definitions;
using InventRX.Logic.Models.Args;
using InventRX.Logic.Models.Entities;
using InventRX.Logic.Services.NHibernate;
using InventRX.Logic.Services.Definitions;
using InventRX.UI.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace InventRX.UI.ViewModel
{

    //TODO : peut-être besoin d'un using des views ou les services s'en occupent ?
    public class ClientDetailsViewModel : BaseViewModel
    {
        private IClientService _clientService;
        /// <summary>
        /// TODO : Clarifier les Args
        /// </summary>
        public RetrieveClientArgs RetrieveClientArgs { get; set; }
        /// <summary>
        /// Initialize une nouvelle instance de la classe ClientViewModel avec ses services.
        /// Les services pour parller à la BD.
        /// </summary>
        public ClientDetailsViewModel()
        {
            _clientService = ServiceFactory.Instance.GetService<IClientService>();
        }

        /// <summary>
        ///    Get l'instance du client
        /// </summary>
        private Client _client;
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

        /// <summary>
        /// TODO : Clarifier les CurrentView et les definition de RaisePropertyChanged et changing.
        /// Sont-ils dans les services ?
        /// </summary>
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
    }
}
