using MachineVision.Core;
using MachineVision.Models;
using MachineVision.Services;
using Prism.Commands;
using Prism.Regions;

namespace MachineVision.ViewModels
{
    public class MainViewModel : NavigationViewModel
    {
        public MainViewModel(IRegionManager manager, INavigationMenuService navigationService)
        {
            this.manager = manager;
            NavigationService = navigationService;
            NavigateCommand = new DelegateCommand<NavigationItem>(Navigate);
        }

        private bool isTopDrawerOpen;
        private readonly IRegionManager manager;

        /// <summary>
        /// 顶部工具栏展开状态
        /// </summary>
        public bool IsTopDrawerOpen
        {
            get { return isTopDrawerOpen; }
            set { isTopDrawerOpen = value; RaisePropertyChanged(); }
        }

        public INavigationMenuService NavigationService { get; }

        public DelegateCommand<NavigationItem> NavigateCommand { get; private set; }
        private void Navigate(NavigationItem item)
        {
            if (item == null) return;
            if (item.Name.Equals("全部"))
            {
                IsTopDrawerOpen = true;
                return;
            }
            IsTopDrawerOpen = false;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            NavigationService.InitMenus();
            NavigatePage("DashboardView");
            base.OnNavigatedTo(navigationContext);
        }

        private void NavigatePage(string pageName)
        {
            manager.Regions["MainViewRegion"].RequestNavigate(pageName, back =>
            {
                if (!(bool)back.Result)
                {
                    System.Diagnostics.Debug.WriteLine(back.Error.Message);
                }
            });
        }
    }
}
