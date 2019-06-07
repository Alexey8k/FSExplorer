﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHFileInfo
{
    [Flags]
    public enum SHGFI : int
    {
        // Получите дескриптор к значку, который представляет файл и индекс значка в списке образов системы. 
        // Дескриптор копируется в член hIcon структуры, указанной в psfi, а индекс копируется в член iIcon.
        Icon = 0x000000100,
        // Извлеките отображаемое имя для файла, то есть имя, которое отображается в проводнике Windows. Имя копируется в член szDisplayName структуры, 
        // указанной в psfi. Возвращаемое отображаемое имя использует длинное имя файла, если оно есть, а не форму 8.3 имени файла. Обратите внимание, 
        // что на отображаемое имя могут влиять такие параметры, как отображение расширений.
        DisplayName = 0x000000200,
        // Получить строку, которая описывает тип файла. Строка копируется в член szTypeName структуры, указанной в psfi.
        TypeName = 0x000000400,
        // Получить атрибуты элемента. Атрибуты копируются в член dwAttributes структуры, указанной в параметре psfi. 
        // Это те же атрибуты, которые получены из IShellFolder::GetAttributesOf.
        Attributes = 0x000000800,
        // Получите имя файла, который содержит значок, представляющий файл, указанный в pszPath , как возвращено методом IExtractIcon::GetIconLocation 
        // обработчика значка файла. Также получите индекс значка в этом файле. Имя файла, содержащего значок, копируется в член szDisplayName структуры, 
        // указанной в psfi. Индекс иконки копируется в элемент структуры iIcon.
        IconLocation = 0x000001000,
        // Получить тип исполняемого файла, если pszPath идентифицирует исполняемый файл. Информация упакована в возвращаемое значение. 
        // Этот флаг не может быть указан с другими флагами.
        ExeType = 0x000002000,
        // Получить индекс значка списка системных изображений. В случае успеха, индекс копируется в iIcon член Psfi. Возвращаемое значение 
        // является дескриптором списка системных изображений. Действительными являются только те изображения, индексы которых успешно скопированы 
        // в iIcon. Попытка получить доступ к другим изображениям в системном списке изображений приведет к неопределенному поведению.
        SysIconIndex = 0x000004000,
        // Измените SHGFI_ICON, заставив функцию добавить наложение ссылки на значок файла. SHGFI_ICON флаг также должен быть установлен.
        LinkOverlay = 0x000008000,
        // Измените SHGFI_ICON, заставив функцию смешивать значок файла с цветом подсветки системы. SHGFI_ICON флаг также должен быть установлен.
        Selected = 0x000010000,
        // Изменение SHGFI_ATTRIBUTES , чтобы указать , что dwAttributes член SHFILEINFO структуры при Psfi содержит конкретные атрибуты, 
        // которые желательны. Эти атрибуты передаются в IShellFolder :: GetAttributesOf . Если этот флаг не указан, 0xFFFFFFFF передается 
        // в IShellFolder::GetAttributesOf , запрашивая все атрибуты. Этот флаг не может быть указан с флагом SHGFI_ICON .
        AttrSpecified = 0x000020000,
        // Измените SHGFI_ICON, чтобы функция получала большой значок файла. SHGFI_ICON флаг также должен быть установлен.
        LargeIcon = 0x000000000,
        // Измените SHGFI_ICON, заставив функцию получить маленький значок файла. Также используется для изменения SHGFI_SYSICONINDEX, 
        // в результате чего функция возвращает дескриптор в системный список изображений, который содержит изображения небольших значков. 
        // SHGFI_ICON и / или SHGFI_SYSICONINDEX флаг также должен быть установлен.
        SmallIcon = 0x000000001,
        // Измените SHGFI_ICON, заставив функцию получить значок открытия файла. Также используется для изменения SHGFI_SYSICONINDEX, 
        // в результате чего функция возвращает дескриптор в системный список изображений, который содержит маленький значок открытия файла. 
        // Контейнерный объект отображает значок открытия, чтобы указать, что контейнер открыт. SHGFI_ICON и / или SHGFI_SYSICONINDEX флаг 
        // также должен быть установлен.
        OpenIcon = 0x000000002,
        // Измените SHGFI_ICON, чтобы функция получала значок размером с оболочку. Если этот флаг не указан, функция изменяет размер иконки 
        // в соответствии со значениями метрики системы. SHGFI_ICON флаг также должен быть установлен.
        ShellIconSize = 0x000000004,
        // Укажите, что pszPath - это адрес структуры ITEMIDLIST, а не имя пути.
        PIDL = 0x000000008,
        // Указывает, что функция не должна пытаться получить доступ к файлу, указанному в pszPath . Скорее, он должен действовать так, как будто файл, 
        // указанный в pszPath, существует с атрибутами файла, переданными в dwFileAttributes. Этот флаг нельзя комбинировать с флагами 
        // SHGFI_ATTRIBUTES, SHGFI_EXETYPE или SHGFI_PIDL.
        UseFileAttributes = 0x000000010,
        // Версия 5.0 . Примените соответствующие пометки к значку файла. SHGFI_ICON флаг также должен быть установлен.
        AddOverlays = 0x000000020,
        // Версия 5.0. Вернуть индекс значка наложения. Значение индекса наложения возвращается в верхних восьми битах элемента iIcon структуры, 
        // указанной в psfi. Этот флаг требует, чтобы SHGFI_ICON также был установлен.
        OverlayIndex = 0x000000040,
    }
}
