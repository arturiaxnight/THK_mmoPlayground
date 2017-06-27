using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour {

	readonly string _ip = "127.0.0.1"; //自己對外IP
    readonly int _port = 7938; //連接port

	public void OnPointerClick(PointerEventData eventData){
		Debug.Log("Click");
		Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);
            socket.Connect(_ip, _port);
            NetworkStream stream = new NetworkStream(socket);
            StreamWriter sw = new StreamWriter(stream);
            StreamReader sr = new StreamReader(stream);

            sw.WriteLine("這裡是客戶端");
            sw.Flush();

            string st = sr.ReadLine();
            print(st);

            sw.Close();
            stream.Close();
            socket.Close();
	}
}
