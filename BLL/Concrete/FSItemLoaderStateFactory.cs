using BLL.Abstract;
using BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    internal class FSItemLoaderStateFactory : IFSItemLoaderStateFactory
    {
        private IFSExplorer _explorer;
        public FSItemLoaderStateFactory(IFSExplorer explorer)
        {
            _explorer = explorer;
        }

        public FSItemLoaderState CreateLoadedItemsState(DateTime lastWriteTime)
            => new FSItemsLoadedState(lastWriteTime, _explorer, this);

        public FSItemLoaderState CreateLoadingItemsState(IEnumerator<FSItemModel> enumerator, DateTime lastWriteTime)
            => new FSItemsLoadingState(lastWriteTime, _explorer, enumerator, this);

        public FSItemLoaderState CreateLoadItemsState(DateTime lastWriteTime)
            => new FSItemsLoadState(lastWriteTime, _explorer, this);

        public FSItemLoaderState CreatePreLoadItemsState()
             => new FSItemsPreLoadState(_explorer, this);
    }
}
