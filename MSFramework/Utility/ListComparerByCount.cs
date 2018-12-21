using System.Collections;
using System.Collections.Generic;

namespace MedicalSys.Framework
{
    public class ListComparerByCount : IComparer<IList>
    {
        public int Compare(IList x, IList y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null && y != null)
            {
                return -1;
            }
            if (x != null && y == null)
            {
                return 1;
            }
           return x.Count < y.Count ? -1 : (x.Count == y.Count ? 0 : 1);

        }
    }
}
