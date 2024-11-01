using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class recv : MonoBehaviour
{
    public static Vector3 recv_pen_tip_position;
    public static int recv_paintdelete;
    public static int recv_CollisionFlag;
    // public static Vector3 recv_pen_edge_position;


    int LOCALPORT = 12345;
    static UdpClient udp;
    Thread thread;

    void Start()
    {

        Debug.Log("debug");
        udp = new UdpClient(LOCALPORT);//UDPのポートの設定
        thread = new Thread(new ThreadStart(ThreadMethod));//スレッド処理の初期化
        thread.Start();//スレッド処理の開始
        recv_CollisionFlag = 0;
    }

    void Update()
    {
    }

    void OnApplicationQuit()
    {
        thread.Abort();//スレッド終了
    }

    static void ThreadMethod()
    {
        /* UDP受信の初期設定＆初期値の読み込み */
        IPEndPoint remoteEP = null;
        byte[] data = udp.Receive(ref remoteEP);
        string recv_string = Encoding.ASCII.GetString(data);
        string[] stArrayData = recv_string.Split(',');
		
        /* 初期値の代入 */

		
        /* UDP受信のループ */
        while (true)
        {
            
            data = udp.Receive(ref remoteEP);	//UDP受信
            recv_string = Encoding.ASCII.GetString(data);	//文字列に変換
            stArrayData = recv_string.Split(',');	//カンマ区切りで配列に入れる関数

            //Debug.Log(recv_string);
            
            /* ペン先の位置と線の描画を消す変数の代入 */
            recv_pen_tip_position.x = float.Parse(stArrayData[0]);
            recv_pen_tip_position.y = float.Parse(stArrayData[1]);
            recv_pen_tip_position.z = float.Parse(stArrayData[2]);
            recv_CollisionFlag = int.Parse(stArrayData[3]);
            /* recv_paintdelete = int.Parse(stArrayData[3]); */

        }
    }
}