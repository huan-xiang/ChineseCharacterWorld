using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 机关
/// </summary>
public class Office : MonoBehaviour
{
    /// <summary>
    /// 开启时目标坐标
    /// </summary>
    public Vector2 openAimPos;
    /// <summary>
    /// 关闭时目标坐标
    /// </summary>
    public Vector2 closeAimPos;
    /// <summary>
    /// 速度
    /// </summary>
    public float speed;
    /// <summary>
    /// 机关是否打开
    /// </summary>
    public bool open;
    void Update()
    {
        if (open)
        {
            //GetComponent<Rigidbody2D>().velocity = new Vector2(-3, 0);
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, openAimPos, speed * Time.deltaTime);
            
        }
        else
        {
            //GetComponent<Rigidbody2D>().velocity = new Vector2(3, 0);
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, closeAimPos, speed * Time.deltaTime);
        }
    }
}
