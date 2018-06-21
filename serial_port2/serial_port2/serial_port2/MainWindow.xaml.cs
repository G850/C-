using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace serial_port2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)//主窗口初始化  
        {

        }

        //声明串口！！！！！！！！！！！！  
        SerialPort ComPort = new SerialPort();
        //串口集  
        public string[] portNames { get; set; }
        //打开标志  
        bool openFlag = false;
        //得到当前可用串口  
        private void GetPort()
        {
            portNames = SerialPort.GetPortNames();                      //得到可用串口  

            if (portNames.Length > 0)
            {
                txtTip.Text = "检测到" + portNames.Length + "个可用串口";
            }
            else
            {
                txtTip.Text = "未检测到串口";
                cbbPort.ItemsSource = null;
                return;

            }
            cbbPort.ItemsSource = portNames;           //添加可用串口  
            cbbPort.SelectedValue = portNames[0];      //默认选中  
        }
        //打开事件  
        private void btnPort_Click(object sender, RoutedEventArgs e)
        {
            OpenPort();
        }
        //鼠标进入事件  
        private void cbbPort_MouseEnter(object sender, MouseEventArgs e)
        {
            if (openFlag == false)
            {
                GetPort();
            }
        }
        //打开串口方法  
        private void OpenPort()
        {
            if (cbbPort.SelectedValue == null)
            {
                GetPort();
            }
            if (openFlag == false)
            {
                try
                {
                    //设置  
                    ComPort.PortName = cbbPort.SelectedValue.ToString();
                    ComPort.BaudRate = 115200;
                    ComPort.Parity = Parity.None;
                    ComPort.StopBits = StopBits.One;
                    ComPort.DataBits = 8;
                    ComPort.Handshake = Handshake.None;
                    // ComPort.RtsEnable = true;  

                    ComPort.Open();
                    txtTip.Text = "打开成功";
                    openFlag = true;
                    btnPort.Content = "关闭串口";
                }
                catch
                {
                    txtTip.Text = "打开失败";
                }
            }
            else
            {
                try
                {
                    ComPort.DiscardInBuffer();
                    ComPort.DiscardOutBuffer();
                    ComPort.Close();
                    openFlag = false;
                    btnPort.Content = "打开串口";
                    txtTip.Text = "关闭成功";
                }
                catch
                {
                    txtTip.Text = "关闭失败";
                }
            }
        }
        //发送方法  
        private bool sendData(byte[] sendBuffer)
        {
            if (openFlag == false)
            {
                txtTip.Text = "请先打开串口";
                return false;
            }
            try
            {
                ComPort.Write(sendBuffer, 0, sendBuffer.Length);
                ComPort.DiscardOutBuffer();
                return true;
            }
            catch
            {
                txtTip.Text = "发送数据失败";
                return false;
            }
        }
        //接收方法  
        private void receiveData(byte[] receiveBuffer)
        {
            receiveBuffer = new byte[ComPort.BytesToRead];
            try
            {
                ComPort.Read(receiveBuffer, 0, receiveBuffer.Length);
                ComPort.DiscardInBuffer();
            }
            catch
            {
                txtTip.Text = ("读取失败");
            }
        }
        //按钮例子，调用发送  
        private void btn_test_Click(object sender, RoutedEventArgs e)
        {
            byte[] send_data = new byte[5];
            send_data[0] = 0xff;
            send_data[1] = 0xff;
            send_data[2] = 0xff;
            send_data[3] = 0xff;
            send_data[4] = 0xff;
            sendData(send_data);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
