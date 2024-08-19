using MachineVision.Core;
using MachineVision.Extensions;
using MachineVision.Models;
using MachineVision.Shared.Events;
using Prism.Events;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace MachineVision.ViewModels
{
    public class SettingViewModel : NavigationViewModel
    {
        public SettingViewModel(IEventAggregator aggregator)
        {
            LanguageInfos = new ObservableCollection<LanguageInfo>();
            this.aggregator = aggregator;

        }

        private ObservableCollection<LanguageInfo> languageInfos;
        private LanguageInfo currentLanguage;
        private readonly IEventAggregator aggregator;

        public ObservableCollection<LanguageInfo> LanguageInfos
        {
            get { return languageInfos; }
            set { languageInfos = value; }
        }

        public LanguageInfo CurrentLanguage
        {
            get { return currentLanguage; }
            set
            {
                currentLanguage = value;
                LanguageChange();
                RaisePropertyChanged();
            }
        }

        private void LanguageChange()
        {
            //设置当前语言
            LanguageHelper.SetLanguage(CurrentLanguage.Key);
            //通过所有的界面刷新语言
            aggregator.GetEvent<LanguageEventBus>().Publish(true);
            //保存系统系统 - 保存按钮里面去做
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            InitLanguageInfos();
        }

        public void InitLanguageInfos()
        {
            LanguageInfos.Add(new LanguageInfo() { Key = "zh-CN", Value = "Chinese" });
            LanguageInfos.Add(new LanguageInfo() { Key = "en-US", Value = "English" });
        }
    }
}
