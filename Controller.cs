using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class Controller : MonoBehaviour
{
    public GameObject spherePrefab;
    public GameObject[] balls;
    private Camera[] cameras;
    public int ballcount;
    private int camcount;
    int ballsChecksum;



    private void Start()
    {

        ballcount = 4;
        camcount = ballcount;
        balls = new GameObject[ballcount];
        cameras = new Camera[camcount];
        ballsChecksum = 0;

        //BALLS INSTANTIATION

        for (int i = 0; i < ballcount; i++)

        {
            balls[i] = Instantiate(spherePrefab);
            if (i == 0)
            {
                balls[i].GetComponent<Ball>().SetRouteFilePath("Assets/Resources/ball_path.json");
            }
            else 
            {
                balls[i].GetComponent<Ball>().SetRouteFilePath("Assets/Resources/ball_path" + (i + 1) + ".json");
            }
            if (balls[i] != null) 
            {
                ballsChecksum++;

}


        }
        Debug.Log(ballsChecksum + " balls was throwed");
        //--

    }





}
