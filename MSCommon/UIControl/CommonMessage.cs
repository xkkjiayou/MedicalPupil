using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSys.MSCommon
{
    public class CommonMessage
    {
        /// <summary>
        /// The M0001
        /// </summary>
        public const string M0001 = "系统启动失败，请关闭现有运行中程序后再启动！";
        /// <summary>
        /// The M0002
        /// </summary>
        public const string M0002 = "系统启动失败，请检查环境设置后再启动！";
        /// <summary>
        /// The M0003
        /// </summary>
        public const string M0003 = "帐号不存在！";
        /// <summary>
        /// The M0004
        /// </summary>
        public const string M0004 = "用户名或密码输入不正确，请重试！";
        /// <summary>
        /// The M0005
        /// </summary>
        public const string M0005 = "数据输入不正确，请检查后再试！";
        /// <summary>
        /// The M0006
        /// </summary>
        public const string M0006 = "数据已经被修改，关闭当前窗口后数据将不会保存，是否要关闭？";
        /// <summary>
        /// The M0007
        /// </summary>
        public const string M0007 = "受测员不存在！";
        /// <summary>
        /// The M0008
        /// </summary>
        public const string M0008 = "确认删除受测员及其检查信息吗？";
        /// <summary>
        /// The M0009
        /// </summary>
        public const string M0009 = "数据库连接失败！";
        /// <summary>
        /// The M0010
        /// </summary>
        public const string M0010 = "确认删除用户信息吗？";
        /// <summary>
        /// The M0011
        /// </summary>
        public const string M0011 = "不能删除当前用户！";

        /// <summary>
        /// The M0012
        /// </summary>
        public const string M0012 = "开始年龄不能大于终了年龄！";

        /// <summary>
        /// The M0013
        /// </summary>
        public const string M0013 = "密码和确认密码不一致！";

        /// <summary>
        /// The M0014
        /// </summary>
        public const string M0014 = "登录名重复！";

        /// <summary>
        /// The M0015
        /// </summary>
        public const string M0015 = "用户ID重复！";

        /// <summary>
        /// The M0016
        /// </summary>
        public const string M0016 = "请输入用户名和密码！";

        /// <summary>
        /// The M0017
        /// </summary>
        public const string M0017 = "出生日期不允许比现在的时间大！";

        /// <summary>
        /// The M0018
        /// </summary>
        public const string M0018 = "受测员ID重复。";

        /// <summary>
        /// The M0019
        /// </summary>
        public const string M0019 = "用户密码没有修改。";

        /// <summary>
        /// The L0001
        /// </summary>
        public const string L0001 = "用户验证成功，进入主画面";
    }
}
