using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����
/// </summary>
public class Office : MonoBehaviour
{
    /// <summary>
    /// ����ʱĿ������
    /// </summary>
    public Vector2 openAimPos;
    /// <summary>
    /// �ر�ʱĿ������
    /// </summary>
    public Vector2 closeAimPos;
    /// <summary>
    /// �ٶ�
    /// </summary>
    public float speed;
    /// <summary>
    /// �����Ƿ��
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
