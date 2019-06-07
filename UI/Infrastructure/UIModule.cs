using BLL.Abstract;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Abstract;
using UI.Concrete;

namespace UI.Infrastructure
{
    public class UIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IFSItemCollection>().To<FSItemCollection>();
            Bind<IFSItemCollectionFactory>().ToFactory();

            Bind<IFSItemViewModelFactory>().To<FSItemViewModelFactory>().InSingletonScope();
        }
    }
}
