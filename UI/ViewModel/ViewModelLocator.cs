using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BLL.Infrastructure;
using UI.Infrastructure;

namespace UI.ViewModel
{
    internal class ViewModelLocator
    {
        public FSExplorerViewModel FSExplorerViewModel => _kernel.Get<FSExplorerViewModel>();

        private static readonly IKernel _kernel = new StandardKernel();

        static ViewModelLocator()
        {
            _kernel.Load(
                new AutoMapperModule(),
                new BLLModule(),
                new UIModule());
        }
    }
}
