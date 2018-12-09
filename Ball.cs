using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

public class Ball : MonoBehaviour, IPointerClickHandler
{

    public int routePoint;
    public Vector3 currentRouteStart;
    public bool isMoving;
    public bool isStopped;
    public bool clickHandler;
    private Vector3 nextPosition;
    private Controller controller;
    public float maxDistanceDelta;
    public int counter;
    public string filePath;
    float doubleClickStart;
    private Slider slider;
    private float multiplier;
    public Stoner jStoner;
    private string jsonContents;



    public void SetCurrentRouteStart(Vector3 what) // Route start setter
    {
        currentRouteStart = what;
    }

    private void Start()
    {
        isMoving = false;
        isStopped = false;
        controller = FindObjectOfType<Controller>();
        maxDistanceDelta = 0.5f;
        clickHandler = false;
        counter = 0;
        routePoint = 0;


        currentRouteStart = Vector3.zero;

        slider = FindObjectOfType<Slider>();
        jStoner = FindObjectOfType<Stoner>();

    }

    public void SetRouteFilePath(string what) 
    {
        filePath = what;
        jsonContents = File.ReadAllText(filePath);
        jStoner.SetJStonerOutput(jsonContents);
    }

    public string GetRouteFilePath()
    {
        return filePath;
    }


    public void SetMaxDistance() 
    {
        Debug.Log("prev "+maxDistanceDelta+"\n");

        if (Mathf.Abs(multiplier) > 0) { maxDistanceDelta = maxDistanceDelta * multiplier; }; //we ran out of range. 
        //Make a method to maximize and minimize!

        Debug.Log("next " + maxDistanceDelta + "\n");
    }

    public Vector3 CoordsTransform(Vector3 what)  //Adding start correction to readed route point 
    {
        Vector3 resultTranformation;
        resultTranformation = Vector3.zero;
        resultTranformation.x = currentRouteStart.x + what.x;
        resultTranformation.y = currentRouteStart.y + what.y;
        resultTranformation.z = currentRouteStart.z + what.z;

        return resultTranformation;
    }


    private void Update()
    {
   
     multiplier = slider.value;
    if (clickHandler == true) {  Move(); }


    }

    private void Move()
    {
        if (Mathf.Abs(multiplier) > 0)
        {


            if (!isStopped)
            {
                if (routePoint < jStoner.minLen - 1)
                {
                    SetMaxDistance();

                    isMoving = true;

                    // Debug.Log("point " + controller.routePoint + "\n");
                    //  Debug.Log("we are " + transform.position + "\n");
                    //  Debug.Log("going to " + controller.CoordsTransform(controller.GetPointByStep(controller.routePoint + 1)) + "\n");

                    // Movement into one route point       

                    if (transform.position != CoordsTransform(jStoner.GetPointByStep(routePoint + 1)))
                    {
                        transform.position = Vector3.MoveTowards(transform.position, CoordsTransform(jStoner.GetPointByStep(routePoint + 1)), maxDistanceDelta);
                        //    Debug.Log("came to  " + transform.position + "\n");
                    }
                    else
                    {

                        // moving into one point is finished, so we increment to next point

                        routePoint++;
                        counter++;
                        Debug.Log("upd point " + routePoint + " count " + counter + "\n");
                        if (transform.position == CoordsTransform(jStoner.GetPointByStep(jStoner.minLen - 1)))
                        {
                            isMoving = false;
                            currentRouteStart = transform.position;
                            clickHandler = false;
                            routePoint = 0;
                            SetCurrentRouteStart(transform.position);
                        }

                    }
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      //  isStopped = false;
        doubleClickStart = Time.time;

        if (eventData.pointerId == -1)  // Get the left click
        {
            if (isMoving == false) { clickHandler = true; }
        }
    }

    public void OnMouseUp()
    {
        float maxDoubleClickTime = 0.5f;

        if ((Time.time - doubleClickStart)<maxDoubleClickTime) 
        {
            transform.position = Vector3.zero;
            isMoving = false;
            clickHandler = false;
           routePoint = 0;
            currentRouteStart = Vector3.zero;
            isStopped = true;
            GetComponent<TrailRenderer>().time = 0;

        }

        else

        {
            isStopped = false;
            doubleClickStart = Time.time;
            GetComponent<TrailRenderer>().time = 500;
        }



    }

}
