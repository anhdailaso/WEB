using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Modules
{
    class Modules
    {
        private static int _iCapNhat;
        public static int iCapNhat
        {
            get
            {
                return _iCapNhat;
            }
            set
            {
                _iCapNhat = value;
            }
        }
    }
}
