using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldObj : MonoBehaviour
{
    /// <summary>
    /// ��ֵ���
    /// </summary>
    public int goldValue;
    /// <summary>
    /// ��ֵ����
    /// </summary>
    public int expValue;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerObject playerObject = collision.GetComponent<PlayerObject>();
            //playerObject.GetExp(expValue);
            Destroy(gameObject);
        }
    }
}
