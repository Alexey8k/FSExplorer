using SHFileInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSInfo
{
    public class FSItemInfo
    {
        public string Path { get; set; }
        public string DisplayName { get; set; }
        public string TypeName { get; set; }
        public FileAttributes FileAttributes { get; set; }
        public SFGAO Attributes { get; set; }
        public int IconIndex { get; set; }
    }
}
