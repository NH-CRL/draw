using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen_tip : MonoBehaviour
{
    void Start()
    {
    }
    void Update()
    {
        Vector3 agent_vector = new Vector3(recv.recv_pen_tip_position.x, recv.recv_pen_tip_position.y, recv.recv_pen_tip_position.z); //ペン先の位置を代入
        transform.localPosition = agent_vector; //ペン先を動かす
    }
}