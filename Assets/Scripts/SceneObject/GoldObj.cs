using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldObj : MonoBehaviour
{
    /// <summary>
    /// 价值金币
    /// </summary>
    public int goldValue;
    /// <summary>
    /// 价值经验
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
