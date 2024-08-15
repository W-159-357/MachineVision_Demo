using HalconDotNet;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace MachineVision.Shared.Controls
{
    public class ImageEditView : Control
    {
        private HSmartWindowControlWPF hSmart;
        private HWindow hWindow;
        private TextBlock txtMsg;

        #region 依赖属性
        public HObject Image
        {
            get { return (HObject)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(HObject), typeof(ImageEditView), new PropertyMetadata(ImageChangedCalledBack));

        public ObservableCollection<DrawingObjectInfo> DrawingObjectList
        {
            get { return (ObservableCollection<DrawingObjectInfo>)GetValue(DrawingObjectListProperty); }
            set { SetValue(DrawingObjectListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DrawingObjectListProperty =
            DependencyProperty.Register("DrawingObjectList", typeof(ObservableCollection<DrawingObjectInfo>), typeof(ImageEditView), new PropertyMetadata(new ObservableCollection<DrawingObjectInfo>()));
        #endregion

        #region 回调函数
        // 元数据发生变化时，触发此回调函数
        public static void ImageChangedCalledBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ImageEditView view && e.NewValue != null)
            {
                view.Display((HObject)e.NewValue);
            }
        }
        #endregion

        /// <summary>
        /// 显示出当前对象
        /// </summary>
        /// <param name="hObject"></param>
        private void Display(HObject hObject)
        {
            hWindow.DispObj(hObject);
            hWindow.SetPart(0, 0, -2, -2);
        }

        #region 绘制形状模板按钮的响应事件
        public override void OnApplyTemplate()
        {
            // 获取图形绘制信息
            txtMsg = (TextBlock)GetTemplateChild("PART_MSG");

            // 加载图片
            if (GetTemplateChild("PART_SMART") is HSmartWindowControlWPF hSmart)
            {
                this.hSmart = hSmart;
                this.hSmart.Loaded += HSmart_Loaded;
            }

            // 绘制矩形按钮的响应
            if (GetTemplateChild("PART_Rectangle") is Button btnRect)
            {
                btnRect.Click += BtnRect_Click;
            }

            // 绘制椭圆按钮的响应
            if (GetTemplateChild("PART_Ellipse") is Button btnEllipse)
            {
                btnEllipse.Click += BtnEllipse_Click;
            }

            // 绘制圆按钮的响应
            if (GetTemplateChild("PART_Circle") is Button btnCircle)
            {
                btnCircle.Click += BtnCircle_Click;
            }

            if (GetTemplateChild("PART_Clear") is Button btnClear)
            {
                btnClear.Click += (s, e) =>
                {
                    DrawingObjectList.Clear();
                    hWindow.ClearWindow();
                    Display(Image);
                };
            }

            base.OnApplyTemplate();
        }
        #endregion

        #region 图形绘制的具体实现
        // 绘制矩形的具体实现，并将坐标存入数组
        private async void BtnRect_Click(object sender, RoutedEventArgs e)
        {
            HTuple row1 = new HTuple();
            HTuple column1 = new HTuple();
            HTuple row2 = new HTuple();
            HTuple column2 = new HTuple();
            HObject drawObj = null;
            txtMsg.Text = "按鼠标左键绘制，右键结束。";

            // 另外开辟一个线程进行绘制操作
            await Task.Run(() =>
            {
                HOperatorSet.SetColor(hWindow, "blue");
                HOperatorSet.DrawRectangle1(hWindow, out row1, out column1, out row2, out column2);
                HOperatorSet.GenRectangle1(out drawObj, row1, column1, row2, column2);
            });

            if (drawObj == null) return;

            // 将绘制的图像的两个坐标存入数组
            DrawingObjectList.Add(new DrawingObjectInfo()
            {
                ShapeType = E_ShapeType.Rectangle,
                HTuples = new HTuple[] { row1, column1, row2, column2 }
            });
            // 清空文本，并显示对象
            txtMsg.Text = string.Empty;
            HOperatorSet.DispObj(drawObj, hWindow);
        }
        // 绘制椭圆的具体实现
        private async void BtnEllipse_Click(object sender, RoutedEventArgs e)
        {
            HTuple row = new HTuple();
            HTuple column = new HTuple();
            HTuple phi = new HTuple();
            HTuple radius1 = new HTuple();
            HTuple radius2 = new HTuple();
            HObject drawObj = null;
            txtMsg.Text = "按鼠标左键绘制，右键结束。";

            // 另外开辟一个线程进行绘制操作
            await Task.Run(() =>
            {
                HOperatorSet.SetColor(hWindow, "yellow");
                HOperatorSet.DrawEllipse(hWindow, out row, out column, out phi, out radius1, out radius2);
                HOperatorSet.GenEllipse(out drawObj, row, column, phi, radius1, radius2);
            });

            if (drawObj == null) return;

            // 将绘制的图像的两个坐标存入数组
            DrawingObjectList.Add(new DrawingObjectInfo()
            {
                ShapeType = E_ShapeType.Ellipse,
                HTuples = new HTuple[] { row, column, phi, radius1, radius2 }
            });
            // 清空文本，并显示对象
            txtMsg.Text = string.Empty;
            HOperatorSet.DispObj(drawObj, hWindow);
        }
        // 绘制圆的具体实现
        private async void BtnCircle_Click(object sender, RoutedEventArgs e)
        {
            HTuple row = new HTuple();
            HTuple column = new HTuple();
            HTuple radius = new HTuple();
            HObject drawObj = null;
            txtMsg.Text = "按鼠标左键绘制，右键结束。";

            // 另外开辟一个线程进行绘制操作
            await Task.Run(() =>
            {
                HOperatorSet.SetColor(hWindow, "red");
                HOperatorSet.DrawCircle(hWindow, out row, out column, out radius);
                HOperatorSet.GenCircle(out drawObj, row, column, radius);
            });

            if (drawObj == null) return;

            // 将绘制的图像的两个坐标存入数组
            DrawingObjectList.Add(new DrawingObjectInfo()
            {
                ShapeType = E_ShapeType.Circle,
                HTuples = new HTuple[] { row, column, radius }
            });
            // 清空文本，并显示对象
            txtMsg.Text = string.Empty;
            HOperatorSet.DispObj(drawObj, hWindow);
        }
        #endregion

        // 图片加载成功后需要在内部进行初始化
        private void HSmart_Loaded(object sender, RoutedEventArgs e)
        {
            hWindow = this.hSmart.HalconWindow;
        }
    }
}
