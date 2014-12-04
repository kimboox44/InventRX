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
using InventRX.Logic.Model.Args;
using InventRX.Logic.Model.Entities;
using InventRX.Services.Definitions;
using InventRX.UI.ViewModel;
using Cstj.MvvmToolkit.Services;

namespace InventRX.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour ClientView.xaml
    /// </summary>
    public partial class ClientView : UserControl
    {
        public ClientViewModel ViewModel { get { return (ClientViewModel)DataContext; } }
        public RetrieveClientArgs RetrieveClientArgs { get; set; }
        public Client Client { get; set; }

        public ClientView()
        {
            InitializeComponent();
            DataContext = new ClientViewModel();
        }
        public ClientView(IDictionary<string, object> parameters)
            : this()
        {
            btnSauvegarderClient.IsEnabled = false;

            ViewModel.Client = parameters["Client"] as Client;
            if (ViewModel.Client.IdClient == null)
            {
                btnCreerClient.Visibility = Visibility.Visible;
            }
            else
            {
                btnCreerClient.Visibility = Visibility.Hidden;
            }
        }

        private void btnSauvegarder_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Client != null)
            {
                btnSauvegarderClient.IsEnabled = false;
                ViewModel.SauvegarderCommand();
            }
        }

        private void btnCreer_Click(object sender, RoutedEventArgs e)
        {
            if (textboxClientNom.Text == "" || textboxClientNumCivique.Text == "" || textboxClientPrenom.Text == "" ||
                textboxClientRue.Text == "" || textboxClientSolde.Text == "" || textboxClientTelephone.Text == "" ||
                textboxClientVille.Text == "")
            {
                MessageBox.Show("Veuillez remplir les champs manquants svp.");
            }
            else
            {
                if (ViewModel.Client != null)
                {
                    //Insérer le client dans la base de données.
                    ViewModel.InsererCommand();
                    textboxNumeroClient.Text = ViewModel.Client.IdClient.ToString();
                    btnCreerClient.Visibility = Visibility.Hidden;

                    MessageBox.Show("Le client a bien été créé.");
                }
            }
        }

        private void buttonQuitter_Click(object sender, RoutedEventArgs e)
        {
            if (btnSauvegarderClient.IsEnabled == true)
            {
                if (MessageBox.Show("Êtes-vous sûr de vouloir fermer l'onglet client sans sauvegarder les modifications?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    ViewModel.CloseCommand();
                }
            }
            else
            {
                ViewModel.CloseCommand();
            }
        }

        private void Client_TextChanged(object sender, KeyEventArgs e)
        {
            btnSauvegarderClient.Visibility = Visibility.Visible;
            btnSauvegarderClient.IsEnabled = true;
        }
    }
}
