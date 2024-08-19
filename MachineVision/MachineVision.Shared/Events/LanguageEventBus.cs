using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Shared.Events
{
    /// <summary>
    /// 语言更改事件
    /// </summary>
    public class LanguageEventBus : PubSubEvent<bool>
    {
    }
}
