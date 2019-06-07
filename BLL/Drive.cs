using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    // есть в System.IO.DriveType
    public enum Drive
    {
        // Тип привода не может быть определен.
        Unknown,
        // Корневой путь недействителен; Например, по указанному пути том не подключен.
        NoRootDir,
        // Диск имеет съемный носитель; например, дисковод гибких дисков, флэш-накопитель или устройство чтения флэш-карт.
        Removable,
        // Диск имеет фиксированный носитель; например, жесткий диск или флешка.
        Fixed,
        // Диск является удаленным (сетевым).
        Remote,
        // Диск представляет собой привод CD-ROM.
        CDRom,
        // Диск является RAM-диском.
        RAMDisk
    }
}
