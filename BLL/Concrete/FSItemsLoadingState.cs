using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Abstract;
using BLL.Model;

namespace BLL.Concrete
{
    internal sealed class FSItemsLoadingState : FSItemLoaderState
    {
        private IEnumerator<FSItemModel> _enumerator;

        public FSItemsLoadingState(
            DateTime lastWriteTime,
            IFSExplorer explorer,
            IEnumerator<FSItemModel> enumerator,
            IFSItemLoaderStateFactory loaderStateFactory)
            : base(explorer, loaderStateFactory)
        {
            _lastWriteTime = lastWriteTime;
            _enumerator = enumerator;
        }

        public sealed override void LoadItems(IFSItemLoader itemsLoader)
        {
            ValideteChildItemsStete(itemsLoader);
            if (!AddItems(itemsLoader, _enumerator))
                itemsLoader.ItemsLoaderState = _loaderStateFactory.CreateLoadedItemsState(_explorer.GetLastWriteTime(itemsLoader.ItemPath));
        }


    }
}
