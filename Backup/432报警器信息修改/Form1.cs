using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

using System.Text.RegularExpressions;

using ZKSmartDeviceLibrary;

namespace _432报警器信息修改
{
    public partial class Form1 : Form
    {

        List<ZKSmartGateway> devices; //所有设备列表
        ZKSmartGateway device; //当前选择的设备

        //临时用到的变量
        int count1 = 0;
        bool openStatus = false;
        int i = 1;
        string zl = "zl";
        //private SerialPort comm = new SerialPort();

        private SerialPort comm = new SerialPort();
        public Queue<byte> receiveQueue = new Queue<byte>(); //存储接收到的消息队列

        public void OpenComport(string ComportNum, int BaudRate)
        {
            comm.BaudRate = BaudRate;
            comm.PortName = ComportNum;
            comm.DataBits = 8;
            comm.StopBits = StopBits.One;
            comm.Parity = Parity.None;

            if (comm.IsOpen)
            {
                return;
            }

            try
            {
                comm.DataReceived += comm_DataReceived;

                comm.Open();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());

            }

        }

        public void CloseComport()
        {


            if (comm.IsOpen)
            {
                comm.Close();

            }



        }

        int j = 1;
        private void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //if (Closing) return;//如果正在关闭，忽略操作，直接返回，尽快的完成串口监听线程的一次循环

            try
            {
                //Listening = true;//设置标记，说明我已经开始处理数据，一会儿要使用系统UI的。  
                int n = comm.BytesToRead;//记录读取长度 
                byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据   
                //received_count += n;//增加接收计数   
                comm.Read(buf, 0, n);//读取缓冲数据  
                count1++;

                StringBuilder builder = new StringBuilder();

                //因为要访问ui资源，所以需要使用invoke方式同步ui。   

                //判断是否是显示为16禁止   

                //依次的拼接出16进制字符串   
                foreach (byte b in buf)
                {
                    //receiveCount += 1;
                    receiveQueue.Enqueue(b);

                    builder.Append(b.ToString("X2") + " ");
                }

                //处理温湿度数据
                this.Invoke((EventHandler)(delegate
                {
                    int cd = 0;//读取APN时用来截取的长度cd
                    if (buf.Length == 53 && buf[0] == 0xBC)
                    {

                        String BB_ID = buf[2].ToString("X2") + " " + buf[3].ToString("X2") + " " + buf[4].ToString("X2") + " " + buf[5].ToString("X2");
                        tbxIDcx.Text = BB_ID;
                        tbxIDsz.Text = buf[2].ToString("X2") + buf[3].ToString("X2") + buf[4].ToString("X2") + buf[5].ToString("X2");

                        string khID = buf[12].ToString("X2") + " " + buf[13].ToString("X2");
                        tbxKHIDcx.Text = khID;
                        tbxKHIDsz.Text = khID;

                        string debug = buf[14].ToString("X2") + " " + buf[15].ToString("X2");
                        tbxDebugcx.Text = debug;
                        tbxDebugSZ.Text = debug;

                        string category = buf[16].ToString("X2");
                        tbxCategoryCx.Text = category;
                        tbxCategorySZ.Text = category;

                        string UpIP = buf[19].ToString() + "." + buf[20].ToString() + "." + buf[21].ToString() + "." + buf[22].ToString();
                        tbxUpIPcx.Text = UpIP;
                        tbxUpIP1.Text = buf[19].ToString();
                        tbxUpIP2.Text = buf[20].ToString();
                        tbxUpIP3.Text = buf[21].ToString();
                        tbxUpIP4.Text = buf[22].ToString();

                        string sjjg = (buf[17] * 256 + buf[18]).ToString();
                        tbxSJJGcx.Text = sjjg;
                        tbxSJJGsz.Text = sjjg;

                        string yjbb = buf[6].ToString("X2") + " " + buf[7].ToString("X2") + " " + buf[8].ToString("X2") + " " + buf[9].ToString("X2");
                        tbxYjbbCX.Text = yjbb;
                        tbxYjbbSZ.Text = buf[6].ToString("X2") + buf[7].ToString("X2") + buf[8].ToString("X2") + buf[9].ToString("X2"); ;

                        string rjbb = buf[10].ToString("X2") + " " + buf[11].ToString("X2");
                        tbxRjbbCX.Text = rjbb;

                        string port = (buf[23] * 256 + buf[24]).ToString();
                        tbxUpPortCX.Text = port;
                        tbxUpPortSZ.Text = port;

                        string gzms = buf[25].ToString("X2");
                        tbxGZMScx.Text = gzms;
                        tbxGZMSsz.Text = gzms;

                        //增加了速率的控制
                        string speed;
                        int selc = -1;
                        if (buf[40] == 0x00)
                        {
                            speed = "4.8Kbps";
                            selc = 0;
                        }
                        else if (buf[40] == 0x02)
                        {
                            speed = "100Kbps";
                            selc = 2;

                        }
                        else if (buf[40] == 0x01)
                        {
                            speed = "38.4Kbps";
                            selc = 1;

                        }
                        else
                        {
                            speed = "未定义";
                            selc = 1;
                        }
                        cbSpeed.SelectedIndex = selc;
                        txbSpeed.Text = speed;

                        string bjjg = (buf[43] * 256 + buf[44]).ToString();
                        tbxBJJGcx.Text = bjjg;
                        tbxBJJGsz.Text = bjjg;

                        string lbjg = (buf[41] * 256 + buf[42]).ToString();
                        tbxLBJGcx.Text = lbjg;
                        tbxLBJGsz.Text = lbjg;

                        string ycbjsl = (buf[36] * 16777216 + buf[37] * 65536 + buf[38] * 256 + buf[39]).ToString();
                        tbxYCBJSJ.Text = ycbjsl;

                        string SJTime;
                        int selce = -1;
                        if (buf[45] == 0x00)
                        {
                            SJTime = "系统时间";
                            selce = 0;
                        }
                        else if (buf[45] == 0x01)
                        {
                            SJTime = "Sensor采集时间";
                            selce = 1;

                        }
                        else
                        {
                            SJTime = "未定义";
                            selce = 1;
                        }
                        cbxSJTime.SelectedIndex = selce;
                        tbxSJTimeCX.Text = SJTime;

                        string NTPIP = buf[46].ToString() + "." + buf[47].ToString() + "." + buf[48].ToString() + "." + buf[49].ToString();
                        tbxNTPipCX.Text = NTPIP;
                        tbxNTPipSZ1.Text = buf[46].ToString();
                        tbxNTPipSZ2.Text = buf[47].ToString();
                        tbxNTPipSZ3.Text = buf[48].ToString();
                        tbxNTPipSZ4.Text = buf[49].ToString();

                        string NTPport = (buf[50] * 256 + buf[51]).ToString();
                        tbxNTPportCX.Text = NTPport;
                        tbxNTPportSZ.Text = NTPport;

                        string Downip = buf[26].ToString() + "." + buf[27].ToString() + "." + buf[28].ToString() + "." + buf[29].ToString();
                        tbxDownIPcx.Text = Downip;
                        tbxBBIPsz1.Text = buf[26].ToString();
                        tbxBBIPsz2.Text = buf[27].ToString();
                        tbxBBIPsz3.Text = buf[28].ToString();
                        tbxBBIPsz4.Text = buf[29].ToString();

                        string Downport = (buf[30] * 256 + buf[31]).ToString();
                        tbxDownPortCX.Text = Downport;
                        tbxDownPortSZ.Text = Downport;

                        string other = (buf[32] * 16777216 + buf[33] * 65536 + buf[34] * 256 + buf[35]).ToString();
                        tbxOtherCX.Text = other;
                        tbxOtherSZ.Text = other;

                        button1.Enabled = true;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;
                        button5.Enabled = true;
                        button6.Enabled = true;
                        button7.Enabled = true;


                    }
                    else if (buf.Length == 60 && buf[0] == 0xCB && buf[7] == 0xBC)
                    {
                        String BB_ID = buf[9].ToString("X2") + " " + buf[10].ToString("X2") + " " + buf[11].ToString("X2") + " " + buf[12].ToString("X2");
                        tbxIDcx.Text = BB_ID;
                        tbxIDsz.Text = buf[9].ToString("X2") + buf[10].ToString("X2") + buf[11].ToString("X2") + buf[12].ToString("X2");

                        string khID = buf[19].ToString("X2") + " " + buf[20].ToString("X2");
                        tbxKHIDcx.Text = khID;
                        tbxKHIDsz.Text = khID;

                        string debug = buf[21].ToString("X2") + " " + buf[22].ToString("X2");
                        tbxDebugcx.Text = debug;
                        tbxDebugSZ.Text = debug;

                        string category = buf[23].ToString("X2");
                        tbxCategoryCx.Text = category;
                        tbxCategorySZ.Text = category;

                        string UpIP = buf[26].ToString() + "." + buf[27].ToString() + "." + buf[28].ToString() + "." + buf[29].ToString();
                        tbxUpIPcx.Text = UpIP;
                        tbxUpIP1.Text = buf[26].ToString();
                        tbxUpIP2.Text = buf[27].ToString();
                        tbxUpIP3.Text = buf[28].ToString();
                        tbxUpIP4.Text = buf[29].ToString();

                        string sjjg = (buf[24] * 256 + buf[25]).ToString();
                        tbxSJJGcx.Text = sjjg;
                        tbxSJJGsz.Text = sjjg;

                        string yjbb = buf[13].ToString("X2") + " " + buf[14].ToString("X2") + " " + buf[15].ToString("X2") + " " + buf[16].ToString("X2");
                        tbxYjbbCX.Text = yjbb;
                        tbxYjbbSZ.Text = buf[13].ToString("X2") + buf[14].ToString("X2") + buf[15].ToString("X2") + buf[16].ToString("X2"); ;

                        string rjbb = buf[17].ToString("X2") + " " + buf[18].ToString("X2");
                        tbxRjbbCX.Text = rjbb;

                        string port = (buf[30] * 256 + buf[31]).ToString();
                        tbxUpPortCX.Text = port;
                        tbxUpPortSZ.Text = port;

                        string gzms = buf[32].ToString("X2");
                        tbxGZMScx.Text = gzms;
                        tbxGZMSsz.Text = gzms;

                        //增加了速率的控制
                        string speed;
                        int selc = -1;
                        if (buf[47] == 0x00)
                        {
                            speed = "4.8Kbps";
                            selc = 0;
                        }
                        else if (buf[47] == 0x02)
                        {
                            speed = "100Kbps";
                            selc = 2;

                        }
                        else if (buf[47] == 0x01)
                        {
                            speed = "38.4Kbps";
                            selc = 1;

                        }
                        else
                        {
                            speed = "未定义";
                            selc = 1;
                        }
                        cbSpeed.SelectedIndex = selc;
                        txbSpeed.Text = speed;

                        string bjjg = (buf[50] * 256 + buf[51]).ToString();
                        tbxBJJGcx.Text = bjjg;
                        tbxBJJGsz.Text = bjjg;

                        string lbjg = (buf[48] * 256 + buf[49]).ToString();
                        tbxLBJGcx.Text = lbjg;
                        tbxLBJGsz.Text = lbjg;

                        string ycbjsl = (buf[43] * 16777216 + buf[44] * 65536 + buf[45] * 256 + buf[46]).ToString();
                        tbxYCBJSJ.Text = ycbjsl;

                        string SJTime;
                        int selce = -1;
                        if (buf[52] == 0x00)
                        {
                            SJTime = "系统时间";
                            selce = 0;
                        }
                        else if (buf[52] == 0x01)
                        {
                            SJTime = "Sensor采集时间";
                            selce = 1;

                        }
                        else
                        {
                            SJTime = "未定义";
                            selce = 1;
                        }
                        cbxSJTime.SelectedIndex = selce;
                        tbxSJTimeCX.Text = SJTime;

                        string NTPIP = buf[53].ToString() + "." + buf[54].ToString() + "." + buf[55].ToString() + "." + buf[56].ToString();
                        tbxNTPipCX.Text = NTPIP;
                        tbxNTPipSZ1.Text = buf[53].ToString();
                        tbxNTPipSZ2.Text = buf[54].ToString();
                        tbxNTPipSZ3.Text = buf[55].ToString();
                        tbxNTPipSZ4.Text = buf[56].ToString();

                        string NTPport = (buf[57] * 256 + buf[58]).ToString();
                        tbxNTPportCX.Text = NTPport;
                        tbxNTPportSZ.Text = NTPport;

                        string Downip = buf[33].ToString() + "." + buf[34].ToString() + "." + buf[35].ToString() + "." + buf[36].ToString();
                        tbxDownIPcx.Text = Downip;
                        tbxBBIPsz1.Text = buf[33].ToString();
                        tbxBBIPsz2.Text = buf[34].ToString();
                        tbxBBIPsz3.Text = buf[35].ToString();
                        tbxBBIPsz4.Text = buf[36].ToString();

                        string Downport = (buf[37] * 256 + buf[38]).ToString();
                        tbxDownPortCX.Text = Downport;
                        tbxDownPortSZ.Text = Downport;

                        string other = (buf[39] * 16777216 + buf[40] * 65536 + buf[41] * 256 + buf[42]).ToString();
                        tbxOtherCX.Text = other;
                        tbxOtherSZ.Text = other;

                        button1.Enabled = true;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;
                        button5.Enabled = true;
                        button6.Enabled = true;
                        button7.Enabled = true;
                    }
                    else if (buf.Length == 47 && buf[0] == 0xBC)
                    {
                        MessageBox.Show("修改成功");

                        String BB_ID = buf[2].ToString("X2") + " " + buf[3].ToString("X2") + " " + buf[4].ToString("X2") + " " + buf[5].ToString("X2");
                        tbxIDcx.Text = BB_ID;

                        string khID = buf[10].ToString("X2") + " " + buf[11].ToString("X2");
                        tbxKHIDcx.Text = khID;
                        //tbxKHIDsz.Text = khID;

                        string debug = buf[12].ToString("X2") + " " + buf[13].ToString("X2");
                        tbxDebugcx.Text = debug;
                        //tbxDebugSZ.Text = debug;

                        string category = buf[14].ToString("X2");
                        tbxCategoryCx.Text = category;
                        //tbxCategorySZ.Text = category;

                        string upIP = buf[17].ToString() + "." + buf[18].ToString() + "." + buf[19].ToString() + "." + buf[20].ToString();
                        tbxUpIPcx.Text = upIP;
                        //tbxBBIPsz1.Text = buf[19].ToString();
                        //tbxBBIPsz2.Text = buf[20].ToString();
                        //tbxBBIPsz3.Text = buf[21].ToString();
                        //tbxBBIPsz4.Text = buf[22].ToString();

                        string sjjg = (buf[15] * 256 + buf[16]).ToString();
                        tbxSJJGcx.Text = sjjg;
                        //tbxSJJGsz.Text = sjjg;

                        string yjbb = buf[6].ToString("X2") + " " + buf[7].ToString("X2") + " " + buf[8].ToString("X2") + " " + buf[9].ToString("X2");
                        tbxYjbbCX.Text = yjbb;
                        //tbxYjbbSZ.Text = yjbb;

                        string port = (buf[21] * 256 + buf[22]).ToString();
                        tbxUpPortCX.Text = port;
                        //tbxPortSZ.Text = port;

                        string gzms = buf[23].ToString("X2");
                        tbxGZMScx.Text = gzms;

                        string speed;
                        if (buf[24] == 0x00)
                        {
                            speed = "4.8Kbps";
                        }
                        else if (buf[24] == 0x02)
                        {
                            speed = "100Kbps";

                        }
                        else if (buf[24] == 0x01)
                        {
                            speed = "38.4Kbps";

                        }
                        else
                        {
                            speed = "未定义";
                        }
                        txbSpeed.Text = speed;

                        string bjjg = (buf[27] * 256 + buf[28]).ToString();
                        tbxBJJGcx.Text = bjjg;

                        string lbjg = (buf[25] * 256 + buf[26]).ToString();
                        tbxLBJGcx.Text = lbjg;

                        string SJTime;
                        int selce = -1;
                        if (buf[29] == 0x00)
                        {
                            SJTime = "系统时间";
                            selce = 0;
                        }
                        else if (buf[29] == 0x01)
                        {
                            SJTime = "Sensor采集时间";
                            selce = 1;

                        }
                        else
                        {
                            SJTime = "未定义";
                            selce = 1;
                        }
                        tbxSJTimeCX.Text = SJTime;

                        string NTPIP = buf[30].ToString() + "." + buf[31].ToString() + "." + buf[32].ToString() + "." + buf[33].ToString();
                        tbxNTPipCX.Text = NTPIP;

                        string NTPport = (buf[34] * 256 + buf[35]).ToString();
                        tbxNTPportCX.Text = NTPport;

                        string downip = buf[36].ToString() + "." + buf[37].ToString() + "." + buf[38].ToString() + "." + buf[39].ToString();
                        tbxDownIPcx.Text = downip;

                        string downport = (buf[40] * 256 + buf[41]).ToString();
                        tbxDownPortCX.Text = downport;

                        string other = (buf[42] * 16777216 + buf[43] * 65536 + buf[44] * 256 + buf[45]).ToString();
                        tbxOtherCX.Text = other;

                        button1.Enabled = true;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;
                        button5.Enabled = true;
                        button6.Enabled = true;
                        button7.Enabled = true;
                    }

                    else if (buf.Length == 15 && buf[0] == 0xBC && buf[1] == 4F)
                    {
                        tbxYCBJSJ.Text = "0";
                        MessageBox.Show("删除成功");
                    }

                    else if (zl == "查询APN" && buf.Length != 0)//读取APN
                    {
                        if (buf[0] == 0xCB && buf.Length > 7 && buf[7] == 0xBC)
                        {


                            for (int length = 0; length < buf.Length - 7; length += cd + 5)
                            {
                                if (buf[7 + length] == 0xBC)
                                {
                                    if (buf[9 + length] == 0x01)
                                    {
                                        cd = Convert.ToInt32(buf[10]);
                                        for (int l = 11; l < 11 + cd; l++)
                                        {
                                            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                                            byte[] byteArray = new byte[] { (byte)buf[l] };
                                            string strCharacter = asciiEncoding.GetString(byteArray);

                                            tbxAPN.Text += strCharacter;

                                        }
                                        //tbxAPNsz.Text = tbxAPN.Text;
                                    }
                                    else if (buf[9 + length] == 0x02)
                                    {
                                        cd = Convert.ToInt32(buf[10 + length]);
                                        for (int l = 11 + length; l < 11 + length + cd; l++)
                                        {
                                            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                                            byte[] byteArray = new byte[] { (byte)buf[l] };
                                            string strCharacter = asciiEncoding.GetString(byteArray);

                                            tbxUsername.Text += strCharacter;
                                        }
                                        //tbxUSEsz.Text = tbxUsername.Text;
                                    }
                                    else if (buf[9 + length] == 0x03)
                                    {
                                        cd = Convert.ToInt32(buf[10 + length]);
                                        for (int l = 11 + length; l < 11 + length + cd; l++)
                                        {
                                            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                                            byte[] byteArray = new byte[] { (byte)buf[l] };
                                            string strCharacter = asciiEncoding.GetString(byteArray);
                                            tbxPassword.Text += strCharacter;
                                        }
                                        //tbxPWsz.Text = tbxPassword.Text;
                                    }
                                }
                            }
                        }
                        else if (buf[0] == 0xBC)
                        {
                            for (int length = 0; length < buf.Length; length += cd + 5)
                            {
                                if (buf[length] == 0xBC)
                                {
                                    if (buf[2 + length] == 0x01)
                                    {
                                        cd = Convert.ToInt32(buf[3]);
                                        for (int l = 4; l < 4 + cd; l++)
                                        {
                                            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                                            byte[] byteArray = new byte[] { (byte)buf[l] };
                                            string strCharacter = asciiEncoding.GetString(byteArray);

                                            tbxAPN.Text += strCharacter;
                                        }
                                        //tbxAPNsz.Text = tbxAPN.Text;
                                    }
                                    else if (buf[2 + length] == 0x02)
                                    {
                                        cd = Convert.ToInt32(buf[3 + length]);
                                        for (int l = 4 + length; l < 4 + length + cd; l++)
                                        {
                                            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                                            byte[] byteArray = new byte[] { (byte)buf[l] };
                                            string strCharacter = asciiEncoding.GetString(byteArray);

                                            tbxUsername.Text += strCharacter;
                                        }
                                        //tbxUSEsz.Text = tbxUsername.Text;
                                    }
                                    else if (buf[2 + length] == 0x03)
                                    {
                                        cd = Convert.ToInt32(buf[3 + length]);
                                        for (int l = 4 + length; l < 4 + length + cd; l++)
                                        {
                                            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                                            byte[] byteArray = new byte[] { (byte)buf[l] };
                                            string strCharacter = asciiEncoding.GetString(byteArray);

                                            tbxPassword.Text += strCharacter;
                                        }
                                        //tbxPWsz.Text = tbxPassword.Text;
                                    }
                                }
                            }
                        }


                        button1.Enabled = true;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;
                        button5.Enabled = true;
                        button6.Enabled = true;
                        button7.Enabled = true;
                    }
                    else if (buf.Length == 5 && buf[0] == 0xBC)
                    {
                        MessageBox.Show("修改成功");

                        button1.Enabled = true;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;
                        button5.Enabled = true;
                        button6.Enabled = true;
                        button7.Enabled = true;
                    }

                }));

                //追加的形式添加到文本框末端，并滚动到最后。   
                //this.txGet.AppendText(builder.ToString());
                //修改接收计数   
                //labelGetCount.Text = "Get:" + received_count.ToString();
                //int inde = dataGridView1.Rows.Add();
                //DataGridViewRow row = dataGridView1.Rows[inde];
                // row.Cells[0].Value = 1;


            }
            finally
            {
                // Listening = false;//我用完了，ui可以关闭串口了。

            }



        }


        public void Send(string Message)
        {

            if (!comm.IsOpen)
            {
                return;
            }

            //我们不管规则了。如果写错了一些，我们允许的，只用正则得到有效的十六进制数   
            MatchCollection mc = Regex.Matches(Message, @"(?i)[\da-f]{2}");
            List<byte> buf = new List<byte>();//填充到这个临时列表中   

            //依次添加到列表中   
            foreach (Match m in mc)
            {
                buf.Add(byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber));
            }
            //转换列表为数组后发送   
            comm.Write(buf.ToArray(), 0, buf.Count);


            //记录发送的字节数   
            //n = buf.Count;

            //send_count += n;//累加发送字节数   
            //labelSendCount.Text = "Send:" + send_count.ToString();//更新界面 





        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            initDevices();
        }

        private void initDevices()
        {
            //自动读取设备名称  , edit by lkj 2015 07 17
            comboBox1.Items.Clear();


            devices = ZKSmartGatewayHelper.FindDevice();
            foreach (ZKSmartGateway device in devices)
            {
                comboBox1.Items.Add(device.DeviceName);
            }

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;

            }



        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openStatus)
            {
                try//异常处理:串口打开时非正常关闭
                {
                    CloseComport();
                }
                catch
                {
                    openStatus = false;
                    btnOpen.Text = "打开";
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = false;
                    button6.Enabled = false;
                    button7.Enabled = false;
                    btnRefresh.Enabled = true;
                    return;
                }
                openStatus = false;
                btnOpen.Text = "打开";
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                btnRefresh.Enabled = true;
                return;


            }


            try
            {
                if (devices == null || devices.Count == 0)
                {
                    return;
                }
                device = devices[comboBox1.SelectedIndex];
                OpenComport(device.DeviceID, 115200);

                Thread.Sleep(50);

                count1 = 0;
                openStatus = true;
                btnOpen.Text = "关闭";
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;

            }
            catch (Exception ex)
            {
                openStatus = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            timer1.Enabled = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            initDevices();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            zl = "上电自检";

            try
            {
                Send("CB 4B 00 00 00 00 BC");
            }
            catch
            {
                MessageBox.Show("串口已关闭！\r\n请刷新串口后再试");
                return;
            }
            timer1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;

            tbxIDcx.Clear();
            tbxKHIDcx.Clear();
            tbxDebugcx.Clear();
            tbxCategoryCx.Clear();
            tbxDownIPcx.Clear();
            tbxSJJGcx.Clear();
            tbxYjbbCX.Clear();
            tbxRjbbCX.Clear();
            tbxDownPortCX.Clear();
            tbxLBJGcx.Clear();
            tbxBJJGcx.Clear();
            tbxYCBJSJ.Clear();
            tbxGZMScx.Clear();
            txbSpeed.Clear();
            tbxSJTimeCX.Clear();
            tbxNTPipCX.Clear();
            tbxNTPportCX.Clear();
            tbxUpIPcx.Clear();
            tbxUpPortCX.Clear();
            tbxOtherCX.Clear();


            try
            {
                //设置时间间隔
                string sjjg = "00 00";
                if (tbxSJJGsz.Text != "")
                {
                    int sj = Convert.ToInt32(tbxSJJGsz.Text);
                    string sj16 = sj.ToString("X2");

                    if (sj > 4095 && sj < 65536)//MAX:FF FF
                    {
                        string g, f;
                        f = sj16.Substring(0, 2);
                        g = sj16.Substring(2, 2);
                        sjjg = f + " " + g;

                    }
                    else if (sj > 255 && sj < 4096)//MAX:F FF
                    {
                        string g, f;
                        f = sj16.Substring(0, 1);
                        g = sj16.Substring(1, 2);
                        sjjg = "0" + f + " " + g;

                    }
                    else if (sj > 0 && sj < 256)//MAX:FF
                    {
                        sjjg = "00 " + sj16;
                    }
                    else
                    {
                        MessageBox.Show("工作时间超出范围！");
                        return;
                    }

                }
                //设置MAC
                string newmac = "00 00 00 00";
                if (tbxIDsz.Text != "")
                {
                    string str = tbxIDsz.Text;
                    string a, b, c, d;
                    if (str.Length < 8)
                    {
                        MessageBox.Show("MAC输入错误");
                    }
                    else
                    {
                        a = str.Substring(0, 2);
                        b = str.Substring(2, 2);
                        c = str.Substring(4, 2);
                        d = str.Substring(6, 2);
                        newmac = a + " " + b + " " + c + " " + d;
                    }
                }
                else
                {
                    MessageBox.Show("请输入TAG MAC!");
                }
                //设置客户码
                string khid = "00 00";
                if (tbxKHIDsz.Text != "" || tbxKHIDsz.Text.Length != 5)
                {
                    khid = tbxKHIDsz.Text;
                }
                else
                {
                    MessageBox.Show("请输入客户码");
                }
                //设置下载IP
                string Downip = "00 00 00 00";
                if (tbxBBIPsz1.Text != "" && tbxBBIPsz2.Text != "" && tbxBBIPsz3.Text != "" && tbxBBIPsz4.Text != "")
                {
                    int ip1 = Convert.ToInt32(tbxBBIPsz1.Text);
                    int ip2 = Convert.ToInt32(tbxBBIPsz2.Text);
                    int ip3 = Convert.ToInt32(tbxBBIPsz3.Text);
                    int ip4 = Convert.ToInt32(tbxBBIPsz4.Text);
                    if (ip1 <= 255 && ip2 <= 255 && ip3 <= 255 && ip3 <= 255)
                    {
                        Downip = ip1.ToString("X2") + " " + ip2.ToString("X2") + " " + ip3.ToString("X2") + " " + ip4.ToString("X2");
                    }

                }
                else
                {
                    MessageBox.Show("请输入下载IP!");
                }
                //设置上传IP
                string Upip = "00 00 00 00";
                if (tbxUpIP1.Text != "" && tbxUpIP2.Text != "" && tbxUpIP3.Text != "" && tbxUpIP4.Text != "")
                {
                    int upip1 = Convert.ToInt32(tbxUpIP1.Text);
                    int upip2 = Convert.ToInt32(tbxUpIP2.Text);
                    int upip3 = Convert.ToInt32(tbxUpIP3.Text);
                    int upip4 = Convert.ToInt32(tbxUpIP4.Text);
                    if (upip1 <= 255 && upip2 <= 255 && upip3 <= 255 && upip3 <= 255)
                    {
                        Upip = upip1.ToString("X2") + " " + upip2.ToString("X2") + " " + upip3.ToString("X2") + " " + upip4.ToString("X2");
                    }

                }
                else
                {
                    MessageBox.Show("请输入上传IP!");
                }
                //设置debug
                string debug = "00 00";
                if (tbxDebugSZ.Text != "" || tbxDebugSZ.Text.Length != 5)
                {
                    debug = tbxDebugSZ.Text;
                }
                else
                {
                    MessageBox.Show("请输入Debug!");
                }
                //设置Category
                string category = "00";
                if (tbxCategorySZ.Text != "" || tbxCategorySZ.Text.Length != 2)
                {
                    category = tbxCategorySZ.Text;
                }
                else
                {
                    MessageBox.Show("请输入Category!");
                }
                //设置下载端口
                string downport = "00 00";
                if (tbxDownPortSZ.Text != "")
                {
                    int pt = Convert.ToInt32(tbxDownPortSZ.Text);
                    string pt16 = pt.ToString("X2");

                    if (pt > 4095 && pt < 65536)//MAX:FF FF
                    {
                        string p, t;
                        p = pt16.Substring(0, 2);
                        t = pt16.Substring(2, 2);
                        downport = p + " " + t;

                    }
                    else if (pt > 255 && pt < 4096)//MAX:F FF
                    {
                        string p, t;
                        p = pt16.Substring(0, 1);
                        t = pt16.Substring(1, 2);
                        downport = "0" + p + " " + t;

                    }
                    else if (pt > 0 && pt < 256)//MAX:FF
                    {
                        downport = "00 " + pt16;
                    }
                    else
                    {
                        MessageBox.Show("下载端口超出范围！");
                        return;
                    }

                }
                //设置上传端口
                string upport = "00 00";
                if (tbxUpPortSZ.Text != "")
                {
                    int uo = Convert.ToInt32(tbxUpPortSZ.Text);
                    string uo16 = uo.ToString("X2");

                    if (uo > 4095 && uo < 65536)//MAX:FF FF
                    {
                        string u,o;
                        u = uo16.Substring(0, 2);
                        o = uo16.Substring(2, 2);
                        upport = u + " " + o;

                    }
                    else if (uo > 255 && uo < 4096)//MAX:F FF
                    {
                        string u,o;
                        u = uo16.Substring(0, 1);
                        o = uo16.Substring(1, 2);
                        upport = "0" + u + " " + o;

                    }
                    else if (uo > 0 && uo < 256)//MAX:FF
                    {
                        upport = "00 " + uo16;
                    }
                    else
                    {
                        MessageBox.Show("上传端口超出范围！");
                        return;
                    }

                }

                
                //设置硬件版本
                string yjbb = "00 00 00 00";
                if (tbxYjbbSZ.Text != "")
                {
                    string str = tbxYjbbSZ.Text;
                    string a, b, c, d;
                    if (str.Length < 8)
                    {
                        MessageBox.Show("MAC输入错误");
                    }
                    else
                    {
                        a = str.Substring(0, 2);
                        b = str.Substring(2, 2);
                        c = str.Substring(4, 2);
                        d = str.Substring(6, 2);
                        yjbb = a + " " + b + " " + c + " " + d;
                    }
                }
                else
                {
                    MessageBox.Show("请输入硬件版本!");
                }

                //设置工作模式
                string gzms = "00";
                if (tbxGZMSsz.Text != "" && tbxGZMSsz.Text.Length != 1)
                {
                    gzms = tbxGZMSsz.Text;
                }
                else
                {
                    MessageBox.Show("请输入正确的工作模式!");
                }

                //林克坚 增加，增加了添加传输速率

                string speed = cbSpeed.SelectedIndex.ToString("X2");

                //设置轮播间隔
                string lbjg = "00 00";
                if (tbxLBJGsz.Text != "")
                {
                    int sj = Convert.ToInt32(tbxLBJGsz.Text);
                    string sj16 = sj.ToString("X2");

                    if (sj > 4095 && sj < 65536)//MAX:FF FF
                    {
                        string g, f;
                        f = sj16.Substring(0, 2);
                        g = sj16.Substring(2, 2);
                        lbjg = f + " " + g;

                    }
                    else if (sj > 255 && sj < 4096)//MAX:F FF
                    {
                        string g, f;
                        f = sj16.Substring(0, 1);
                        g = sj16.Substring(1, 2);
                        lbjg = "0" + f + " " + g;

                    }
                    else if (sj > 0 && sj < 256)//MAX:FF
                    {
                        lbjg = "00 " + sj16;
                    }
                    else
                    {
                        MessageBox.Show("轮播间隔超出范围！");
                        return;
                    }

                }

                //设置报警间隔
                string bjjg = "00 00";
                if (tbxBJJGsz.Text != "")
                {
                    int sj = Convert.ToInt32(tbxBJJGsz.Text);
                    string sj16 = sj.ToString("X2");

                    if (sj > 4095 && sj < 65536)//MAX:FF FF
                    {
                        string g, f;
                        f = sj16.Substring(0, 2);
                        g = sj16.Substring(2, 2);
                        bjjg = f + " " + g;

                    }
                    else if (sj > 255 && sj < 4096)//MAX:F FF
                    {
                        string g, f;
                        f = sj16.Substring(0, 1);
                        g = sj16.Substring(1, 2);
                        bjjg = "0" + f + " " + g;

                    }
                    else if (sj > 0 && sj < 256)//MAX:FF
                    {
                        bjjg = "00 " + sj16;
                    }
                    else
                    {
                        MessageBox.Show("报警间隔超出范围！");
                        return;
                    }

                }

                //设置数据时间
                string sjtime = cbxSJTime.SelectedIndex.ToString("X2");

                //设置NTPIP
                string ntpip = "00 00 00 00";
                if (tbxNTPipSZ1.Text != "" && tbxNTPipSZ2.Text != "" && tbxNTPipSZ3.Text != "" && tbxNTPipSZ4.Text != "")
                {
                    int ntpip1 = Convert.ToInt32(tbxNTPipSZ1.Text);
                    int ntpip2 = Convert.ToInt32(tbxNTPipSZ2.Text);
                    int ntpip3 = Convert.ToInt32(tbxNTPipSZ3.Text);
                    int ntpip4 = Convert.ToInt32(tbxNTPipSZ4.Text);
                    if (ntpip1 <= 255 && ntpip2 <= 255 && ntpip3 <= 255 && ntpip3 <= 255)
                    {
                        ntpip = ntpip1.ToString("X2") + " " + ntpip2.ToString("X2") + " " + ntpip3.ToString("X2") + " " + ntpip4.ToString("X2");
                    }

                }
                else
                {
                    MessageBox.Show("请输入NTP IP!");
                }

                //设置NTP端口
                string ntpport = "00 00";
                if (tbxNTPportSZ.Text != "")
                {
                    int ntppt = Convert.ToInt32(tbxNTPportSZ.Text);
                    string ntppt16 = ntppt.ToString("X2");

                    if (ntppt > 4095 && ntppt < 65536)//MAX:FF FF
                    {
                        string p, t;
                        p = ntppt16.Substring(0, 2);
                        t = ntppt16.Substring(2, 2);
                        ntpport = p + " " + t;

                    }
                    else if (ntppt > 255 && ntppt < 4096)//MAX:F FF
                    {
                        string p, t;
                        p = ntppt16.Substring(0, 1);
                        t = ntppt16.Substring(1, 2);
                        ntpport = "0" + p + " " + t;

                    }
                    else if (ntppt > 0 && ntppt < 256)//MAX:FF
                    {
                        ntpport = "00 " + ntppt16;
                    }
                    else
                    {
                        MessageBox.Show("NTP端口超出范围！");
                        return;
                    }

                }
                //设置其他
                string other = "00 00 00 00";
                //if (tbxOtherSZ.Text != "")
                //{
                //    int oz = Convert.ToInt32(tbxOtherSZ.Text);
                //    string oz16 = oz.ToString("X2");

                //    if (oz > 4095 && oz < 65536)//MAX:FF FF
                //    {
                //        string oz1,oz2;
                //        oz1 = oz16.Substring(0, 2);
                //        oz2 = oz16.Substring(2, 2);
                //        other = oz1 + " " + oz2;

                //    }
                //    else if (oz > 255 && oz < 4096)//MAX:F FF
                //    {
                //        string oz1, oz2;
                //        oz1 = oz16.Substring(0, 1);
                //        oz2 = oz16.Substring(1, 2);
                //        other = "0" + oz1 + " " + oz2;

                //    }
                //    else if (oz >= 0 && oz < 256)//MAX:FF
                //    {
                //        other = "00 " + oz16;
                //    }
                //    else
                //    {
                //        MessageBox.Show("其他超出范围！");
                //        return;
                //    }

                //}

                //发送指令
                string sendStr = "CB 4C " + newmac + " " + yjbb + " " + khid + " " + debug + " " + category + " " + sjjg + " " + Upip + " " + upport + " " + gzms + " " + speed + " " + lbjg + " " + bjjg + " " + sjtime + " " + ntpip + " " + ntpport + " " + Downip + " " + downport + " " + other + " BC";
                try
                {
                    Send(sendStr);
                }
                catch
                {
                    MessageBox.Show("串口已关闭！\r\n请刷新串口后再试");
                    return;
                }

            }
            catch
            {
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Send("CB 4F 00 00 00 00 00 00 00 00 BC");
            }
            catch
            {
                MessageBox.Show("串口已关闭！\r\n请刷新串口后再试");
                return;
            }
            timer1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tbxAPNsz.Clear();
            tbxAPN.Clear();
            tbxUsername.Clear();
            tbxUSEsz.Clear();
            tbxPassword.Clear();
            tbxPWsz.Clear();

            try
            {
                Send("CB 53 00 00 00 00 BC");
            }
            catch
            {
                MessageBox.Show("串口已关闭！\r\n请刷新串口后再试");
                return;
            }
            zl = "查询APN";
            timer1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            zl = "修改APN";
            timer1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;

            if (tbxAPNsz.Text.Length <= 50)
            {
                string apn = null;

                for (int i = 0; i < tbxAPNsz.Text.Length; i++)
                {
                    string b = tbxAPNsz.Text.Substring(i, 1);

                    System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                    int intAsciiCode = (int)asciiEncoding.GetBytes(b)[0];

                    apn += intAsciiCode.ToString("X2") + " ";
                }

                string length = "00";
                if (tbxAPNsz.Text.Length == 0)
                {
                    length = "00";
                }
                else
                {
                    length = (apn.Length / 3).ToString("X2") + " ";
                }

                string sendStr = "CB 54 01 " + length + apn + "BC";
                try
                {
                    Send(sendStr);
                }
                catch
                {
                    MessageBox.Show("串口已关闭！\r\n请刷新串口后再试");
                    return;
                }
            }
            else
            {
                MessageBox.Show("请输入正确的APN！");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            zl = "修改Username";
            timer1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;

            if (tbxUSEsz.Text.Length <= 50)
            {
                string username = null;

                for (int i = 0; i < tbxUSEsz.Text.Length; i++)
                {
                    string b = tbxUSEsz.Text.Substring(i, 1);

                    System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                    int intAsciiCode = (int)asciiEncoding.GetBytes(b)[0];

                    username += intAsciiCode.ToString("X2") + " ";
                }

                string length = "00";
                if (tbxUSEsz.Text.Length == 0)
                {
                    length = "00";
                }
                else
                {
                    length = (username.Length / 3).ToString("X2") + " ";
                }

                string sendStr = "CB 54 02 " + length + username + "BC";
                try
                {
                    Send(sendStr);
                }
                catch
                {
                    MessageBox.Show("串口已关闭！\r\n请刷新串口后再试");
                    return;
                }
            }
            else
            {
                MessageBox.Show("请输入正确的Username！");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            zl = "修改Password";
            timer1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;

            if (tbxPWsz.Text.Length <= 50)
            {
                string password = null;

                for (int i = 0; i < tbxPWsz.Text.Length; i++)
                {
                    string b = tbxPWsz.Text.Substring(i, 1);

                    System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                    int intAsciiCode = (int)asciiEncoding.GetBytes(b)[0];

                    password += intAsciiCode.ToString("X2") + " ";
                }

                string length = "00";
                if (tbxPWsz.Text.Length == 0)
                {
                    length = "00";
                }
                else
                {
                    length = (password.Length / 3).ToString("X2") + " ";
                }

                string sendStr = "CB 54 03 " + length + password + "BC";
                try
                {
                    Send(sendStr);
                }
                catch
                {
                    MessageBox.Show("串口已关闭！\r\n请刷新串口后再试");
                    return;
                }
            }
            else
            {
                MessageBox.Show("请输入正确的Password！");
            }
        }
    }
}
