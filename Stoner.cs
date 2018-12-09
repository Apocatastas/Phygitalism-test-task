using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stoner : MonoBehaviour
{

    private int xLen;
    private int yLen;
    private int zLen;
    public int minLen;

    public class JsonRawData // Storing data from JSON
    {
        public float[] x;
        public float[] y;
        public float[] z;

    };

    public JsonRawData fileOutputClass;


    private void Start()
    {
        minLen = 0;
        xLen = 0;
        yLen = 0;
        zLen = 0;
        WhosShorter(); // min length search (if json is not complete)
                       // filePath = "Assets/Resources/ball_path.json";


        Debug.Log("Min Len" + minLen);
    }



    public void SetJStonerOutput(string what) 
    {
        fileOutputClass = JsonUtility.FromJson<JsonRawData>(what);
    }


   

    private void WhosShorter()  // Check for shortest data stream if file is not complete. We could only use complete data

    {

        xLen = fileOutputClass.x.Length;
        yLen = fileOutputClass.y.Length;
        zLen = fileOutputClass.z.Length;

        if (xLen > yLen)
        {
            if (yLen > zLen)
            {
                minLen = zLen;
            }
            else
            {
                minLen = yLen;
            }


        }
        else
        {
            if (xLen > zLen)
            {
                minLen = zLen;
            }
            else
            {
                minLen = xLen;
            }

        }
    }

    public Vector3 GetPointByStep(int what)
    {  // Getting next point of route while we got current route step

        Vector3 point = Vector3.zero;
        point.x = fileOutputClass.x[what];
        point.y = fileOutputClass.y[what];
        point.z = fileOutputClass.z[what];
        return point;
    }
}
