using HalconDotNet;
using MachineVision.Shared.Extensions;
using System.Collections.ObjectModel;
using System.Data.Common;
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

            // 绘制区域的响应
            if (GetTemplateChild("PART_Region") is Button btnRegion)
            {
                btnRegion.Click += BtnRegion_Click;
            }

            // 清空按钮的响应
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
        private void BtnRect_Click(object sender, RoutedEventArgs e)
        {
            DrawShape(E_ShapeType.Rectangle, new HTuple(), new HTuple(), new HTuple(), new HTuple());
        }
        // 绘制椭圆的具体实现
        private void BtnEllipse_Click(object sender, RoutedEventArgs e)
        {
            DrawShape(E_ShapeType.Ellipse, new HTuple(), new HTuple(), new HTuple(), new HTuple(), new HTuple());
        }
        // 绘制圆的具体实现
        private void BtnCircle_Click(object sender, RoutedEventArgs e)
        {
            DrawShape(E_ShapeType.Circle, new HTuple(), new HTuple(), new HTuple());
        }
        // 绘制区域的具体实现
        private void BtnRegion_Click(object sender, RoutedEventArgs e)
        {
            DrawShape(E_ShapeType.Region);
        }

        /// <summary>
        /// 图像绘制和生成的具体实现（绘制不同形状）
        /// </summary>
        /// <param name="shapeType"></param>
        /// <param name="hTuples"></param>
        private async void DrawShape(E_ShapeType shapeType, params HTuple[] hTuples)
        {
            txtMsg.Text = "按鼠标左键绘制，右键结束。";
            HObject drawObj;
            HOperatorSet.GenEmptyObj(out drawObj);

            hSmart.HZoomContent = HSmartWindowControlWPF.ZoomContent.Off;

            // 另外开辟一个线程进行绘制操作
            await Task.Run(() =>
            {
                switch (shapeType)
                {
                    case E_ShapeType.Rectangle:
                        {
                            HOperatorSet.SetColor(hWindow, "blue");
                            HOperatorSet.DrawRectangle1(hWindow, out hTuples[0], out hTuples[1], out hTuples[2], out hTuples[3]);
                            drawObj = hTuples.GenRectangle();
                            break;
                        }
                    case E_ShapeType.Ellipse:
                        {
                            HOperatorSet.SetColor(hWindow, "yellow");
                            HOperatorSet.DrawEllipse(hWindow, out hTuples[0], out hTuples[1], out hTuples[2], out hTuples[3], out hTuples[4]);
                            drawObj = hTuples.GenEllipse();
                            break;
                        }
                    case E_ShapeType.Circle:
                        {
                            HOperatorSet.SetColor(hWindow, "red");
                            HOperatorSet.DrawCircle(hWindow, out hTuples[0], out hTuples[1], out hTuples[2]);
                            drawObj = hTuples.GenCircle();
                            break;
                        }
                    case E_ShapeType.Region:
                        {
                            // 绘制自定义区域
                            HOperatorSet.SetColor(hWindow, "red");
                            HOperatorSet.DrawRegion(out drawObj, hWindow);
                            break;
                        }
                }
            });

            if (drawObj != null)
            {
                DrawingObjectList.Add(new DrawingObjectInfo() { ShapeType = shapeType, HTuples = hTuples, Hobject = drawObj });
                HOperatorSet.DispObj(drawObj, hWindow);
            }

            txtMsg.Text = String.Empty;
            hSmart.HZoomContent = HSmartWindowControlWPF.ZoomContent.WheelForwardZoomsIn; 
        }
        #endregion



        // 图片加载成功后需要在内部进行初始化
        private void HSmart_Loaded(object sender, RoutedEventArgs e)
        {
            hWindow = this.hSmart.HalconWindow;
        }
    }
}
