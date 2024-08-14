using MachineVision.Core;
using MachineVision.Services;
using Prism.Regions;


namespace MachineVision.ViewModels
{
    public class DashboradViewModel : NavigationViewModel
    {
        public DashboradViewModel(INavigationMenuService navigationService)
        {
            NavigationService = navigationService;
        }

        public INavigationMenuService NavigationService { get; }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
        }
    }
}
