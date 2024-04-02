using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObj : MonoBehaviour
{
    /// <summary>
    /// 使用者
    /// </summary>
    public PlayerObject userObj;
    /// <summary>
    /// 武器攻击力
    /// </summary>
    public float weapon_atk;
    /// <summary>
    /// 施法产生点
    /// </summary>
    public GameObject spellPos;
    /// <summary>
    /// 跟随玩家面向方向，绑定在武器上不用
    /// </summary>
    public bool followFacing;
    public AudioSource myAudioSource;
    public GameManagement gameManagement;

    private void Update()
    {
        ChangeFacing();
    }
    /// <summary>
    /// 改变朝向
    /// </summary>
    public void ChangeFacing()
    {
        if (userObj.deepAttack)
        {
            gameObject.SetActive(true);

        }
        if (followFacing)
        {
            if (userObj.pixelCharacter.Facing > 0)
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1);
                transform.localPosition = new Vector3(Mathf.Abs(transform.localPosition.x), transform.localPosition.y, transform.localPosition.z);
            }
            else
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1);
                transform.localPosition = new Vector3(-Mathf.Abs(transform.localPosition.x), transform.localPosition.y, transform.localPosition.z);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!userObj.GetComponent<BossObject>())
        {
            if (collision.gameObject.tag == "Monster")
            {
                MonsterObj monsterObj = collision.GetComponent<MonsterObj>();
                /*角色攻击*/
                if (!monsterObj.isAttacked)
                {
                    monsterObj.BeAttacked(userObj.gameObject);
                    monsterObj.GetDamage(userObj.atk + weapon_atk);
                    monsterObj.lastAttackObject = userObj.gameObject;
                }
            }
            else if (collision.gameObject.tag == "Boss")
            {
                BossObject bossObject = collision.GetComponent<BossObject>();
                if (!bossObject.isAttacked)
                {
                    bossObject.BeAttacked(userObj.gameObject);
                    bossObject.GetDamage(userObj.atk + weapon_atk);
                }
            }
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                PlayerObject playerObject = collision.GetComponent<PlayerObject>();
                if (!playerObject.isAttacked)
                {
                    playerObject.BeAttacked(userObj.gameObject);
                    playerObject.GetDamage(userObj.atk + weapon_atk);
                }
            }
        }
    }

    public void PlayDeepAttackAudio()
    {
        myAudioSource.clip = gameManagement.audioManager.deepAttackAudio;
        myAudioSource.Play();
    }
}
