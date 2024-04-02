using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public int newZ = 0;
    void Update()
    {
        if(newZ < 360)
        {
            newZ++;
        }
        else
        {
            newZ = 0;
        }
        transform.eulerAngles = new Vector3(0, 0,newZ);
    }
}
