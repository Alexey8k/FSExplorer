using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SHFileInfo
{
    public class SHFInfo : IDisposable
    {
        private SHFILEINFO _info;

        public ref readonly SHFILEINFO GetSHFileInfo(string strPath, uint dwFileAttributes, SHGFI uFlags)
        {
            if (IntPtr.Zero == SHFI.SHGetFileInfo(strPath, dwFileAttributes, out _info, (uint)Marshal.SizeOf<SHFILEINFO>(), uFlags))
                throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());

            return ref _info;
        }

        #region IDisposable

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Освобождаем управляемые ресурсы
                }
                // освобождаем неуправляемые объекты
                if (_info.hIcon != IntPtr.Zero)
                {
                    SHFI.DestroyIcon(_info.hIcon);
                    _info.hIcon = IntPtr.Zero;
                }
                disposed = true;
            }
        }

        ~SHFInfo()
        {
            Dispose(false);
        }
        #endregion // IDisposable
    }
}
