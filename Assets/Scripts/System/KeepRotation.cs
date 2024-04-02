using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 保持旋转世界坐标
/// </summary>
public class KeepRotation : MonoBehaviour
{
    public Vector3 aimRotation;
    void Update()
    {
        transform.eulerAngles = aimRotation;
    }
}
