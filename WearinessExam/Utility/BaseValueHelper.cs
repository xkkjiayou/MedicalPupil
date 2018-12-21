using MedicalSys.Framework;
using WearinessExam.DO;

namespace WearinessExam.Utility
{
    /// <summary>
    /// Class BaseValueHelper
    /// </summary>
    public class BaseValueHelper
    {
        /// <summary>
        /// Gets the default base value.
        /// </summary>
        /// <returns>BaseValue.</returns>
        public static BaseValue GetDefaultBaseValue()
        {
            BaseValue defaultBaseVaule = new BaseValue();
            // 从INI文件中读取默认的基础值
            defaultBaseVaule.CFF = IniSettingConfig.GetInstance().CFF;
            return defaultBaseVaule;
        }
    }
}
