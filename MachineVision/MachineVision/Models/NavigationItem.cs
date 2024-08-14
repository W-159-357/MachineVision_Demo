using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Models
{
    /// <summary>
    /// 菜单功能模型
    /// </summary>
    public class NavigationItem : BindableBase
    {
        public NavigationItem(
            string icon,
            string name,
            string pageName,
            ObservableCollection<NavigationItem> items = null)
        {
            this.Icon = icon;
            this.Name = name;
            this.PageName = pageName;
            this.Items = items;
        }

        private string name;
        private string icon;
        private string pageName;
        private ObservableCollection<NavigationItem> items;

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        /// <summary>
        /// 菜单导航的页面名
        /// </summary>
        public string PageName
        {
            get { return pageName; }
            set { pageName = value; }
        }

        /// <summary>
        /// 菜单子项
        /// </summary>
        public ObservableCollection<NavigationItem> Items
        {
            get { return items; }
            set { items = value; }
        }
    }
}
