using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using MedicalSys.Framework;

namespace WearinessExam.Examination
{
    /// <summary>
    /// Class DataFileHelper
    /// </summary>
    public class DataFileHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataFileHelper"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public DataFileHelper(string fileName)
        {
            string filePath = Path.Combine(Path.Combine(Application.StartupPath, "Data"), fileName);

            XmlFilePath = filePath;
        }

        /// <summary>
        /// Gets or sets the XML file path.
        /// </summary>
        /// <value>The XML file path.</value>
        public string XmlFilePath { get; set; }

        /// <summary>
        /// Checks the file exist.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns><c>true</c> if success, <c>false</c> otherwise</returns>
        public static bool CheckFileExist(string fileName)
        {
            string filePath = Path.Combine(Path.Combine(Application.StartupPath, "Data"), fileName);

            return File.Exists(filePath);
        }

        /// <summary>
        /// Creates the data file.
        /// </summary>
        /// <param name="patientId">The patient id.</param>
        /// <returns>System.String.</returns>
        public static string CreateDataFile(string patientId)
        {
            string dataFileName = patientId + DateTime.Now.ToString("_yyyyMMddHHmmssff") + ".xml";
            string destFileName = Path.Combine(Path.Combine(Application.StartupPath, "Data"), dataFileName);
            string sourceFileName = Path.Combine(Path.Combine(Application.StartupPath, "Data"), "DataTemplate.xml");

            File.Copy(sourceFileName, destFileName);
            return destFileName;
        }

        /// <summary>
        /// Gets the CFF low2 high serials.
        /// </summary>
        /// <returns>System.Double[][].</returns>
        public double[] GetCffLow2HighSerials()
        {
            return GetNodeValues("CffLow2High");
        }

        /// <summary>
        /// Gets the CFF high2 low serials.
        /// </summary>
        /// <returns>System.Double[][].</returns>
        public double[] GetCffHigh2LowSerials()
        {
            return GetNodeValues("CffHigh2Low");
        }

        /// <summary>
        /// Gets the sv left serials.
        /// </summary>
        /// <returns>System.Double[][].</returns>
        public double[] GetSvLeftSerials()
        {
            return GetNodeValues("SvLeft");
        }

        /// <summary>
        /// Gets the sv right serials.
        /// </summary>
        /// <returns>System.Double[][].</returns>
        public double[] GetSvRightSerials()
        {
            return GetNodeValues("SvRight");
        }

        /// <summary>
        /// Gets the pd left serials.
        /// </summary>
        /// <returns>System.Double[][].</returns>
        public double[] GetPdLeftSerials()
        {
            return GetNodeValues("PdLeft");
        }

        /// <summary>
        /// Gets the pd right serials.
        /// </summary>
        /// <returns>System.Double[][].</returns>
        public double[] GetPdRightSerials()
        {
            return GetNodeValues("PdRight");
        }

        /// <summary>
        /// Saves the CFF low2 high.
        /// </summary>
        /// <param name="data">The data.</param>
        public void SaveCffLow2High(double[] data)
        {
            SaveXml(data, "CffLow2High");
        }

        /// <summary>
        /// Saves the CFF high2 low.
        /// </summary>
        /// <param name="data">The data.</param>
        public void SaveCffHigh2Low(double[] data)
        {
            SaveXml(data, "CffHigh2Low");
        }

        /// <summary>
        /// Saves the sv left.
        /// </summary>
        /// <param name="data">The data.</param>
        public void SaveSvLeft(double[] data)
        {
            SaveXml(data, "SvLeft");
        }

        /// <summary>
        /// Saves the sv right.
        /// </summary>
        /// <param name="data">The data.</param>
        public void SaveSvRight(double[] data)
        {
            SaveXml(data, "SvRight");
        }

        /// <summary>
        /// Saves the pd left.
        /// </summary>
        /// <param name="data">The data.</param>
        public void SavePdLeft(double[] data)
        {
            SaveXml(data, "PdLeft");
        }

        /// <summary>
        /// Saves the pd right.
        /// </summary>
        /// <param name="data">The data.</param>
        public void SavePdRight(double[] data)
        {
            SaveXml(data, "PdRight");
        }


        /// <summary>
        /// Saves the XML.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="xpath">The xml path.</param>
        private void SaveXml(double[] data, string xpath)
        {
            string value = ConnectWithComma(data);

            XmlDocument xdocData = new XmlDocument();
            xdocData.Load(XmlFilePath);
            XmlNode objDataNode = xdocData.DocumentElement.SelectSingleNode(xpath);
            objDataNode.InnerText = value;
            xdocData.Save(XmlFilePath);
        }

        /// <summary>
        /// Gets the node values.
        /// </summary>
        /// <param name="xpath">The xml path.</param>
        /// <returns>System.Double[].</returns>
        private double[] GetNodeValues(string xpath)
        {
            XmlDocument xdocData = new XmlDocument();
            xdocData.Load(XmlFilePath);
            XmlNode objDataNode = xdocData.DocumentElement.SelectSingleNode(xpath);
            string text = objDataNode.InnerText;
            double[] arrDouble = Convert2Array(text);
            return arrDouble;
        }

        /// <summary>
        /// Convert string to array.
        /// </summary>
        /// <param name="data">The string data.</param>
        /// <returns>System.Double[].</returns>
        private double[] Convert2Array(string data)
        {
            double[] result = null;
            if (!string.IsNullOrEmpty(data))
            {
                result = Array.ConvertAll<string, double>(data.Split(','), s => double.Parse(s));
            }
            return result;
        }

        /// <summary>
        /// Connects the with comma.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.String.</returns>
        private string ConnectWithComma(double[] data)
        {
            string result = string.Empty;
            if (data != null && data.Length > 0)
            {
                result = string.Join(",", data);
            }
            return result;
        }

        /// <summary>
        /// Saves the pupil exam data.
        /// </summary>
        /// <param name="pupilExamData">The pupil exam data.</param>
        public void SavePupilExamData(PupilExamData pupilExamData)
        {
            XmlDocument xdocData = new XmlDocument();
            xdocData.Load(XmlFilePath);

            XmlNode leftDataNode = xdocData.DocumentElement.SelectSingleNode("PdLeft");
            string leftValue = ConnectWithComma(pupilExamData.PdLeftData);
            leftDataNode.InnerText = leftValue;

            XmlNode rightDataNode = xdocData.DocumentElement.SelectSingleNode("PdRight");
            string rightValue = ConnectWithComma(pupilExamData.PdRightData);
            rightDataNode.InnerText = rightValue;

            xdocData.Save(XmlFilePath);
        }

    }
}
