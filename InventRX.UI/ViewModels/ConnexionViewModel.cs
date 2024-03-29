﻿using Cstj.MvvmToolkit;
using Cstj.MvvmToolkit.Services;
using Cstj.MvvmToolkit.Services.Definitions;
using InventRX.Logic.Models.Args;
using InventRX.Logic.Models.Entities;
using InventRX.Logic.Services.Definitions;
using InventRX.UI.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace InventRX.UI.ViewModel
{
    public class ConnexionViewModel : BaseViewModel
    {
        public ConnexionViewModel()
        {
            
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
