using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������ת��������
/// </summary>
public class KeepRotation : MonoBehaviour
{
    public Vector3 aimRotation;
    void Update()
    {
        transform.eulerAngles = aimRotation;
    }
}
