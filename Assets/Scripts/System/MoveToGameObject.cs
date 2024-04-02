using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToGameObject : MonoBehaviour
{
    /// <summary>
    /// Ŀ������
    /// </summary>
    public GameObject aimObj;
    /// <summary>
    /// ƫ����
    /// </summary>
    public float numX;
    public float numY;
    public float speed;
    void Update()
    {
        Vector2 aimPos = new Vector2(aimObj.transform.position.x + numX, aimObj.transform.position.y + numY);
        transform.position = Vector2.MoveTowards(gameObject.transform.position, aimPos, speed*Time.deltaTime);
    }
}
