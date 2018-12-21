
namespace WearinessExam
{
    public class ConstMessage
    {

        public const string COLUMN_ID = "ID";
        public const string COLUMN_NAME = "姓名";
        public const string COLUMN_SEX = "性别";
        public const string COLUMN_BIRTHDAY = "出生日期";
        public const string COLUMN_UNIT = "单位";
        public const string COLUMN_EXAM_SCENARIO = "检测情景";
        public const string COLUMN_EXAM_DATETIME = "检测时间";
        public const string COLUMN_EXAM_PARAMS = "检测参数";
        public const string COLUMN_CFF = "CFF";
        public const string COLUMN_SL = "SL";
        public const string COLUMN_SV = "SV";
        public const string COLUMN_PID = "PID";
        public const string COLUMN_PCV = "PCV";
        public const string COLUMN_PCL = "PCL";
        public const string COLUMN_PCR = "PCR";
        public const string COLUMN_AWI = "AWI";
        public const string COLUMN_EXAM_KEY = "EXAM_KEY";
        public const string COLUMN_PATIENT_KEY = "PATIENT_KEY";
        public const string COLUMN_CFF1 = "CFF1";
        public const string COLUMN_CFF2 = "CFF2";
        public const string DB_COLUMN_SCENARIO = "Scenario";
        public const string DB_COLUMN_PATIENTNAME = "PatientName";
        public const string DB_COLUMN_PATIENTID = "PatientId";

        public const string MESSAGE_M1101 = "检测信息不存在！";
        public const string MESSAGE_M1102 = "确定要删除检测信息吗";
        public const string MESSAGE_M1103 = "导入文件格式不正确，请重新选择。";
        public const string MESSAGE_M1105 = "请选择同一个人的检测数据！";
        public const string MESSAGE_M1106 = "文件类型不正确请重新选择。";
        public const string MESSAGE_M1107 = "文件路径不存在请重新选择。";
        public const string MESSAGE_M1108 = "请选择数据类型。";
        public const string MESSAGE_M1109 = "相同名称的文件已经存在，继续将覆盖原有的文件，是否要继续导出？";
        public const string MESSAGE_M1110 = "文件正在被使用，程序不能操作。请重新设置导出文件。";
       
        public const string MESSAGE_M1201 = "开始年龄不能大于终了年龄！";
        public const string MESSAGE_M1202 = "检测开始时间不能大于结束时间！";
        public const string MESSAGE_M1203 = "基础值被更改但尚未保存，关闭当前窗口基础值数据将不会保存，是否要关闭？";
        public const string MESSAGE_PATIENTDATA_ERROR = "导入受测员数据:ID {0}  姓名 {1} 数据不完整。";
        public const string MESSAGE_BASEVALUE_ERROR = "导入受测员数据:ID {0}   基础值数据不完整。";

        public const string MESSAGE_M1301 = "打印机 {0} 打开失败，请先配置打印机。";
        public const string MESSAGE_M1302 = "主程序文件不存在!";

        public const string MESSAGE_M1401 ="确认删除？";
        public const string MESSAGE_M1402 = "所选人员未完成全部检测，确认删除？";
        public const string MESSAGE_M1403 = "重复的受测员没有被添加到检测列表。";
        public const string MESSAGE_M1404 = "请先选择受测员。";
        public const string MESSAGE_M1405 = "系统自检未通过！请尝试重启系统或联系维修人员！";
        public const string MESSAGE_M1406 = "请输入医生结论。";
        public const string TITLE_M1405 = "瞳孔分析仪系统自检";

        public const string TEMPLATE_NAME = "WearinessTemplate.xls";
        public const string PATIENTDATA_SHEET_NAME = "个人信息";
        public const string BASEVALUE_SHEET_NAME = "基础值";
        public const string EXAM_DATA_SHEET_NAME = "检测值";
        public const string EXAM_ORIGINAL_DATA_SHEET_NAME = "原始数据";

        public const string APP_NAME = "WearinessExam";



        public const string MESSAGE_M1003 = "CFF（低到高）检测";
        public const string MESSAGE_M1004 = "CFF（高到低）检测";
        public const string MESSAGE_M1005 = "未追踪到瞳孔，请确认受测人员已就位！";
        public const string MESSAGE_M1006 = "该人员此项检测状态为已完成，是否重新进行此项检测？";
        public const string MESSAGE_M1007 = "时间超过30s，检测自动停止，是否保存本次检测数据？";
        public const string MESSAGE_M1008 = "瞳孔跟踪失败，检测终止，请确认受测人员已就绪并重新进行瞳孔追踪。";
        public const string MESSAGE_M1009 = "CFF低到高检测启动失败！";
        public const string MESSAGE_M1010 = "CFF高到低检测启动失败！";
        public const string MESSAGE_M1011 = "瞳孔追踪启动失败！";
        public const string MESSAGE_M1012 = "眼扫视检测启动失败！";
        public const string MESSAGE_M1013 = "瞳孔对光反应检测启动失败！";
        public const string MESSAGE_M1016 = "准备对{0}进行检测。";
        public const string MESSAGE_M1018 = "准备对{0}再次检测。";
        public const string MESSAGE_M1017 = "确定要退出吗？";

        //状态
        public const string STATUS_READY = "就绪";
        public const string STATUS_PUPIL_TRACK = "正在追踪瞳孔";
        //public const string STATUS_PUPIL_TRACK_SUCCESS = "追踪到瞳孔";
        //public const string STATUS_RUPIL_TRACK_FAILED = "未追踪到瞳孔";
        public const string STATUS_PUPIL_TRACK_SUCCESS = "";
        public const string STATUS_RUPIL_TRACK_FAILED = "";
        public const string STATUS_EYE_SCAN = "扫视速率检测";
        public const string STATUS_PUPIL_EXAM = "瞳孔对光反应检测";

        public const string STATUS_NOT_START = "未开始";
        public const string STATUS_COMPLETED = "已完成";

    }
}
