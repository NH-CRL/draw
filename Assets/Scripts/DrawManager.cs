using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour {

    //変数を用意
    //SerializeFieldをつけるとInspectorウィンドウからゲームオブジェクトやPrefabを指定できます。
    [SerializeField] GameObject LineObjectPrefab;
    public bool preCollisionFlag;
    public bool currentCollisionFlag;
    

    //現在描画中のLineObject;
    private GameObject CurrentLineObject = null;
    

    // Use this for initialization
    void Start ()
    {
        preCollisionFlag = false;
        currentCollisionFlag = false;
    }


    // Update is called once per frame
    void Update ()
    {
        if (recv.recv_CollisionFlag == 1)
        {
            currentCollisionFlag = true;
        }
        else
        {
            currentCollisionFlag = false;
        }
        
        // ここから追加コード

        //ペンが紙面に接触しているとき
        if(currentCollisionFlag)
        {
            if(CurrentLineObject == null)
            {
                //PrefabからLineObjectを生成
                CurrentLineObject = Instantiate(LineObjectPrefab, recv.recv_pen_tip_position, Quaternion.identity);
            }
            //ゲームオブジェクトからLineRendererコンポーネントを取得
            LineRenderer render = CurrentLineObject.GetComponent<LineRenderer>();

            //LineRendererからPositionsのサイズを取得
            int NextPositionIndex = render.positionCount;

            //LineRendererのPositionsのサイズを増やす
            render.positionCount = NextPositionIndex + 1;

            //LineRendererのPositionsに現在のコントローラーの位置情報を追加
            /*render.SetPosition(NextPositionIndex, pointer.position);*/
            render.SetPosition(NextPositionIndex, recv.recv_pen_tip_position);
        } 
        else if (currentCollisionFlag == false & preCollisionFlag == true)//ペンが紙面から離れたとき
        {
            if(CurrentLineObject != null)
            {
                //現在描画中の線があったらnullにして次の線を描けるようにする。
                CurrentLineObject = null;
            }
        }
        preCollisionFlag = currentCollisionFlag;
    }
}