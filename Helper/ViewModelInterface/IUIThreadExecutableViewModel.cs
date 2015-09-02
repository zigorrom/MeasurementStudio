using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Helper.ViewModelInterface
{
    public interface IUIThreadExecutableViewModel
    {
        void ExecuteInUIThread(Action action);
        Task ExecuteInUIThreadAsync(Action action);
        
    }
}
