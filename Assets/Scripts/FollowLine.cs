using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLine : MonoBehaviour
{
    [SerializeField]
    private Vector3 startPoint;
    [SerializeField]
    private Vector3 endPoint;
    [SerializeField]
    private Vector3 currentPoint;//something
    private Vector3 length;
    private float mSlope;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        startPoint = this.transform.position;
        currentPoint = startPoint;
        player = GameObject.FindGameObjectWithTag("Player");

        //Find the parametric equation for each variable in the line
        //Finding the length of the line
        length.x = endPoint.x - startPoint.x;
        length.y = endPoint.y - startPoint.y;
        length.z = endPoint.z - startPoint.z;

        mSlope = -(endPoint.x - startPoint.x) / (endPoint.z - startPoint.z);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = findPointOfIntersection();
    }

    private Vector3 findPointOfIntersection()
    {
        //Find the perpendicular line from the player point
        Vector3 playerPosition = player.transform.position;
        Debug.Log("Player Vector: " + playerPosition);
        float e = playerPosition.x + mSlope * 100000;
        float d = playerPosition.z + 100000;

        //3d -> 2d
        Vector2 p1 = new Vector2(startPoint.x, startPoint.z);
        Vector2 p2 = new Vector2(endPoint.x, endPoint.z);

        Vector2 p3 = new Vector2(playerPosition.x, playerPosition.z);
        Vector2 p4 = new Vector2(d, e);

        float denominator = (p4.y - p3.y) * (p2.x - p1.x) - (p4.x - p3.x) * (p2.y - p1.y);
        Debug.Log("Float d: " + d);
        Debug.Log("Float e: " + d);

        if (denominator != 0)
        {
            float u_a = ((p4.x - p3.x) * (p1.y - p3.y) - (p4.y - p3.y) * (p1.x - p3.x)) / denominator;
            float u_b = ((p2.x - p1.x) * (p1.y - p3.y) - (p2.y - p1.y) * (p1.x - p3.x)) / denominator;

            Debug.Log("U-A: " + u_a);
            Debug.Log("U-B: " + u_b);

            if (u_a < 0)
            {
                u_a = 0;
            } else if(u_a > 1)
            {
                u_a = 1;
            }

            currentPoint.x = startPoint.x + u_a * length.x;
            currentPoint.y = startPoint.y + u_a * length.y;
            currentPoint.z = startPoint.z + u_a * length.z;
        }

        return currentPoint;
    }
}

/*
 * Parametric Line Intersection Point Equation from: https://www.habrador.com/tutorials/math/5-line-line-intersection/
 */
