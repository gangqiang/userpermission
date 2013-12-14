using System;
using System.Collections.Generic;
using System.Text;

namespace UserPermission.Model
{
    [Serializable]
    public class CompanyJsonModel
    {
        private int _companyid = 0;
        private string _companyname = string.Empty;
        private string _gropidn = string.Empty;

        public int CompanyId
        {
            get { return _companyid; }
            set { _companyid = value; }
        }

        public string CompanyName
        {
            get { return _companyname; }
            set { _companyname = value; }
        }

        public string GroupIdn
        {
            get { return _gropidn; }
            set { _gropidn = value; }
        }

    }
}
