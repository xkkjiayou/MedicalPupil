using System.Collections.Generic;

namespace MedicalSys.DataAccessor
{
    public class PatientComparer : IComparer<Patient>
    {
        public int Compare(Patient x, Patient y)
        {
            int result = -1;
            if (x.ID == y.ID && x.Name == y.Name && x.Sex == y.Sex && x.Unit == y.Unit &&
                x.Birth.Equals(y.Birth))
            {
                result = 0;
            }
            return result;

        }
    }
}
