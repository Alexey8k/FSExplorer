using AutoMapper;
using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class FSDriveService : IFSDriveService
    {
        private IMapper _mapper;
        public FSDriveService(IMapper mapper)
        {
            _mapper = mapper;
        }

        #region IFSDriveService

        public IEnumerable<string> GetDrives(DriveTypes driveTypes) => Directory.GetLogicalDrives()
            .Where(drive => driveTypes.HasFlag(_mapper.Map<DriveTypes>(GetDriveType(drive))));

        #endregion

        private DriveType GetDriveType(string nameDrive) => (DriveType)Kernel32dll.GetDriveType(nameDrive);

        #region API

        private abstract class Kernel32dll
        {
            [DllImport("kernel32.dll", EntryPoint = "GetDriveTypeA")]
            public static extern int GetDriveType(string lpRootPathName);
        }

        #endregion
    }
}