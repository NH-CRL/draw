using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawCircle : MonoBehaviour
{
    public float radius = 5f;
    public int segments = 100;
    private LineRenderer lineRenderer;
    private List<Vector3> circlePoints = new List<Vector3>();  // 円の座標を格納

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // LineRendererの頂点数を設定
        lineRenderer.positionCount = segments + 1;

        // 始点と終点の色を設定
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.blue, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;

        // 円を描画する
        CreateCircle();
    }

    void CreateCircle()
    {
        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 position = new Vector3(x, 0, z);

            circlePoints.Add(position);  // 座標を保存
            lineRenderer.SetPosition(i, position);

            angle += 2 * Mathf.PI / segments;
        }
    }

    public List<Vector3> GetCirclePoints()  // 座標リストを返す
    {
        return circlePoints;
    }
}
