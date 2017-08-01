using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shop.DB;

namespace Shop.Common
{

    public class FenDianIdComparer : IEqualityComparer<clsFA_FenDianinfo>
    {
      
        public bool Equals(clsFA_FenDianinfo x, clsFA_FenDianinfo y)
        {
            if (x == null)
                return y == null;
            return x.R_id == y.R_id;
        }

        public int GetHashCode(clsFA_FenDianinfo obj)
        {
            if (obj == null)
                return 0;
            return obj.R_id.GetHashCode();
        }
    }
}
