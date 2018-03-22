using System;

namespace PlariumThreadsTest
{
    [Serializable]
    public class Info
    {
        //имя, дата создания, дата модификации, дата последнего доступа, атрибуты, размер, 
        //владелец, а также допустимые права (запись, чтение, удаление и т.п.)
        public String Name;
        public String FullName;
        public Boolean IsDirectory;
        public DateTime DateOfCreate;
        public DateTime DateOfModification;
        public DateTime DateOfLastAccess;
        public long SizeInBytes;
        
        public Boolean IsSystem;
        public Boolean IsReadOnly;
        public Boolean IsArchive;
        public Boolean IsCompressed;
        public Boolean IsHidden;
        public Boolean IsTemporary;

        public String Owner;
        public Boolean IsCurrentUserCanRead;
        public Boolean IsCurrentUserCanModify;
        public Boolean IsCurrentUserCanChangePermissions;
    }
}
