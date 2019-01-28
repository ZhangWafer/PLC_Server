using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLC_POSITION.Class;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace PLC_Server
{
    public partial class ServerFrom : Form
    {
        public ServerFrom()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectPlc();
            GetReferTable();
            TimerInit();
        }

        //启动PLC方法
        private void ConnectPlc()
        {
            //PLC连接标志位
            int iRenturnCode;
            //PLC接口选择PLC号码
            axActEasyIF1.ActLogicalStationNumber = Convert.ToInt16(comboBox1.Text);
            iRenturnCode = axActEasyIF1.Open();
            if (iRenturnCode == 0)
            {
                MessageBox.Show("连接成功");
            }
            else
            {
                MessageBox.Show("连接失败");
            }

        }

        //数据库读出数据
        string[] Register_array = new string[250];
        private void GetReferTable()
        {
            int i = 1;
            SqlDataReader sqlReader = SqlHelper.ExecuteReader("select * from Refer_Table order by Position;");

            while (sqlReader.Read())
            {
                Register_array[i] = sqlReader["Register"].ToString();
                i++;
            }
        }


        //从plc各自的寄存器中读出相应的数据
        int[] RegisterData = new int[250];
        private void GetPlcRegisterData()
        {

            for (int i = 1; i < 213; i++)
            {
                axActEasyIF1.ReadDeviceBlock(Register_array[i], 1, out RegisterData[i]);
            }

        }

        //存入数据后进行计算得到真实数据
        float[] floatRegisterData = new float[250];
        private void CalculateData()
        {
            //转化为float型数据，并且全部除以10
            for (int i = 1; i < 220; i++)
            {
                floatRegisterData[i] = RegisterData[i];
                floatRegisterData[i] = floatRegisterData[i] / 10;
            }
            //还原压力数值，乘以10，原值即可
            for (int i = 31; i < 43; i++)
            {
                floatRegisterData[i] = floatRegisterData[i] * 10;
            }
            //Mpa数值需要除以100，所以在除以10基础上再除10
            for (int i = 43; i < 53; i++)
            {
                floatRegisterData[i] = floatRegisterData[i] / 10;
            }
        }

        //定时器初始化方法
        private void TimerInit()
        {
            timer1.Enabled = true;
            timer1.Interval = 10000;
            timer1.Start();
        }

        //socket初始化
        static Socket socketwatch = null;
        private void SocketInit()
        {
            //定义一个套接字用于监听客户端发来的消息，包含三个参数（IP4寻址协议，流式连接，Tcp协议）  
            socketwatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //服务端发送信息需要一个IP地址和端口号  
            IPAddress address = IPAddress.Parse("172.16.13.59");
            //将IP地址和端口号绑定到网络节点point上  
            IPEndPoint point = new IPEndPoint(address, 60000);
            //监听绑定的网络节点  
            socketwatch.Bind(point);
            //将套接字的监听队列长度限制为20  
            socketwatch.Listen(20);
            //负责监听客户端的线程:创建一个监听线程  
            Thread threadwatch = new Thread(watchConnecting);
            //将窗体线程设置为与后台同步，随着主线程结束而结束  
            threadwatch.IsBackground = true;
            //启动线程
            threadwatch.Start();
        }

        //监听客户端请求方法
        //定义一个集合，存储客户端信息
        static Dictionary<string, Socket> clientConnectionItems = new Dictionary<string, Socket>();
        static void watchConnecting()
        {
            Socket connection = null;
            //持续不断监听客户端请求
            while (true)
            {
                try
                {
                    connection = socketwatch.Accept();
                }
                catch (Exception ex)
                {
                    //提示接收异常
                    MessageBox.Show(ex.Message);
                    throw;
                }
                IPAddress clientIP = (connection.RemoteEndPoint as IPEndPoint).Address;
                int clientPort = (connection.RemoteEndPoint as IPEndPoint).Port;
                //让客户显示"连接成功的"的信息  
                string sendmsg = "连接服务端成功！\r\n" + "本地IP:" + clientIP + "，本地端口" + clientPort.ToString();
                byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendmsg);
                connection.Send(arrSendMsg);

                string remoteEndPoint = connection.RemoteEndPoint.ToString();
                //显示连接成功信息
                Console.Write("成功与" + remoteEndPoint + "客户端建立连接！\t\n");
                //添加客户端信息
                clientConnectionItems.Add(remoteEndPoint, connection);
                //创建一个通信线程
                ParameterizedThreadStart pts = new ParameterizedThreadStart(recv);
                Thread thread = new Thread(pts);
                //设置为后台线程
                thread.IsBackground = true;
                //启动线程
                thread.Start(connection);
            }
        }

        //接收客户端发过来的消息
        static void recv(object socketClientPara)
        {
            //创建一个内存缓冲区，其大小为1024字节
            Socket socketClient = socketClientPara as Socket;
            while (true)
            {
                byte[] arrServerRecMsg = new byte[1024];
                try
                {
                    //将接收到的信息存入到内存缓冲区，并返回其字节数组的长度
                    int length = socketClient.Receive(arrServerRecMsg);
                    if (arrServerRecMsg[0]==0xf4)
                    {
                        MessageBox.Show("666");
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        //存数据入数据库
        private void SaveDataIntoSql()
        {
            StringBuilder UpdateStb = new StringBuilder();
            for (int i = 0; i < 250; i++)
            {
                UpdateStb.Append(floatRegisterData[i] + "*");
            }
            try
            {
                SqlHelper.ExecuteNonQuery("update Temp_Table set value='" + UpdateStb.ToString() + "' where id = 1;");
            }
            catch (Exception)
            {

                Console.Write("写数据库出错");
            }
           
        }

        //数据库记录写入方法
        private void Record_WriteInto_Sql()
        {
            StringBuilder writeRecordString = new StringBuilder();
            string stringInit = "INSERT INTO MAT_Chemical_Line.dbo.Simple_RecordTable (value,RecordTime,TypeId) VALUES ";
            writeRecordString.Append(stringInit);
            for (int i = 1; i < 213; i++)
            {
                string apenndText = string.Format("('{0}',{1},'{2}'),", floatRegisterData[i], "GETDATE()", i);
                writeRecordString.Append(apenndText);
            }
            writeRecordString.Remove(writeRecordString.Length - 1,1);
            SqlHelper.ExecuteNonQuery(writeRecordString.ToString());
        }

        //定时器定时执行方法
        private int timerCount60S = 0;//60s计时count
        private void timer1_Tick(object sender, EventArgs e)
        {
            GetPlcRegisterData();
            CalculateData();
            SaveDataIntoSql();
            timerCount60S++;
            //每进一次加一
            if (timerCount60S >= 6)
            {
                timerCount60S = 0;
                Record_WriteInto_Sql();
            }
        }
    }
}
