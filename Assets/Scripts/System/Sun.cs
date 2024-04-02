using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Sun : MonoBehaviour
{
    public float time;
    /// <summary>
    /// һ��һ������֡
    /// </summary>
    public float totalDaytime;
    /// <summary>
    /// ��ͼ��������
    /// </summary>
    public GameObject worldCenter;
    /// <summary>
    /// ̫����
    /// </summary>
    public Light2D sunLight;
    void Start()
    {
        
    }

    void Update()
    {
        if(time < totalDaytime)
        {
            time++;
        }
        else
        {
            time = 0;
        }
        if (time >= totalDaytime / 2f)
        {
            sunLight.color = Color.black;
        }
        else
        {
            sunLight.color = Color.white;
        }
        float newX = worldCenter.transform.position.x + 50 * Mathf.Cos(time / totalDaytime * 2 * Mathf.PI);
        float newY = worldCenter.transform.position.y + 50 * Mathf.Sin(time / totalDaytime * 2 * Mathf.PI);
        transform.position = new Vector2(newX, newY);
    }
}
