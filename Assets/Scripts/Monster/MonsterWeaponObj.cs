using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeaponObj : MonoBehaviour
{
    /// <summary>
    /// ʹ����
    /// </summary>
    public GameObject userObj;
    /// <summary>
    /// ����������
    /// </summary>
    public float weapon_atk;
    void Update()
    {
        if (userObj.GetComponent<BossObject>())
        {
            if (userObj.GetComponent<BossObject>().pixelCharacter.Facing > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        if (userObj.GetComponent<MonsterObj>())
        {
            if (userObj.GetComponent<MonsterObj>().pixelMonster.Facing > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerObject playerObject = collision.GetComponent<PlayerObject>();
            if (playerObject.isAttacked) return;
            playerObject.BeAttacked(userObj);
            if (userObj.GetComponent<BossObject>())
            {
                playerObject.GetDamage(userObj.GetComponent<BossObject>().atk + weapon_atk);

            }
            if (userObj.GetComponent<MonsterObj>())
            {
                playerObject.GetDamage(userObj.GetComponent<MonsterObj>().atk + weapon_atk);
            }
        }

    }
}
