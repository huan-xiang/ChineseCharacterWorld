using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public int time = 0;
    public int destroyTime;
    void Update()
    {
        /*²»Ïú»Ù*/
        if(time == -1)
        {
            return;
        }
        if (time >= destroyTime)
        {
            Destroy(gameObject);
        }
        else
            time++;
    }
}
