using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class DrawManager : MonoBehaviour
{
=======
public class DrawManager : MonoBehaviour {

    //変数を用意
>>>>>>> origin/master
    [SerializeField] GameObject LineObjectPrefab;
    [SerializeField] DrawCircle drawCircle;  // DrawCircleスクリプトへの参照
    public bool preCollisionFlag;
    public bool currentCollisionFlag;

    private GameObject CurrentLineObject = null;
<<<<<<< HEAD
=======

    //生成されたLineObjectを格納するリスト
>>>>>>> origin/master
    private List<GameObject> LineObjects = new List<GameObject>();

    // 描いた線の座標を格納
    private List<Vector3> drawnPoints = new List<Vector3>();

    void Start()
    {
        preCollisionFlag = false;
        currentCollisionFlag = false;
        // DrawCircleスクリプトを持つオブジェクトをシーンから自動的に取得
        drawCircle = FindObjectOfType<DrawCircle>();
    
        if (drawCircle == null)
        {
            Debug.LogError("DrawCircleがシーンに存在しません。");
        }
    }

<<<<<<< HEAD
    void Update()
=======
    // Update is called once per frame
    void Update ()
>>>>>>> origin/master
    {
        // 衝突フラグの取得
        if (recv.recv_CollisionFlag == 1)
        {
            currentCollisionFlag = true;
        }
        else
        {
            currentCollisionFlag = false;
        }
        
<<<<<<< HEAD
=======
        // ペンが紙面に接触しているとき
>>>>>>> origin/master
        if(currentCollisionFlag)
        {
            if(CurrentLineObject == null)
            {
<<<<<<< HEAD
=======
                //PrefabからLineObjectを生成し、リストに追加
>>>>>>> origin/master
                CurrentLineObject = Instantiate(LineObjectPrefab, recv.recv_pen_tip_position, Quaternion.identity);
                LineObjects.Add(CurrentLineObject);
            }

            LineRenderer render = CurrentLineObject.GetComponent<LineRenderer>();
            int NextPositionIndex = render.positionCount;
            render.positionCount = NextPositionIndex + 1;
<<<<<<< HEAD
=======

            //LineRendererのPositionsに現在のコントローラーの位置情報を追加
>>>>>>> origin/master
            render.SetPosition(NextPositionIndex, recv.recv_pen_tip_position);

            // 描いた点をリストに追加
            drawnPoints.Add(recv.recv_pen_tip_position);
        } 
<<<<<<< HEAD
        else if (currentCollisionFlag == false & preCollisionFlag == true)
=======
        else if (currentCollisionFlag == false & preCollisionFlag == true) //ペンが紙面から離れたとき
>>>>>>> origin/master
        {
            if(CurrentLineObject != null)
            {
                CurrentLineObject = null;
                CalculateAverageDistance();  // 描画が終わったら距離計算
            }
        }

<<<<<<< HEAD
=======
        // dキーが押されたら全ての線を削除
>>>>>>> origin/master
        if (Input.GetKeyDown(KeyCode.D))
        {
            foreach (GameObject line in LineObjects)
            {
                Destroy(line);
            }
<<<<<<< HEAD
            LineObjects.Clear();
            drawnPoints.Clear();
        }
        preCollisionFlag = currentCollisionFlag;
    }

    void CalculateAverageDistance()
    {
        List<Vector3> circlePoints = drawCircle.GetCirclePoints();
        if (circlePoints == null || circlePoints.Count == 0)
        {
            Debug.LogError("Circle points are not set or empty.");
            return;
        }

        if (drawnPoints == null || drawnPoints.Count == 0)
        {
            Debug.LogError("Drawn points are not set or empty.");
            return;
        }

        int segmentCount = circlePoints.Count;

        // 描いた線の点数を見本の円の点数に合わせてサンプリング
        List<Vector3> sampledDrawnPoints = ResamplePoints(drawnPoints, segmentCount);

        float totalDistance = 0f;
        for (int i = 0; i < segmentCount; i++)
        {
            // x-z平面での距離を計算
            Vector3 circlePointXZ = new Vector3(circlePoints[i].x, 0, circlePoints[i].z);
            Vector3 drawnPointXZ = new Vector3(sampledDrawnPoints[i].x, 0, sampledDrawnPoints[i].z);

            // x-z平面でのユークリッド距離を計算
            float distance = Vector3.Distance(circlePointXZ, drawnPointXZ);
            totalDistance += distance;
        }

        float averageDistance = totalDistance / segmentCount;
        Debug.Log("平均ユークリッド距離 (x-z平面): " + averageDistance);
    }

    // 点をサンプリングするメソッド
    List<Vector3> ResamplePoints(List<Vector3> originalPoints, int targetCount)
    {
        List<Vector3> resampledPoints = new List<Vector3>();
    
        // 元のリストが空の場合は処理をスキップ
        if (originalPoints == null || originalPoints.Count == 0)
        {
            Debug.LogError("Original points list is empty.");
            return resampledPoints;
        }
    
        float step = (float)originalPoints.Count / targetCount;
    
        for (int i = 0; i < targetCount; i++)
        {
            // サンプリングするインデックスを計算
            int index = Mathf.RoundToInt(i * step);
        
            // インデックスが範囲を超えないように調整
            index = Mathf.Clamp(index, 0, originalPoints.Count - 1);
        
            resampledPoints.Add(originalPoints[index]);
        }
    
        return resampledPoints;
    }
}
=======
            // リストもクリア
            LineObjects.Clear();
        }
        preCollisionFlag = currentCollisionFlag;
    }
}













// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class DrawManager : MonoBehaviour {
//
//     //変数を用意
//     //SerializeFieldをつけるとInspectorウィンドウからゲームオブジェクトやPrefabを指定できます。
//     [SerializeField] GameObject LineObjectPrefab;
//     public bool preCollisionFlag;
//     public bool currentCollisionFlag;
//     
//
//     //現在描画中のLineObject;
//     private GameObject CurrentLineObject = null;
//     
//
//     // Use this for initialization
//     void Start ()
//     {
//         preCollisionFlag = false;
//         currentCollisionFlag = false;
//     }
//
//
//     // Update is called once per frame
//     void Update ()
//     {
//         if (recv.recv_CollisionFlag == 1)
//         {
//             currentCollisionFlag = true;
//         }
//         else
//         {
//             currentCollisionFlag = false;
//         }
//         
//         // ここから追加コード
//
//         //ペンが紙面に接触しているとき
//         if(currentCollisionFlag)
//         {
//             if(CurrentLineObject == null)
//             {
//                 //PrefabからLineObjectを生成
//                 CurrentLineObject = Instantiate(LineObjectPrefab, recv.recv_pen_tip_position, Quaternion.identity);
//             }
//             //ゲームオブジェクトからLineRendererコンポーネントを取得
//             LineRenderer render = CurrentLineObject.GetComponent<LineRenderer>();
//
//             //LineRendererからPositionsのサイズを取得
//             int NextPositionIndex = render.positionCount;
//
//             //LineRendererのPositionsのサイズを増やす
//             render.positionCount = NextPositionIndex + 1;
//
//             //LineRendererのPositionsに現在のコントローラーの位置情報を追加
//             /*render.SetPosition(NextPositionIndex, pointer.position);*/
//             render.SetPosition(NextPositionIndex, recv.recv_pen_tip_position);
//         } 
//         else if (currentCollisionFlag == false & preCollisionFlag == true)//ペンが紙面から離れたとき
//         {
//             if(CurrentLineObject != null)
//             {
//                 //現在描画中の線があったらnullにして次の線を描けるようにする。
//                 CurrentLineObject = null;
//             }
//         }
//         preCollisionFlag = currentCollisionFlag;
//     }
// }
>>>>>>> origin/master
