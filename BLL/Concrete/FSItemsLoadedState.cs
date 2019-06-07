using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Abstract;

namespace BLL.Concrete
{
    internal sealed class FSItemsLoadedState : FSItemLoaderState
    {
        public FSItemsLoadedState(
            DateTime lastWriteTime,
            IFSExplorer explorer,
            IFSItemLoaderStateFactory loaderStateFactory)
            : base(explorer, loaderStateFactory)
            => _lastWriteTime = lastWriteTime;

        public sealed override void LoadItems(IFSItemLoader itemsLoader) => ValideteChildItemsStete(itemsLoader);
    }
}
