﻿using Cstj.MvvmToolkit.Services;
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
        private MainWindow _mainWindow { get; set; }

        public ConnexionView()
        {
            InitializeComponent();
            _mainWindow = (Application.Current.MainWindow as MainWindow);
        }

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _employeService = ServiceFactory.Instance.GetService<IEmployeService>();
                _retrieveEmployeArgs = new RetrieveEmployeArgs();
                _retrieveEmployeArgs.NomUsager = textboxNomUsager.Text;
                _retrieveEmployeArgs.MotDePasse = textboxMotDePasse.Password;
                _mainWindow.Employe = _employeService.RetrieveByIdents(_retrieveEmployeArgs);
                if (_mainWindow.Employe != null && _mainWindow.Employe.IdEmploye != null)
                {
                    labelWarning.Visibility = Visibility.Hidden;
                    _mainWindow.IsLogged(true);
                }
                else
                {
                    labelWarning.Content = "Le nom d'usager ou le mot de passe est incorrect.";
                    labelWarning.Visibility = Visibility.Visible;
                    _mainWindow.IsLogged(false);
                }
            }
            catch (Exception)
            {
                labelWarning.Content = "Une erreur interne est survenue.";
                labelWarning.Visibility = Visibility.Visible;
                _mainWindow.IsLogged(false);
            }
        }
    }
}
