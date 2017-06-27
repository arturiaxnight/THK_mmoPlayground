using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class CommunicateCS : MonoBehaviour
{
    string serverIP = "666.666.666.666"; //IP框架初始提示字串

    Socket socket;
    EndPoint serverEnd;
    IPEndPoint ipEnd;
    string recvStr;
    string sendStr;
    byte[] recvData = new byte[1024];
    byte[] sendData = new byte[1024];
    int recvLen;
    Thread connectThread;

    void InitSocket()
    {
        ipEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7938);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        serverEnd = (EndPoint)sender;
        print("等待傳送UDP dgram");

        SocketSend("yolo");

        connectThread = new Thread(new ThreadStart(SocketReceive));
        connectThread.Start();

    }

    void SocketSend(string sendStr)
    {
        sendData = new byte[1024];
        sendData = Encoding.ASCII.GetBytes(sendStr);
        socket.SendTo(sendData, sendData.Length, SocketFlags.None, ipEnd);
    }

    void SocketReceive()
    {
        while (true)
        {
            recvData = new byte[1024];
            recvLen = socket.ReceiveFrom(recvData, ref serverEnd);
            print("訊息來自: " + serverEnd.ToString());
            recvStr = Encoding.ASCII.GetString(recvData, 0, recvLen);
            print(recvStr);
        }
    }

    void socketQuit()
    {
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        if (socket != null)
        {
            socket.Close();
        }
    }

    private void Start()
    {
        InitSocket();
    }

    private void OnGUI()
    {
        serverIP = GUI.TextField(new Rect(10, 10, 100, 20), serverIP, 15);
        if (GUI.Button(new Rect(10, 30, 60, 20), "連線!"))
        {
            SocketSend(serverIP);
        }
    }

    private void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        socketQuit();
    }
}