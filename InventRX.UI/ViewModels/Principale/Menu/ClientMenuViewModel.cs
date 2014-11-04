using Cstj.MvvmToolkit;
using Cstj.MvvmToolkit.Services;
using InventRX.Logic.Models.Args;
using InventRX.Logic.Models.Entities;
using InventRX.Logic.Services.NHibernate;
using InventRX.Logic.Services.Definitions;
using InventRX.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace InventRX.UI.ViewModel
{
    /// <summary>
    /// Logique d'interaction avec le View du menu pour les clients
    /// Kev
    /// </summary>
    public class ClientMenuViewModel : MainWindow
    {/*
        private IClientService _clientService;
        public RetrieveClientArgs RetrieveClientArgs { get; set; }
        public IList<Client> ListeClients { get; set; }
        public ClientMenuViewModel()
        {
            TabItem nouvelleTab = new TabItem();
            nouvelleTab.Header = "Clients";
            nouvelleTab.DataContext = ViewModel;

            ServiceFactory.Instance.Register<IClientService, NHibernateClientService>(new NHibernateClientService());

            //Charge la liste de tous les clients
            _clientService = ServiceFactory.Instance.GetService<IClientService>();
            RetrieveClientArgs = new RetrieveClientArgs();
            ListeClients = _clientService.RetrieveAll();
            //datagridListeClients.ItemsSource = ListeClients;
        }
        private void datagridListeClients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //TODO : Gerer si la tabulation est deja ouverte pour ce client
            Client clientSelectionnee = (datagridListeClients.SelectedItem as Client);
            if (clientSelectionnee != null)
            {
                datagridListeClients.SelectedItem = null;

            }
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "Client", clientSelectionnee } };
            //TODO : Enlever la creation de new mainviewmodel et client view
            ViewModel.CurrentView = new ClientDetailsView(parameters);
            ContentPresenter contentPresenter = new ContentPresenter();

            Binding myBinding = new Binding("client" + clientSelectionnee.IdClient + "Data");
            myBinding.Source = ViewModel.CurrentView;
            contentPresenter.Content = myBinding.Source;

            TabItem nouvelleTab = new TabItem();
            nouvelleTab.Header = "client #" + clientSelectionnee.IdClient;
            nouvelleTab.Content = contentPresenter;
            nouvelleTab.DataContext = ViewModel;
            TabControlPrincipalDetails.Items.Add(nouvelleTab);
            TabControlPrincipalDetails.SelectedItem = nouvelleTab;
        }
    */}
}
