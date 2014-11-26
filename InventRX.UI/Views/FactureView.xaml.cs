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
using InventRX.Logic.Model.Entities;
using InventRX.UI.ViewModel;
using System.Collections.ObjectModel;
using InventRX.Services.Definitions;
using InventRX.Logic.Model.Args;
using Cstj.MvvmToolkit.Services;

namespace InventRX.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour FactureView.xaml
    /// </summary>
    public partial class FactureView : UserControl
    {
        public FactureViewModel ViewModel { get { return (FactureViewModel)DataContext; } }
        
        private IClientService _clientService;
    
        public RetrieveClientArgs RetrieveClientArgs { get; set; }
        public Client Client { get; set; }
        public Employe Employe { get; set; }

        public FactureView()
        {
            InitializeComponent();
            DataContext = new FactureViewModel();
        }

        public FactureView(IDictionary<string, object> parameters)
            : this()
        {
            ViewModel.Facture = parameters["Facture"] as Facture;
            //Si c'est une nouvelle Facture, on affiche le bouton créer au lieu de sauvegarder
            if (ViewModel.Facture.IdFacture == null)
            {
                if (ViewModel.Facture.Client == null)
                {
                    Client = new Client();
                    ViewModel.Facture.Client = Client;
                }
                if (ViewModel.Facture.ItemsFacture == null)
                {
                    //List<ItemFacture> liste = new List<ItemFacture>();
                    ViewModel.Facture.ItemsFacture = new List<ItemFacture>();
                }
                if (ViewModel.Facture.Paiements == null)
                {
                    //List<ItemFacture> liste = new List<ItemFacture>();
                    ViewModel.Facture.Paiements = new List<Paiement>();
                }
            }


            double sousTotal = 0;
            double sousTotalTPS;
            double sousTotalTVQ;
            double total;
            const double TPS = 5;
            const double TVQ = 9.975;



            foreach (Paiement paiement in ViewModel.Facture.Paiements)
            {
                sousTotal += paiement.Montant;
            }

            sousTotalTPS = Math.Round(sousTotal * (TPS / 100), 2);
            sousTotalTVQ = Math.Round(sousTotal * (TVQ / 100), 2);
            total = (sousTotal + sousTotalTPS + sousTotalTVQ);
                       
            labelSousTotal.Content = sousTotal.ToString("C2");
            labelTPS.Content = sousTotalTPS.ToString("C2");
            labelTVQ.Content = sousTotalTVQ.ToString("C2");
            labelTotal.Content = total.ToString("C2");

        }

        private void btnSauvegarder_Click(object sender, RoutedEventArgs e)
        {
            //Si aucun client, on va le chercher dans la base de données par son numéro de téléphone
            if (ViewModel.Facture.Client == null)
            {
                _clientService = ServiceFactory.Instance.GetService<IClientService>();
                RetrieveClientArgs = new RetrieveClientArgs();
                RetrieveClientArgs.IdClient = Convert.ToInt32(ViewModel.Facture.Client.IdClient);
                Client = _clientService.Retrieve(RetrieveClientArgs);
            }
            ViewModel.SauvegarderCommand();
        }
      

        private void btnSupprimerItem_Click(object sender, RoutedEventArgs e)
        {
            ItemFacture item = (ItemFacture)((sender as Button).CommandParameter);
            if (item != null)
            {
                if (datagridListeItemsFacture.ItemsSource != null)
                {
                    ViewModel.Facture.ItemsFacture.Remove(item);
                    datagridListeItemsFacture.Items.Refresh();
                }
            }
        }

        private void btnCreer_Click(object sender, RoutedEventArgs e)
        {
            //Vérifier si le client dans la base de données grâce a son numéro de téléphone
            IEmployeService _employeService = ServiceFactory.Instance.GetService<IEmployeService>();
            RetrieveEmployeArgs retrieveEmployeArgs = new RetrieveEmployeArgs();
            retrieveEmployeArgs.IdEmploye = 1;
            Employe = _employeService.Retrieve(retrieveEmployeArgs);
            /*Employe employe = new Employe();
            employe.IdEmploye = 1;*/
            ViewModel.Facture.Employe = Employe; // Employé Cruise

            //On met la date du jour
            DateTime date = new DateTime();
            ViewModel.Facture.Date = date;
 
        }

        private void buttonQuitter_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CloseCommand();            
        }   
    }
}
