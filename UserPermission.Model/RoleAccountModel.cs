using System;
using System.Collections.Generic;
using System.Text;

namespace UserPermission.Model
{
    public class RoleAccountModel
    {
        private int _accountid = 0;
        public int AccountId
        {
            get { return _accountid; }
            set { _accountid = value; }
        }

        private bool _ischecked = false;
        public bool IsChecked
        {
            get { return _ischecked; }
            set { _ischecked = value; }
        }

    }
}
