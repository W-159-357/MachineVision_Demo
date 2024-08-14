using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace MachineVision.Core
{
    /// <summary>
    /// 弹窗基类
    /// </summary>
    public class DiglogViewModel : BindableBase, IDialogAware
    {
        public string Title { get; set; }

        public event Action<IDialogResult> RequestClose;

        /// <summary>
        /// 是否允许关闭，默认为true
        /// </summary>
        /// <returns></returns>
        public bool CanCloseDialog()
        {
            return true;
        }

        /// <summary>
        /// 关闭时触发
        /// </summary>
        public void OnDialogClosed()
        {

        }

        /// <summary>
        /// 打开前触发
        /// </summary>
        /// <param name="parameters"></param>
        public virtual void OnDialogOpened(IDialogParameters parameters)
        {

        }
    }
}
