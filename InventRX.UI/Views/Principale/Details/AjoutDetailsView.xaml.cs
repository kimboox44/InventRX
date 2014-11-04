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
using InventRX.Logic.Models.Entities;
using InventRX.UI.ViewModels;
using InventRX.UI.ViewModels.Principale.Details;

namespace InventRX.UI.Views.Principale.Details
{
    /// <summary>
    /// Logique d'interaction pour AjoutDetailsView.xaml
    /// </summary>
    public partial class AjoutDetailsView : UserControl
    {
        public AjoutDetailsViewModel ViewModel { get { return (AjoutDetailsViewModel)DataContext; } }
        public AjoutDetailsView()
        {
            InitializeComponent();

        }
        private void btnNewSoumission_Click(object sender, RoutedEventArgs e)
        {
            Soumission newSoumission = new Soumission();
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "Soumission", newSoumission } };

            ViewModel.CurrentView = new SoumissionDetailsView(parameters);

            ContentPresenter contentPresenter = new ContentPresenter();

            Binding myBinding = new Binding("soumission" + newSoumission.IdSoumission + "Data");
            myBinding.Source = ViewModel.CurrentView;
            contentPresenter.Content = myBinding.Source;

            //Ajout du contentPresenter dans un scrollviewer pour pouvoir scroller à l'interieur
            ScrollViewer newScrollViewer = new ScrollViewer();
            newScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            newScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            newScrollViewer.Content = contentPresenter;

            //Création d'un nouveau item
            TabItem nouvelleTab = new TabItem();
            nouvelleTab.Header = "Nouvelle soumission";
            nouvelleTab.Content = newScrollViewer;

            //Sans scrollviewer
            //nouvelleTab.Content = contentPresenter;

            nouvelleTab.DataContext = ViewModel;

            //Ajout de l'item à la tab control 
            //Changement MVVM 3 novembre
            PrincipaleView PrincipaleView = (this.Parent as PrincipaleView);

            PrincipaleView.TabControlPrincipaleDetails.Items.Add(nouvelleTab);
            PrincipaleView.TabControlPrincipaleDetails.SelectedItem = nouvelleTab;
            
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
