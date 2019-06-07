using BLL.Abstract;
using BLL.Concrete;
using FSIcon;
using FSInfo;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    public class BLLModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IFSIconService>().To<FSIconService>().WhenInjectedInto(typeof(FSIconServiceProxy)).InSingletonScope();
            Bind<IFSIconService>().To<FSIconServiceProxy>().WhenInjectedInto(typeof(FSExplorer)).InSingletonScope();
            Bind<IFSItemInfoService>().To<FSItemInfoService>().InSingletonScope();
            Bind<IFSDriveService>().To<FSDriveService>().InSingletonScope();

            Bind<IFSItemLoaderStateFactory>().To<FSItemLoaderStateFactory>();
            Bind<IFSExplorer>().To<FSExplorer>().InSingletonScope();

            Bind<FSItemLoaderState>().To<FSItemsPreLoadState>();
            Bind<IFSItemLoader>().To<FSItemLoader>();
            Bind<IFSItemLoaderFactory>().ToFactory();

            Bind<IFSItemWatcher>().To<FSItemWatcher>();
            //Bind<IFSItemWatcherFactory>().ToFactory();

            Bind<IFSItemManager>().To<FSItemManager>();
            Bind<IFSItemManagerFactory>().ToFactory();
        }
    }
}
