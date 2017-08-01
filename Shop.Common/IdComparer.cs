using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shop.DB;

namespace Shop.Common
{

    public class IdComparer : IEqualityComparer<clsFAinfo>
    {
        public bool Equals(clsFAinfo x, clsFAinfo y)
        {
            if (x == null)
                return y == null;
            return x.R_id == y.R_id;
        }

        public int GetHashCode(clsFAinfo obj)
        {
            if (obj == null)
                return 0;
            return obj.R_id.GetHashCode();
        }
      
    }
}
