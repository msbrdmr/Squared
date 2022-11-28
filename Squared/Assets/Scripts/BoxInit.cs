using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxInit : MonoBehaviour
{
    [SerializeField] private float time;
    // [SerializeField] private float time;
    [SerializeField] private Slider slider;
    [SerializeField] private ObjectPool objectpool;
    [SerializeField] Transform camTransform;
    [SerializeField] private float force = 5f;
    private static bool mouseFirstPosTaken;
    private static bool mouseSecondPosTaken;
    // public static BoxInit Instance;
    static Vector3 mousestartPos;
    static Vector3 mouseFinishPos;
    static Vector3 difference;
    static float forceVector;
    public static float mouseXdif;
    public static float mouseXstart;
    public static float mouseXcurrent;
    static Vector2 forceV2;

    void Update()
    {
        // calculateMagnitude();
        // getData();
        // Instance = this;
        // Debug.Log(getXdiff());
    }
    private IEnumerator coroutine()
    {
        while (true)
        {
            GameObject obj = objectpool.getCubefromPool();
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.transform.position = camTransform.position;
            rb.AddForce(new Vector3(-force, 0, 0), ForceMode.Impulse);
            yield return new WaitForSeconds(time);
        }
    }

    public void throwBox(Vector3 direction, Vector3 forcevector)
    {
        //Debug.Log("space");
        GameObject obj = objectpool.getCubefromPool();
        obj.GetComponent<CubeEditor>().hasSpeed = true;
        float fr = forcevector.magnitude / 10;
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.transform.position = camTransform.position;
        // rb.velocity = direction * fr;
        rb.AddForce(direction * fr, ForceMode.Impulse);
    }

    public void getData()
    {
        if (Input.GetMouseButtonUp(0))

        {


            if (-1f < difference.x / difference.y && difference.x / difference.y < 1f)
            {
                //Vector3 randDir = new Vector3(-1, 0, Random.Range(-.5f, .5f));
                //forceV2=new Vector2(difference.x,difference.y);
                //throwBox(new Vector3(-1, 0, difference.x / difference.y), difference);
                //Debug.Log(randDir);
                Debug.Log("VALUE = " + (difference.x / difference.y));
            }
            else if (difference.x / difference.y < -.8f)
            {
                Debug.Log("VALUE = " + -.8f);
                //    throwBox(new Vector3(-1f, 0f, -.8f), difference);
            }
            else if (difference.x / difference.y > .8f)
            {
                Debug.Log("VALUE = " + .8f);
                //    throwBox(new Vector3(-1f, 0f, .8f), difference);
            }
        }
    }



    public float getXdiff()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseXstart = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            mouseXcurrent = Input.mousePosition.x;
        }

        return mouseXcurrent - mouseXstart;

    }

    public Vector3 calculateMagnitude()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (!mouseFirstPosTaken)
            {
                mousestartPos = Input.mousePosition;

                mouseFirstPosTaken = true;

            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (!mouseSecondPosTaken)
            {
                mouseFinishPos = Input.mousePosition;
                mouseSecondPosTaken = true;
                difference = mouseFinishPos - mousestartPos;

                // Debug.Log("second = " + mouseFinishPos);
                // Debug.Log("first = " + mousestartPos);
                // Debug.Log("diff" + difference);

            }
        }

        if (mouseSecondPosTaken)
        {
            difference = mouseFinishPos - mousestartPos;
            forceVector = difference.magnitude;

        }


        mouseFirstPosTaken = false;
        mouseSecondPosTaken = false;
        return difference;
    }

}
