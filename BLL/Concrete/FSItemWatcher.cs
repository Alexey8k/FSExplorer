using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class FSItemWatcher : IFSItemWatcher
    {
        private AutoResetEvent _resetEvent = new AutoResetEvent(false);
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private string _itemPath;
        private IFSItemCollection _itemCollection;
        private IFSExplorer _explorer;

        public FSItemWatcher(string itemPath, IFSItemCollection itemCollection, IFSExplorer explorer)
        {
            _itemPath = itemPath;
            _itemCollection = itemCollection;
            _explorer = explorer;
        }

        public void BeginWatch()
        {
            _resetEvent.Reset();
            _cancellationTokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
            {
                using (var watcher = new FileSystemWatcher(_itemPath))
                {
                    watcher.NotifyFilter = NotifyFilters.LastWrite
                                         | NotifyFilters.FileName
                                         | NotifyFilters.DirectoryName;

                    watcher.IncludeSubdirectories = false;
                    watcher.Filter = "";

                    watcher.Changed += OnChanged;
                    watcher.Created += OnCreated;
                    watcher.Deleted += OnDeleted;
                    watcher.Renamed += OnRenamed;

                    
                    watcher.EnableRaisingEvents = true;

                    Debug.WriteLine("START ---- " + _itemPath);
                    _resetEvent.WaitOne();
                    Debug.WriteLine("STOP ---- " + _itemPath);

                }
            }, _cancellationTokenSource.Token);
        }

        public void EndWatch()
        {
            _resetEvent.Set();
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        #region EventHandlers

        private void OnChanged(object sender, FileSystemEventArgs args) => _itemCollection[args.FullPath]?.ValidateItemState();

        private void OnCreated(object sender, FileSystemEventArgs args) => _itemCollection.Add(_explorer.GetFSItem(args.FullPath));

        private void OnDeleted(object sender, FileSystemEventArgs args)
        {
            BeforeDeleteStopManagement(args.FullPath);
            _itemCollection.Remove(args.FullPath);
        }

        private void OnRenamed(object sender, RenamedEventArgs args)
        {
            BeforeDeleteStopManagement(args.OldFullPath);
            if (_itemCollection.Remove(args.OldFullPath))
                _itemCollection.Add(_explorer.GetFSItem(args.FullPath));
        }

        #endregion // EventHandlers

        private void BeforeDeleteStopManagement(string itemPath)
        {
            var itemManager = _itemCollection[itemPath];
            if (itemManager.IsManagement)
                itemManager.EndManagement();
        }
    }
}
