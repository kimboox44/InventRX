﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventRX.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour PageAdminisration.xaml
    /// </summary>
    public partial class PageAide : Page
    {
        public PageAide()
        {
            InitializeComponent();

            string curDir = Directory.GetCurrentDirectory();
            string pathWeb = String.Format(@"file:///{0}\doc\index.html", curDir);
            string pathFile = String.Format(@"{0}\doc\index.html", curDir);
            Uri uri = new Uri(pathWeb);
            if (File.Exists(pathFile))
            {
                aideBrowser.Source = uri;
            }
            else
            {
                aideBrowser.NavigateToString("Le document d'aide n'existe pas.");
            }
        }

        private void btnTDM_Click(object sender, RoutedEventArgs e)
        {
            string curDir = Directory.GetCurrentDirectory();
            string pathWeb = String.Format(@"file:///{0}\doc\index.html#_Toc405585152", curDir);
            string pathFile = String.Format(@"{0}\doc\index.html", curDir);
            Uri uri = new Uri(pathWeb);
            if (File.Exists(pathFile))
            {
                aideBrowser.Navigate(pathWeb);
            }
            else
            {
                aideBrowser.NavigateToString("Le document d'aide n'existe pas.");
            }
        }

        public void NavigateToAncre(string ancre)
        {
            string curDir = Directory.GetCurrentDirectory();
            string pathWeb;
            if (ancre != "")
            {
                pathWeb = String.Format(@"file:///{0}\doc\index.html#" + ancre, curDir);
            }
            else
            {
                //Table des matières
                pathWeb = String.Format(@"file:///{0}\doc\index.html#_Toc405585152", curDir);
            }
            string pathFile = String.Format(@"{0}\doc\index.html", curDir);
            Uri uri = new Uri(pathWeb);
            if (File.Exists(pathFile))
            {
                aideBrowser.Navigate(pathWeb);
            }
            else
            {
                aideBrowser.NavigateToString("Le document d'aide n'existe pas.");
            }
        }
    }
}
