using MachineVision.Core;
using MachineVision.Models;
using MachineVision.Services;
using Prism.Commands;
using Prism.Regions;

namespace MachineVision.ViewModels
{
    public class DashboradViewModel : NavigationViewModel
    {
        private readonly IRegionManager manager;

        public DashboradViewModel(IRegionManager manager, INavigationMenuService navigationService)
        {
            this.manager = manager;
            NavigationService = navigationService;
            OpenPageCommand = new DelegateCommand<NavigationItem>(OpenPage);
        }

        public INavigationMenuService NavigationService { get; }

        public DelegateCommand<NavigationItem> OpenPageCommand { get; private set; }
        private void OpenPage(NavigationItem item)
        {
            manager.Regions["MainViewRegion"].RequestNavigate(item.PageName);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
        }
    }
}
