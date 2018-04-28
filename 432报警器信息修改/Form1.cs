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

        List<ZKSmartGateway> devices;   // 所有设备列表
        ZKSmartGateway device;          // 当前选择的设备

        //临时用到的变量
        bool openStatus = false;
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

        /// <summary>
        /// 处理网关的上电自检数据包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        Int16 DataProcess_SelfTest(byte[] rxBuf, UInt16 rxLen)
        {
            if (rxLen < 42)
            {
                return -1;
            }

            // id
            tbx_id.Text = rxBuf[2].ToString("X2") + " " + rxBuf[3].ToString("X2") + " " + rxBuf[4].ToString("X2") + " " + rxBuf[5].ToString("X2");
            tbx_new_id.Text = tbx_id.Text;

            // hardware revision
            tbx_hwRevision.Text = rxBuf[6].ToString("X2") + " " + rxBuf[7].ToString("X2") + " " + rxBuf[8].ToString("X2") + " " + rxBuf[9].ToString("X2");
            tbx_new_hwRevision.Text = tbx_hwRevision.Text;

            // MSP432 software revision
            tbx_swRevisionMSP432.Text = rxBuf[10].ToString("X2") + " " + rxBuf[11].ToString("X2");

            // CC1310 software revision
            tbx_swRevisionCC1310.Text = rxBuf[12].ToString("X2") + " " + rxBuf[13].ToString("X2");

            // customer
            tbx_customer.Text = rxBuf[14].ToString("X2") + " " + rxBuf[15].ToString("X2");
            tbx_new_customer.Text = tbx_customer.Text;

            // debug
            tbx_debug.Text = rxBuf[16].ToString("X2") + " " + rxBuf[17].ToString("X2");
            tbx_new_debug.Text = tbx_debug.Text;

            // category
            tbx_category.Text = rxBuf[18].ToString("X2");
            tbx_new_category.Text = tbx_category.Text;

            // interval
            tbx_interval.Text = (rxBuf[19] * 256 + rxBuf[20]).ToString();
            tbx_new_interval.Text = tbx_interval.Text;

            // pattern
            tbx_pattern.Text = rxBuf[21].ToString("X2");
            tbx_new_pattern.Text = tbx_pattern.Text;

            // bps
            byte bps = rxBuf[22];
            if (bps > 2)
            {
                return -2;
            }
            cbx_bps.SelectedIndex = bps;
            cbx_new_bps.SelectedIndex = bps;

            // channel
            byte channel = rxBuf[23];
            if (channel > 7)
            {
                return -3;
            }
            cbx_channel.SelectedIndex = channel;
            cbx_new_channel.SelectedIndex = channel;

            // Carousel
            tbx_carousel.Text = (rxBuf[24] * 256 + rxBuf[25]).ToString();
            tbx_new_carousel.Text = tbx_carousel.Text;

            // Alert
            tbx_alert.Text = (rxBuf[26] * 256 + rxBuf[27]).ToString();
            tbx_new_alert.Text = tbx_alert.Text;

            // transPolicy
            byte transPolicy = rxBuf[28];
            if (transPolicy > 1)
            {
                return -4;
            }
            cbx_transPolicy.SelectedIndex = transPolicy;
            cbx_new_transPolicy.SelectedIndex = transPolicy;

            // timeSrc
            byte timeSrc = rxBuf[29];
            if (timeSrc > 1)
            {
                return -5;
            }
            cbx_timeSrc.SelectedIndex = timeSrc;
            cbx_new_timeSrc.SelectedIndex = timeSrc;

            // RAM Len H
            tbx_ramH.Text = rxBuf[30].ToString();

            // RAM Len L
            tbx_ramL.Text = rxBuf[31].ToString();

            // Flash Len H
            tbx_flashH.Text = (rxBuf[32] * 65536 + rxBuf[33] * 256 + rxBuf[34]).ToString();

            // Flash Len L
            tbx_flashL.Text = (rxBuf[35] * 65536 + rxBuf[36] * 256 + rxBuf[37]).ToString();

            // Server Domain
            byte SDL = rxBuf[38];
            string serverDomain = "";
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();

            for (byte iCount = 0; iCount < SDL; iCount++)
            {
                byte[] sdTmp = new byte[] { (byte)rxBuf[39 + iCount] };
                serverDomain += asciiEncoding.GetString(sdTmp);
            }

            tbx_serverDomain.Text = serverDomain;
            tbx_new_serverDomain.Text = tbx_serverDomain.Text;

            // Server Port 
            tbx_serverPort.Text = (rxBuf[39 + SDL] * 256 + rxBuf[40 + SDL]).ToString();
            tbx_new_serverPort.Text = tbx_serverPort.Text;

            return 0;
        }

        /// <summary>
        /// 处理网关的修改参数数据包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        Int16 DataProcess_ModifyInfo(byte[] rxBuf, UInt16 rxLen)
        {
            if (rxLen < 30)
            {
                return -1;
            }

            // id
            tbx_id.Text = rxBuf[2].ToString("X2") + " " + rxBuf[3].ToString("X2") + " " + rxBuf[4].ToString("X2") + " " + rxBuf[5].ToString("X2");
            tbx_new_id.Text = tbx_id.Text;

            // hardware revision
            tbx_hwRevision.Text = rxBuf[6].ToString("X2") + " " + rxBuf[7].ToString("X2") + " " + rxBuf[8].ToString("X2") + " " + rxBuf[9].ToString("X2");
            tbx_new_hwRevision.Text = tbx_hwRevision.Text;

            // customer
            tbx_customer.Text = rxBuf[10].ToString("X2") + " " + rxBuf[11].ToString("X2");
            tbx_new_customer.Text = tbx_customer.Text;

            // debug
            tbx_debug.Text = rxBuf[12].ToString("X2") + " " + rxBuf[13].ToString("X2");
            tbx_new_debug.Text = tbx_debug.Text;

            // category
            tbx_category.Text = rxBuf[14].ToString("X2");
            tbx_new_category.Text = tbx_category.Text;

            // interval
            tbx_interval.Text = (rxBuf[15] * 256 + rxBuf[16]).ToString();
            tbx_new_interval.Text = tbx_interval.Text;

            // pattern
            tbx_pattern.Text = rxBuf[17].ToString("X2");
            tbx_new_pattern.Text = tbx_pattern.Text;

            // bps
            byte bps = rxBuf[18];
            if (bps > 2)
            {
                return -2;
            }
            cbx_bps.SelectedIndex = bps;
            cbx_new_bps.SelectedIndex = bps;

            // channel
            byte channel = rxBuf[19];
            if (channel > 7)
            {
                return -3;
            }
            cbx_channel.SelectedIndex = channel;
            cbx_new_channel.SelectedIndex = channel;

            // Carousel
            tbx_carousel.Text = (rxBuf[20] * 256 + rxBuf[21]).ToString();
            tbx_new_carousel.Text = tbx_carousel.Text;

            // Alert
            tbx_alert.Text = (rxBuf[22] * 256 + rxBuf[23]).ToString();
            tbx_new_alert.Text = tbx_alert.Text;

            // transPolicy
            byte transPolicy = rxBuf[24];
            if (transPolicy > 1)
            {
                return -4;
            }
            cbx_transPolicy.SelectedIndex = transPolicy;
            cbx_new_transPolicy.SelectedIndex = transPolicy;

            // timeSrc
            byte timeSrc = rxBuf[25];
            if (timeSrc > 1)
            {
                return -5;
            }
            cbx_timeSrc.SelectedIndex = timeSrc;
            cbx_new_timeSrc.SelectedIndex = timeSrc;          

            // Server Domain
            byte SDL = rxBuf[26];
            string serverDomain = "";
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();

            for (byte iCount = 0; iCount < SDL; iCount++)
            {
                byte[] sdTmp = new byte[] { (byte)rxBuf[27 + iCount] };
                serverDomain += asciiEncoding.GetString(sdTmp);
            }

            tbx_serverDomain.Text = serverDomain;
            tbx_new_serverDomain.Text = tbx_serverDomain.Text;

            // Server Port 
            tbx_serverPort.Text = (rxBuf[27 + SDL] * 256 + rxBuf[28 + SDL]).ToString();
            tbx_new_serverPort.Text = tbx_serverPort.Text;

            return 0;
        }

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

                StringBuilder builder = new StringBuilder();

                //因为要访问ui资源，所以需要使用invoke方式同步ui。   

                //判断是否是显示为16禁止   

                //处理温湿度数据
                this.Invoke((EventHandler)(delegate
                {
                    int error = 0;

                    if (zl == "上电自检" && buf.Length >= 40 && buf[0] == 0xBC && buf[11] >= 19)
                    {

                        DataProcess_SelfTest(buf, (UInt16)n);

                        button1.Enabled = true;
                        button2.Enabled = true;
                        button3.Enabled = true;

                    }
                    else if (zl == "上电自检" && buf.Length >= 47 && buf[0] == 0xCB && buf[7] == 0xBC && buf[18] >= 19)
                    {

                        DataProcess_SelfTest(buf, (UInt16)n);

                        button1.Enabled = true;
                        button2.Enabled = true;
                        button3.Enabled = true;

                    }
                    else if (zl == "修改参数" && buf.Length >= 34 && buf[0] == 0xBC)
                    {
                       error = DataProcess_ModifyInfo(buf, (UInt16)n);
                        button1.Enabled = true;
                        button2.Enabled = true;
                        button3.Enabled = true;

                        if(error < 0)
                        {
                            return;
                        }
                        else
                        {
                            MessageBox.Show("修改成功");
                        }                        
                    }
                    else if (buf.Length == 15 && buf[0] == 0xBC && buf[1] == 4F)
                    {
                        tbx_ramH.Text = "0";
                        MessageBox.Show("删除成功");
                    }
                    else if (buf.Length == 5 && buf[0] == 0xBC)
                    {
                        MessageBox.Show("修改成功");

                        button1.Enabled = true;
                        button2.Enabled = true;
                        button3.Enabled = true;
                    }

                }));
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
            initDevices();
        }

        private void initDevices()
        {
            //自动读取设备名称  , edit by lkj 2015 07 17
            comboBox1.Items.Clear();

            devices = ZKSmartGatewayHelper.FindDevice();
            foreach (ZKSmartGateway dev in devices)
            {           
                comboBox1.Items.Add(dev.DeviceName);
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
                    btnRefresh.Enabled = true;
                    comboBox1.Enabled = true;
                    return;
                }
                openStatus = false;
                btnOpen.Text = "打开";
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                btnRefresh.Enabled = true;
                comboBox1.Enabled = true;
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

                openStatus = true;
                btnOpen.Text = "关闭";
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                btnRefresh.Enabled = false;
                comboBox1.Enabled = false;
            }
            catch (Exception)
            {
                openStatus = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            timer1.Enabled = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            initDevices();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            zl = "上电自检";

            Display_clear();

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
        }

        /// <summary>
        /// 清空上电自检的显示
        /// </summary>
        /// <param name=""></param>
        private void Display_clear()
        {
            tbx_id.Clear();
            tbx_hwRevision.Clear();
            tbx_swRevisionMSP432.Clear();
            tbx_swRevisionCC1310.Clear();
            tbx_customer.Clear();
            tbx_debug.Clear();
            tbx_category.Clear();
            tbx_interval.Clear();
            tbx_pattern.Clear();
            cbx_bps.SelectedIndex = -1;
            cbx_channel.SelectedIndex = -1;
            tbx_carousel.Clear();
            tbx_alert.Clear();
            cbx_transPolicy.SelectedIndex = -1;
            cbx_timeSrc.SelectedIndex = -1;
            tbx_ramH.Clear();
            tbx_ramL.Clear();
            tbx_flashH.Clear();
            tbx_flashL.Clear();
            tbx_serverDomain.Clear();
            tbx_serverPort.Clear();
        }

        /// <summary>
        /// 检查输入框的内容是否合法；如果合法，就组包发给嵌入式设备；
        /// </summary>
        /// <returns></returns>
        private int PacketAndSend()
        {
            // id
            string id = "00 00 00 00 ";
            if (tbx_new_id.Text.Length != 11)
            {
                MessageBox.Show("ID错误!");
                return -1;
            }
            id = tbx_new_id.Text + " ";

            // hardware revision
            string hwRevision = "00 00 00 00 ";
            if (tbx_new_hwRevision.Text.Length != 11)
            {
                MessageBox.Show("硬件版本错误");
                return -2;
            }
            hwRevision = tbx_new_hwRevision.Text + " ";

            // customer
            string customer = "00 00 ";
            if (tbx_new_customer.Text.Length != 5)
            {
                MessageBox.Show("客户码错误");
                return -3;
            }
            customer = tbx_new_customer.Text + " ";

            // debug
            string debug = "00 00 ";
            if (tbx_new_debug.Text.Length != 5)
            {
                MessageBox.Show("Debug错误!");
                return -4;
            }
            debug = tbx_new_debug.Text + " ";

            // category
            string category = "00 ";
            if (tbx_new_category.Text.Length != 2)
            {
                MessageBox.Show("Category错误!");
                return -5;
            }
            category = tbx_new_category.Text + " ";

            // interval
            string interval = "00 00 ";
            if (tbx_new_interval.Text == "" || tbx_new_interval.Text.Length > 5)
            {
                MessageBox.Show("时间间隔错误！");
                return -6;
            }
            UInt16 intervalI = Convert.ToUInt16(tbx_new_interval.Text);
            interval = (intervalI / 256).ToString("X2") + " " + (intervalI % 256).ToString("X2") + " ";

            // pattern
            string pattern = "00 ";
            if (tbx_new_pattern.Text.Length != 2)
            {
                MessageBox.Show("工作模式错误!");
                return -7;
            }
            pattern = tbx_new_pattern.Text + " ";

            // bps
            string bps = cbx_new_bps.SelectedIndex.ToString("X2") + " ";

            // channel
            string channel = cbx_new_channel.SelectedIndex.ToString("X2") + " ";

            // carousel
            string carousel = "00 00 ";
            if (tbx_new_carousel.Text == "" || tbx_new_carousel.Text.Length > 5)
            {
                MessageBox.Show("轮播间隔错误！");
                return -8;
            }
            UInt16 carouselI = Convert.ToUInt16(tbx_new_carousel.Text);
            carousel = (carouselI / 256).ToString("X2") + " " + (carouselI % 256).ToString("X2") + " ";

            // alert
            string alert = "00 00 ";
            if (tbx_new_alert.Text == "" || tbx_new_alert.Text.Length > 5)
            {
                MessageBox.Show("轮播间隔错误！");
                return -9;
            }
            UInt16 alertI = Convert.ToUInt16(tbx_new_alert.Text);
            alert = (alertI / 256).ToString("X2") + " " + (alertI % 256).ToString("X2") + " ";

            // transPolicy
            string transPolicy = cbx_new_transPolicy.SelectedIndex.ToString("X2") + " ";

            // timeSrc
            string timeSrc = cbx_new_timeSrc.SelectedIndex.ToString("X2") + " ";

            // Server Domain
            string ServerDomain = "";

            tbx_new_serverDomain.Text = tbx_new_serverDomain.Text.Trim();       // 去除域名中的空格
            if (tbx_new_serverDomain.Text == "")
            {
                MessageBox.Show("服务器域名错误!");
                return -10;
            }

            byte SDL = (byte)tbx_new_serverDomain.Text.Length;
            for (int iCount = 0; iCount < SDL; iCount++)
            {
                string b = tbx_new_serverDomain.Text.Substring(iCount, 1);

                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                int intAsciiCode = (int)asciiEncoding.GetBytes(b)[0];

                ServerDomain += intAsciiCode.ToString("X2") + " ";
            }

            // Server Port
            string ServerPort = "00 00 ";
            if (tbx_new_serverPort.Text == "" || tbx_new_serverPort.Text.Length > 5)
            {
                MessageBox.Show("服务器端口错误！");
                return -11;
            }
            UInt16 ServerPortI = Convert.ToUInt16(tbx_new_serverPort.Text);
            ServerPort = (ServerPortI / 256).ToString("X2") + " " + (ServerPortI % 256).ToString("X2") + " ";

            //发送指令
            string sendStr = "CB 4C " + id + hwRevision + customer + debug + category + interval + pattern + bps + channel + carousel + alert + transPolicy + timeSrc + SDL.ToString("X2") + " " + ServerDomain + ServerPort + "BC";
            try
            {
                Send(sendStr);
            }
            catch
            {
                MessageBox.Show("指令发送失败!");
                return -12;
            }

            return 0;
        }


        /// <summary>
        /// 修改参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            int error = 0;

            zl = "修改参数";
            timer1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

            Display_clear();

            try
            {
               error = PacketAndSend();
                if(error < 0)
                {
                    timer1.Enabled = false;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                }                
            }
            catch
            {
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(cbx_queueNo.SelectedIndex < 0 || cbx_queueNo.SelectedIndex > 4)
            {
                return;
            }

            byte queueNo = (byte)cbx_queueNo.SelectedIndex;

            try
            {
                Send("CB 4F " + queueNo.ToString("X2") + " BC");
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
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            initDevices();
        }
    }
}
