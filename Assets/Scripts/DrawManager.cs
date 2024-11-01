using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] GameObject LineObjectPrefab;
    [SerializeField] DrawCircle drawCircle;  // DrawCircleスクリプトへの参照
    public bool preCollisionFlag;
    public bool currentCollisionFlag;

    private GameObject CurrentLineObject = null;
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

    void Update()
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
        
        if(currentCollisionFlag)
        {
            if(CurrentLineObject == null)
            {
                CurrentLineObject = Instantiate(LineObjectPrefab, recv.recv_pen_tip_position, Quaternion.identity);
                LineObjects.Add(CurrentLineObject);
            }

            LineRenderer render = CurrentLineObject.GetComponent<LineRenderer>();
            int NextPositionIndex = render.positionCount;
            render.positionCount = NextPositionIndex + 1;
            render.SetPosition(NextPositionIndex, recv.recv_pen_tip_position);

            // 描いた点をリストに追加
            drawnPoints.Add(recv.recv_pen_tip_position);
        } 
        else if (currentCollisionFlag == false & preCollisionFlag == true)
        {
            if(CurrentLineObject != null)
            {
                CurrentLineObject = null;
                CalculateAverageDistance();  // 描画が終わったら距離計算
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            foreach (GameObject line in LineObjects)
            {
                Destroy(line);
            }
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
