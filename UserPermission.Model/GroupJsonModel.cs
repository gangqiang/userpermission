using System;
using System.Collections.Generic;
using System.Text;

namespace UserPermission.Model
{
    [Serializable]
    public class GroupJsonModel
    {
        private string _groupid = "";
        private string _groupname = string.Empty;
        private string _gropidn = string.Empty;

        public string GroupId
        {
            get { return _groupid; }
            set { _groupid = value; }
        }

        public string GroupName
        {
            get { return _groupname; }
            set { _groupname = value; }
        }

        public string GroupIdn
        {
            get { return _gropidn; }
            set { _gropidn = value; }
        }
    }
}
