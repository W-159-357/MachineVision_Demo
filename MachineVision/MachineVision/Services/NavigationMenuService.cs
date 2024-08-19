using MachineVision.Extensions;
using MachineVision.Models;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace MachineVision.Services
{
    internal class NavigationMenuService : BindableBase, INavigationMenuService
    {
        public NavigationMenuService()
        {
            Items = new ObservableCollection<NavigationItem>();
        }

        private ObservableCollection<NavigationItem> items;

        public ObservableCollection<NavigationItem> Items
        {
            get { return items; }
            set { items = value; RaisePropertyChanged(); }
        }

        public void InitMenus()
        {
            Items.Clear();
            Items.Add(new NavigationItem("", "All", "全部", "", new ObservableCollection<NavigationItem>()
            {
                new NavigationItem("","TemplateMatch", "模板匹配","",new ObservableCollection<NavigationItem>()
                {
                    new NavigationItem("ShapeCirclePlus","OutlineMatch",  "轮廓匹配", "DrawShapeView"),
                    new NavigationItem("ShapeOutline","ShapeMatch",  "形状匹配", ""),
                    new NavigationItem("Clouds","NccMatch",  "相似性匹配", ""),
                    new NavigationItem("ShapeOvalPlus","DeformationMatch",  "形变匹配", ""),
                }),
                new NavigationItem("","Measure", "比较测量","",new ObservableCollection<NavigationItem>()
                {
                    new NavigationItem("Circle","Caliper",  "卡尺找圆", ""),
                    new NavigationItem("Palatte","Color",  "颜色检测", ""),
                    new NavigationItem("Ruler","GeometricMeasurement",  "几何测量", ""),
                }),
                new NavigationItem("","Character", "字符识别","",new ObservableCollection<NavigationItem>()
                {
                    new NavigationItem("FormatColorText","Character",  "字符识别", ""),
                    new NavigationItem("Barcode","BarCode",  "一维码识别", ""),
                    new NavigationItem("Qrcode", "QrCode", "二维码识别", ""),
                }),
                new NavigationItem("","Defect", "缺陷检测","",new ObservableCollection<NavigationItem>()
                {
                    new NavigationItem("Crop","Difference",  "差分模型", ""),
                    new NavigationItem("CropRotate","Deformable",  "形变模型", ""),
                })
            }));
            Items.Add(new NavigationItem("", "TemplateMatch", "模板匹配", ""));
            Items.Add(new NavigationItem("", "Measure", "比较测量", ""));
            Items.Add(new NavigationItem("", "Character", "字符识别", ""));
            Items.Add(new NavigationItem("", "Defect", "缺陷检测", ""));
            Items.Add(new NavigationItem("", "Document", "学习文档", ""));
            Items.Add(new NavigationItem("", "Setting", "系统设置", "SettingView"));
        }

        public void RefreshMenus()
        {
            foreach (var item in Items)
            {
                item.Name = LanguageHelper.KeyValues[item.Key];
                if (item.Items != null && item.Items.Count > 0)
                {
                    foreach (var subItem in item.Items)
                    {
                        subItem.Name = LanguageHelper.KeyValues[subItem.Key];
                    }
                }
            }
        }
    }
}
