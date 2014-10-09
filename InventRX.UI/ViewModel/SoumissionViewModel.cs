using Cstj.MvvmToolkit;
using Cstj.MvvmToolkit.Services;
using Cstj.MvvmToolkit.Services.Definitions;
using InventRX.Logic.Model.Args;
using InventRX.Logic.Model.Entities;
using InventRX.Services.Definitions;
using InventRX.UI.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace InventRX.UI.ViewModel
{
    public class SoumissionViewModel : BaseViewModel
    {
        private ISoumissionService _soumissionService;
        private Soumission _soumission;

        public SoumissionViewModel()
        {
            _soumissionService = ServiceFactory.Instance.GetService<ISoumissionService>();
        }

        public Soumission Soumission
        {
            get
            {
                return _soumission;
            }

            set
            {
                if (_soumission == value)
                {
                    return;
                }
                _soumission = value;
            }
        }


        public void SauvegarderCommand()
        {
            /*Soumission.Visites.Where(v => v.Maison == null).ToList().ForEach(v => v.Maison = Maison);
            _maisonService.Update(Maison);

            IApplicationService appService = ServiceFactory.Instance.GetService<IApplicationService>();
            appService.ChangeView<RechercheView>(new RechercheView());*/
        }
      

    }
}
