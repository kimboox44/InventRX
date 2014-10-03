using Cstj.MvvmToolkit.Services;
using Cstj.MvvmToolkit.Services.Definitions;
using InventRX.Logic.Model.Args;
using InventRX.Logic.Model.Entities;
using InventRX.Logic.Services.NHibernate;
using InventRX.Services;
using InventRX.Services.Definitions;
using InventRX.UI.ViewModel;
using InventRX.UI.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InventRX.UI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get { return (MainViewModel)DataContext; } }

        #region Soumission

        private ISoumissionService _soumissionService;
        public RetrieveSoumissionArgs RetrieveSoumissionArgs { get; set; }
        /*private ObservableCollection<Soumission> _soumissions = new ObservableCollection<Soumission>();

        public ObservableCollection<Soumission> Soumissions
        {
            get
            {
                return _soumissions;
            }

            set
            {
                if (_soumissions == value)
                {
                    return;
                }
                _soumissions = value;
            }
        }*/

        public IList<Soumission> ListeSoumissions { get; set; } 

        #endregion


        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            Configure();

            ViewModel.CurrentView = new ConnexionView();
        }

        private void Configure()
        {
            ServiceFactory.Instance.Register<ISoumissionService, NHibernateSoumissionService>(new NHibernateSoumissionService());

            ServiceFactory.Instance.Register<IApplicationService, MainViewModel>((MainViewModel)this.DataContext);


            //Charge la liste de toutes les soumissions
            _soumissionService = ServiceFactory.Instance.GetService<ISoumissionService>();
            //Soumissions = new ObservableCollection<Soumission>(_soumissionService.RetrieveAll());
            RetrieveSoumissionArgs = new RetrieveSoumissionArgs();
            ListeSoumissions = _soumissionService.RetrieveAll();
            //Simulation fake
            foreach (Soumission s in  ListeSoumissions)
            {
                s.Client = new Client();
                s.Client.Nom = "Doe";
                s.Client.Prenom = "John";
            }
            datagridListeSoumissions.ItemsSource = ListeSoumissions;
        }

        #region Tabs Config
        /*
         * http://social.msdn.microsoft.com/Forums/vstudio/en-US/ed077477-a742-4c3d-bd4e-3efdd5dd6ba2/dragdrop-tabitem?forum=wpf
         * Pour déplacer les tabs dans l'ordre que l'on veut.
         */
        private void TabItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var tabItem = e.Source as TabItem;

            if (tabItem == null)
                return;

            if (Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(tabItem, tabItem, DragDropEffects.All);
            }
        }

        private void TabItem_Drop(object sender, DragEventArgs e)
        {
            var tabItemTarget = e.Source as TabItem;

            var tabItemSource = e.Data.GetData(typeof(TabItem)) as TabItem;

            if (!tabItemTarget.Equals(tabItemSource))
            {
                var tabControl = tabItemTarget.Parent as TabControl;
                int sourceIndex = tabControl.Items.IndexOf(tabItemSource);
                int targetIndex = tabControl.Items.IndexOf(tabItemTarget);

                tabControl.Items.Remove(tabItemSource);
                tabControl.Items.Insert(targetIndex, tabItemSource);

                tabControl.Items.Remove(tabItemTarget);
                tabControl.Items.Insert(sourceIndex, tabItemTarget);
            }
        }
        #endregion

    }
}