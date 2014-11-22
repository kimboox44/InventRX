using Cstj.MvvmToolkit.Services;
using InventRX.Logic.Model.Args;
using InventRX.Services.Definitions;
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

namespace InventRX.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour ConnexionView.xaml
    /// </summary>
    public partial class ConnexionView : UserControl
    {
        private IEmployeService _employeService;
        private RetrieveEmployeArgs _retrieveEmployeArgs;
        private MainWindow MainWindow { get; set; }

        public ConnexionView()
        {
            InitializeComponent();
            MainWindow = (Application.Current.MainWindow as MainWindow);
        }

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _employeService = ServiceFactory.Instance.GetService<IEmployeService>();
                _retrieveEmployeArgs = new RetrieveEmployeArgs();
                _retrieveEmployeArgs.NomUsager = textboxNomUsager.Text;
                _retrieveEmployeArgs.MotDePasse = textboxMotDePasse.Password;
                MainWindow.Employe = _employeService.RetrieveByIdents(_retrieveEmployeArgs);
                if (MainWindow.Employe != null && MainWindow.Employe.IdEmploye != null)
                {
                    labelWarning.Visibility = Visibility.Hidden;
                    MainWindow.IsLogged(true);
                }
                else
                {
                    labelWarning.Content = "Le nom d'usager ou le mot de passe est incorrect.";
                    labelWarning.Visibility = Visibility.Visible;
                    MainWindow.IsLogged(false);
                }
            }
            catch (Exception)
            {
                labelWarning.Content = "Une erreur interne est survenue.";
                labelWarning.Visibility = Visibility.Visible;
                MainWindow.IsLogged(false);
            }
        }
    }
}
