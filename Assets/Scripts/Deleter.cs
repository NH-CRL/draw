using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deleter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            DeleteLine();
        }
    }
    
    void DeleteLine()
    {
        //これがダメならタグを使おう
        //GameObject.FindGameObjectsWithTag("TargetTag")
        GameObject deleteLine;
        deleteLine = GameObject.Find("LineObjectPrefab");
        Destroy(deleteLine);
    }
}
