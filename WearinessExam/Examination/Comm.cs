using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WearinessExam.Examination
{
    public class Comm
    {
        public delegate void EventHandle(byte[] readBuffer);
        public event EventHandle DataReceived;

        public SerialPort comm;
        Thread thread;
        volatile bool _keepReading;
        private const int B1 = (int)'7';


        public Comm()
        {
            comm = new SerialPort();
            thread = null;
            _keepReading = false;
        }

        // 串口自动连接，从注册表当中获得当前可用的串口号
        public bool getComPort()
        {
            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);

            if (ports.Length > 0)
            {
                comm.PortName = ports[0];
                return true;
            }
            else
            {
                return false;
            }
        }

        public void setComPort(string portName)
        {
            comm.PortName = portName;
        }

        // 通信一次
        public bool OnCommunicate()
        {
            Close();

            //波特率
            comm.BaudRate = 115200;
            //数据位
            comm.DataBits = 8;
            //停止位
            comm.StopBits = StopBits.One;
            //校验位
            comm.Parity = Parity.None;
            //comm.ReadTimeout = 100;
            try
            {
                comm.Open();
            }
            catch (Exception ex)
            {
                //捕获到异常信息，创建一个新的comm对象，之前的不能用了。   
                comm = new SerialPort();
                IsCommflag = false;
                //现实异常信息给客户。   
                MessageBox.Show(ex.Message);
                return false;
            }

            if (comm.IsOpen)
            {
                //StartReading();
            }
            else
            {
                IsCommflag = false;
                MessageBox.Show("串口打开失败！");
                return false;
            }

            comm.Write("ok");
            Thread.Sleep(20);
            byte[] readBuf = new byte[comm.BytesToRead];
            int i = comm.Read(readBuf, 0, comm.BytesToRead);
            string readStr = Encoding.UTF8.GetString(readBuf);

            if(readStr.Equals("ok"))
            {
                IsCommflag = true;
                return true;
            }
            else
            {
                IsCommflag = false;
                Close();
                return false;
            }
        }


        // 判断是否通信
        public bool IsCommflag { get; set; }

        // 关闭串口，同时也关闭关联线程
        public bool Close()
        {
            StopReading();

            if (comm.IsOpen)
            {
                //打开时点击，则关闭串口   
                comm.Close();
            }

            return true;
        }

        public void QueRen_But(int colour, int rate1, int rate2, int Liangheibi, int LDLiangdu)
        {
            IsCommflag = true;

            byte[] sendData = { 0xFE, B1, 0, (byte)colour, (byte)rate1, (byte)rate2, (byte)LDLiangdu, (byte)Liangheibi, 0xFF };
            WritePort(sendData);
        }

        public void QueRen_But1(int BJLiangdu, int BJColor, int BJTime, int mode)
        {
            IsCommflag = true;

            if (mode == 0)
            {
                if (BJColor == 0)
                {
                    byte[] sendData = { 0xFE, B1, 1, 0, 0, 0, 0, 0, 0xFF };
                    WritePort(sendData);
                }
                else
                {
                    byte[] sendData = { 0xFE, B1, 1, (byte)BJColor, 0, 0, (byte)BJLiangdu, 0, 0xFF };

                    WritePort(sendData);
                }
            }
            else //触发模式
            {
                byte[] sendData = { 0xFE, B1, 1,(byte)BJColor, (byte)((BJTime & 0xff00) >> 8), (byte)(BJTime & 0x00ff), (byte)BJLiangdu, 1, 0xFF };
                WritePort(sendData);
            }
        }
        public void QueRen_But2(int HWLianghen)
        {
            IsCommflag = true;

            if (HWLianghen == 0)
            {
                byte[] sendData = { 0xFE, B1, 2, 0, 0, 0, 0, 0, 0xFF };
                WritePort(sendData);
            }
            else
            {
                byte[] sendData = { 0xFE, B1, 2, 2, 0, 0, 0, (byte)HWLianghen, 0xFF };
                WritePort(sendData);
            }
        }

        public void Check_But()
        {
            IsCommflag = true;

            byte[] sendData = { 0xFE, B1, 0, 6, 0, 0, 0, 0, 0xFF };
            WritePort(sendData);
        }

        //相机帧率设置（由控制板触发）
        public void camerTrigger(int triggerAcquisition, int camerFrame)
        {
            IsCommflag = true;

            byte[] sendData = { 0xFE, B1, 3, (byte)triggerAcquisition, (byte)((camerFrame & 0xff00) >> 8), (byte)(camerFrame & 0x00ff), 0, 0, 0xFF };
            WritePort(sendData);
        }

        //亮度精调
        public void setupLight(int setType, int lightValue)
        {
            IsCommflag = true;

            if (setType == 0) //查询
            {
                byte[] sendData = { 0xFE, B1, 4, 0, 0, 0, 0, 0, 0xFF };
                WritePort(sendData);
            }
            else //设置亮度
            {
                byte[] sendData = { 0xFE, B1, 4, (byte)setType, 0, 0, (byte)((lightValue & 0xff00) >> 8), (byte)(lightValue & 0x00ff), 0xFF };
                WritePort(sendData);
            }
        }


        // 清除发送缓冲区
        public void ClearOutputBuffer()
        {
            comm.DiscardOutBuffer();
        }

        // 清除接受缓冲区
        public void ClearInputBuffer()
        {
            comm.DiscardInBuffer();
        }

        private void StartReading()
        {
            if (!_keepReading)
            {
                _keepReading = true;
                thread = new Thread(new ThreadStart(ReadPort));
                thread.Start();
            }
        }

        private void StopReading()
        {
            if (_keepReading)
            {
                _keepReading = false;
                thread.Join();
                thread = null;
            }
        }

        private void ReadPort()
        {
            while (_keepReading)
            {
                if (comm.IsOpen)
                {
                    int count = comm.BytesToRead;
                    if (count > 0)
                    {
                        byte[] readBuffer = new byte[count];
                        try
                        {
                            Application.DoEvents();
                            comm.Read(readBuffer, 0, count);
                            if (DataReceived != null)
                            {
                                DataReceived(readBuffer);
                            }
                            Thread.Sleep(100);
                        }
                        catch (TimeoutException)
                        {
                        }
                    }
                }
            }
        }

        public void ReadString(byte[] buffer, int bufferLength)
        {
            int i = comm.Read(buffer, 0, bufferLength);
            string readStr = Encoding.UTF8.GetString(buffer);
        }

        public void WritePort(byte[] sendData)
        {
            if (comm.IsOpen)
            {
                comm.Write(sendData, 0, sendData.Length);
            }
        }
    }
}
