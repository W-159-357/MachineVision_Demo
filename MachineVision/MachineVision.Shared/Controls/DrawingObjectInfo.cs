using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Shared.Controls
{
    public enum E_ShapeType
    {
        Rectangle,
        Ellipse,
        Circle
    }

    public class DrawingObjectInfo
    {
        public E_ShapeType ShapeType { get; set; }
        public HTuple[] HTuples { get; set; }
    }
}
