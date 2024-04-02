using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObj : MonoBehaviour
{
    /// <summary>
    /// ʹ����
    /// </summary>
    public PlayerObject userObj;
    /// <summary>
    /// ����������
    /// </summary>
    public float weapon_atk;
    /// <summary>
    /// ʩ��������
    /// </summary>
    public GameObject spellPos;
    /// <summary>
    /// ������������򣬰��������ϲ���
    /// </summary>
    public bool followFacing;
    public AudioSource myAudioSource;
    public GameManagement gameManagement;

    private void Update()
    {
        ChangeFacing();
    }
    /// <summary>
    /// �ı䳯��
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
                /*��ɫ����*/
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
