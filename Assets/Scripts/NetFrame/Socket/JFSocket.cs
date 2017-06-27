using UnityEngine;
using System.Collections;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
namespace DataFrame.Net
{

    public class JFSocket
    {
        class CReciverData
        {
            public const int BufferDataLength = 1024;
            public byte[] data;
            public int len;
            public CReciverData()
            {
                len = 0;
                data = null;
            }
            /// <summary>
            /// 获取包含CMD头长度的CMD数据
            /// </summary>
            /// <returns></returns>
            public int getCmdLength()
            {
                if (len <= 4) return -1;
                byte[] cmd_data = new byte[4];
                Array.Copy(data, 0, cmd_data, 0, 4);
                Array.Reverse(cmd_data, 0, 4);
                int cmd_length = BitConverter.ToInt32(cmd_data, 0) + 4;
                return cmd_length;
            }

            /// <summary>
            /// 获取不包含数据头长度的CMD数据
            /// </summary>
            /// <returns></returns>
            public byte[] getOneCmdData()
            {

                int cmd_len = this.getCmdLength();
                if (cmd_len > 0 &&
                    cmd_len <= len)
                {
                    byte[] cmd_data = new byte[cmd_len - 4];
                    Array.Copy(data, 4, cmd_data, 0, cmd_len - 4);
                    this.remove(cmd_len);
                    return cmd_data;
                }
                return null;
            }


            /// <summary>
            /// 增加数据到缓冲区中
            /// </summary>
            /// <param name="d"></param>
            /// <param name="l"></param>
            public void add(byte[] d, int l)
            {
                if (len == 0)
                {
                    this.data = d;
                    this.len = l;
                }
                else
                {
                    byte[] nw_data = new byte[len + l];
                    Array.Copy(data, 0, nw_data, 0, len);
                    Array.Copy(d, 0, nw_data, len, l);
                    this.len += l;
                    this.data = nw_data;
                }
            }

            /// <summary>
            /// 删除缓冲区中的数据
            /// </summary>
            /// <param name="l"></param>
            public void remove(int l)
            {
                if (l - len >= 0)
                {
                    len = 0;
                    data = null;
                }
                else
                {
                    byte[] nw_data = new byte[len - l];
                    Array.Copy(data, l, nw_data, 0, len - l);
                    len -= l;
                    this.data = nw_data;
                }
            }



            public CReciverData(byte[] d, int l)
            {
                data = d;
                len = l;
            }
        }

        private Socket clientSocket;

        public delegate void ConnectedResult(bool rs);

        public delegate void DeleReceiveMessage(string str);

        public DeleReceiveMessage WhenReceiveMessageCalled;

        bool success;

        public JFSocket() { }

        /// <summary>
        /// socket 链接
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="callback"></param>
        public void ConnectServer(string ip, int port, ConnectedResult callback)
        {

            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint ipEndpiont = new IPEndPoint(ipAddress, port);
                clientSocket.Connect(ipEndpiont);
                if (clientSocket.Connected)
                {
                    Thread thread = new Thread(new ThreadStart(ReceiveSorket));
                    thread.IsBackground = true;
                    thread.Start();
                    callback(true);
                }
                else
                {
                    callback(false);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                callback(false);
            }

        }

        /// <summary>
        /// 是否超时
        /// </summary>
        public bool isSuccess
        {
            get { return success; }
        }

        /// <summary>
        /// 是否连接上了
        /// </summary>
        public bool isConnected
        {
            get
            {
                if (clientSocket == null)
                {
                    return false;
                }
                return clientSocket.Connected;
            }

        }

        /// <summary>
        /// 收包
        /// </summary>
        private void ReceiveSorket()
        {
            try
            {
                CReciverData recive_data_buffer = new CReciverData();
                while (true)
                {
                    if (clientSocket == null ||
                        !clientSocket.Connected)
                    {
                        clientSocket.Close();
                        break;
                    }
                    if (clientSocket.Available <= 0)
                    {
                        continue;
                    }
                    CReciverData rc = new CReciverData();
                    rc.data = new byte[CReciverData.BufferDataLength];
                    rc.len = clientSocket.Receive(rc.data);
                    SplitPackage(recive_data_buffer, rc);
                }
            }
            catch (Exception e)
            {
                MyLogger.LogError("[Socket Lower Reciver]Socer Recive Error：" + e);
            }
        }

        private void SplitPackage(CReciverData buffer, CReciverData reciver)
        {

            buffer.add(reciver.data, reciver.len);
            try
            {
                do
                {
                    byte[] cmd_data = buffer.getOneCmdData();
                    //当没有数据可以解析的时候跳出
                    if (cmd_data == null) break;
                    PBDataManager.Instance.GetBaseDataFromPb(cmd_data);

                    PB_BaseData cpbdata = PB_BaseData.Create(cmd_data);
                    //MyLogger.Log("[Socket Lower Reciver] Recive：" + cpbdata.ToString());

                }
                while (true);
            }
            catch (Exception ex)
            {
                MyLogger.LogError("[Socket Lower Reciver]Data Press Error：" + ex);
                // 清理缓冲数据
                buffer.data = null;
                buffer.len = 0;
            }

        }

        /// <summary>
        /// 发送一个对象
        /// </summary>
        /// <param name="data"></param>
        public bool SendMessage(byte[] data)
        {
            if (!clientSocket.Connected)
            {
                MyLogger.LogError("[Socket Lower Sender]停止接受数据~！~");
                return false;
            }
            try
            {
                int length = data.Length;
                byte[] head = BitConverter.GetBytes(length);
                Array.Reverse(head, 0, 4);
                byte[] pushdata = new byte[length + head.Length];
                Array.Copy(head, pushdata, head.Length);
                Array.Copy(data, 0, pushdata, head.Length, length);
                IAsyncResult asyncSend = clientSocket.BeginSend(pushdata
                    , 0
                    , pushdata.Length
                    , SocketFlags.None
                    , new AsyncCallback(sendCallback)
                    , clientSocket);

                bool waitOne = asyncSend != null && asyncSend.AsyncWaitHandle.WaitOne(50000, true);
                if (!waitOne)
                {
                    MyLogger.LogC2S("[Socket Lower Sender]联结发送服务器失败");
                    return false;
                }
                else
                {
                    PB_BaseData cpbdata = PB_BaseData.Create(data);
                    MyLogger.LogC2S("[Socket Lower Sender]发送：" + cpbdata.ToString());
                    return true;
                }
            }
            catch (Exception e)
            {
                MyLogger.LogC2S("[Socket Lower Sender]发送失败 " + e.ToString());
                return false;
            }
        }
        /// <summary>
        /// 发送会掉
        /// </summary>
        /// <param name="asyncSend"></param>
        private void sendCallback(IAsyncResult asyncSend)
        {
            //Debug.Log("发送回掉： "+asyncSend.IsCompleted+",,,,ddd:"+asyncSend.CompletedSynchronously);
        }
        /// <summary>
        /// 关闭socket
        /// </summary>
        public void Closed()
        {
            if (clientSocket != null && clientSocket.Connected)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            clientSocket = null;
        }


        /// <summary>
        /// 获取当时时间
        /// </summary>
        /// <returns></returns>
        private string GetTime()
        {
            System.DateTime dt = System.DateTime.Now;
            string timeNow = dt.Hour + ":" + dt.Minute + ":" + dt.Second + ":" + dt.Millisecond;
            return timeNow;
        }

    }
}

