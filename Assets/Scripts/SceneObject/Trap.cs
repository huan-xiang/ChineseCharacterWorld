using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    /// <summary>
    /// 陷阱能造成的伤害
    /// </summary>
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerObject playerObject = collision.GetComponent<PlayerObject>();
            playerObject.BeAttacked(null);
            playerObject.GetDamage(damage);
        }
        else if(collision.tag == "Monster")
        {
            MonsterObj monsterObj = collision.GetComponent<MonsterObj>();
            monsterObj.BeAttacked(null);
            monsterObj.GetDamage(damage);
        }
    }
}
