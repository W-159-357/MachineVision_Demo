using HalconDotNet;
using MachineVision.Core;
using MachineVision.Shared.Controls;
using Microsoft.Win32;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.TemplateMatch.ViewModels
{
    public class DrawShapeViewModel : NavigationViewModel
    {
        public DrawShapeViewModel()
        {
            LoadImageCommand = new DelegateCommand(loadImage);
            DrawObjectList = new ObservableCollection<DrawingObjectInfo>();
        }

        private HObject image;

        public HObject Image
        {
            get { return image; }
            set { image = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<DrawingObjectInfo> drawObjectList;

        public ObservableCollection<DrawingObjectInfo> DrawObjectList
        {
            get { return drawObjectList; }
            set { drawObjectList = value; RaisePropertyChanged(); }
        }


        public DelegateCommand LoadImageCommand { get; private set; }
        private void loadImage()
        {
            // 通过打开文件对话框来获得文件的路劲
            OpenFileDialog dialog = new OpenFileDialog();
            var dialogResult = (bool)dialog.ShowDialog();
            if (dialogResult)
            {
                var img = new HImage();
                img.ReadImage(dialog.FileName);
                Image = img;
            }
        }
    }
}
