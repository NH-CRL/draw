using System.IO;
using UnityEngine;

public class LineRendererToCSV : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public string fileName = "lineRendererData.csv";

    void Start()
    {
        SaveLineRendererToCSV();
    }

    void SaveLineRendererToCSV()
    {
        // 保存先のパスを設定
        string filePath = Path.Combine(Application.dataPath, fileName);

        // LineRendererの頂点数を取得
        int positionCount = lineRenderer.positionCount;

        // CSVデータを生成するためのストリームを開く
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // ヘッダーを書き込む
            writer.WriteLine("Index,X,Y,Z");

            // 各頂点の座標を取得して書き込む
            for (int i = 0; i < positionCount; i++)
            {
                Vector3 position = lineRenderer.GetPosition(i);
                writer.WriteLine($"{i},{position.x},{position.y},{position.z}");
            }
        }

        Debug.Log($"CSVファイルが保存されました: {filePath}");
    }
}