using System.Collections.Generic;

namespace WearinessExam.DO
{
    /// <summary>
    /// Class BaseValueComparer
    /// </summary>
    public class BaseValueComparer : IComparer<BaseValue>
    {

        /// <summary>
        /// Compares the BaseValue.
        /// </summary>
        /// <param name="x">The x BaseValue.</param>
        /// <param name="y">The y BaseValue.</param>
        /// <returns>result</returns>
        public int Compare(BaseValue x, BaseValue y)
        {
            int result = -1;
            if (x.CFF == y.CFF)
            {
                result = 0;
            }
            return result;
        }
    }
}
