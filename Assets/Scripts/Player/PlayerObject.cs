using Cainos.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerObject : MonoBehaviour
{
    /// <summary>
    /// ��Ϸ������
    /// </summary>
    public GameManagement gameManagement;
    /// <summary>
    /// Ѫ��
    /// </summary>
    public float hp;
    /// <summary>
    /// ���Ѫ��
    /// </summary>
    public float maxHp;
    /// <summary>
    /// Ѫ��UI
    /// </summary>
    public Slider hpUI;
    /// <summary>
    /// ʩ��������
    /// </summary>
    public Slider spellTimeSlider;
    /// <summary>
    /// ������
    /// </summary>
    public float atk;
    /// <summary>
    /// �Ƿ��ػ�
    /// </summary>
    public bool deepAttack;
    /// <summary>
    /// ����
    /// </summary>
    public WeaponObj weaponObj;
    /// <summary>
    /// ��������
    /// </summary>
    public GameObject body;
    public PixelCharacter pixelCharacter;
    /// <summary>
    /// ����
    /// </summary>
    public GameObject player_enemy;
    public AudioSource myAudioSource;
    public GameObject conversationFrame;
    public GameObject deadPanel;
    public GameObject nowRelifePos;
    private void Start()
    {
        pixelCharacter = GetComponent<PixelCharacter>();
        myAudioSource = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        if(gameObject.tag!="Player")
        {
            return;
        }
        UpdateUI();
        InjuredTwinkle();
    }
    /// <summary>
    /// ���½�ɫUI
    /// </summary>
    public void UpdateUI()
    {
        hpUI.value = hp / maxHp;
    }
    /// <summary>
    /// �ܵ�����
    /// </summary>
    public void BeAttacked(GameObject attacker)
    {
        isAttacked = true;
        /*����߱��򣬲��ҳ���*/
        if (attacker == null
            ||(attacker.transform.position.x <= transform.position.x && pixelCharacter.Facing <= 0)
            || (attacker.transform.position.x > transform.position.x && pixelCharacter.Facing > 0))
        {
            pixelCharacter.InjuredFront();
        }
        else
        {
            pixelCharacter.InjuredBack();
        }
    }
    /// <summary>
    /// �ܵ��˺�
    /// </summary>
    public void GetDamage(float damage)
    {
        if (GetComponent<BossObject>())
        {
            myAudioSource.clip = gameManagement.audioManager.beAttackedAudio;
            myAudioSource.Play();
        }
        hp -= damage;
        if(hp <= 0)
        {
            pixelCharacter.IsDead = true;
            if(this.GetComponent<BossObject>())
            {
                if (this.GetComponent<BossUnitControl>())
                {
                    this.GetComponent<BossUnitControl>().bossOver();
                }
                else if (this.GetComponent<Boss2UnitControl>())
                {
                    this.GetComponent<Boss2UnitControl>().bossOver();
                }
                else if (this.GetComponent<Boss3UnitControl>())
                {
                    this.GetComponent<Boss3UnitControl>().bossOver();
                }
                else if (this.GetComponent<Boss4UnitControl>())
                {
                    this.GetComponent<Boss4UnitControl>().BossOver();
                }
                this.GetComponent<BossObject>().myAirWall.SetActive(false);
            }
            if(this.transform.tag=="Player")
            {
                deadPanel.SetActive(true);
                for(int i=0;i<gameManagement.bossManager.bossLIist.Count;i++)
                {
                    if (gameManagement.bossManager.bossLIist[i].GetComponent<BossUnitControl>())
                    {
                        Debug.Log(gameManagement.bossManager.bossLIist[i].name);
                        gameManagement.bossManager.bossLIist[i].GetComponent<BossUnitControl>().bossOver();
                        gameManagement.bossManager.bossLIist[i].transform.position = gameManagement.bossManager.bossLIist[i].GetComponent<BossUnitControl>().resetPos;

                    }
                    else if (gameManagement.bossManager.bossLIist[i].GetComponent<Boss2UnitControl>())
                    {
                        gameManagement.bossManager.bossLIist[i].GetComponent<Boss2UnitControl>().bossOver();
                        gameManagement.bossManager.bossLIist[i].transform.position = gameManagement.bossManager.bossLIist[i].GetComponent<Boss2UnitControl>().resetPos;
                    }
                    else if (gameManagement.bossManager.bossLIist[i].GetComponent<Boss3UnitControl>())
                    {
                        gameManagement.bossManager.bossLIist[i].GetComponent<Boss3UnitControl>().bossOver();
                        gameManagement.bossManager.bossLIist[i].transform.position = gameManagement.bossManager.bossLIist[i].GetComponent<Boss3UnitControl>().resetPos;

                    }
                    else if (gameManagement.bossManager.bossLIist[i].GetComponent<Boss4UnitControl>())
                    {
                        gameManagement.bossManager.bossLIist[i].GetComponent<Boss4UnitControl>().BossOver();
                        gameManagement.bossManager.bossLIist[i].transform.position = gameManagement.bossManager.bossLIist[i].GetComponent<Boss4UnitControl>().resetPos;
                    }
                }
            }
        }
    }
    /// <summary>
    /// �Ƿ񱻹���
    /// </summary>
    public bool isAttacked;
    /// <summary>
    /// �����޵���˸����
    /// </summary>
    public int twinkleTimes;
    public int twinkleNow;
    /// <summary>
    /// ������˸
    /// </summary>
    public void InjuredTwinkle()
    {
        if (!isAttacked)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            twinkleNow = 0;
            return;
        }
        twinkleNow++;
        if (twinkleNow % 3 != 0)
        {
            return;
        }
        if (transform.position.z == 2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            if (twinkleNow >= twinkleTimes)
            {
                isAttacked = false;
                twinkleNow = 0;
                return;
            }
        }
        else if (transform.position.z == 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 2);
        }
    }
    public void Relife()
    {
        this.GetComponent<BuffStates>().buffList.Clear();
        this.GetComponent<BuffStates>().passiveChineseCharacterList.Clear();
        deadPanel.SetActive(false);
        pixelCharacter.IsDead = false;
        this.transform.position = nowRelifePos.transform.position;
        for (int i = 0; i < gameManagement.bossManager.bossLIist.Count; i++)
        {
            if(gameManagement.bossManager.bossLIist[i].GetComponent<PixelCharacter>().IsDead==false)
            {
                if (gameManagement.bossManager.bossLIist[i].GetComponent<BossUnitControl>())
                {
                    gameManagement.bossManager.bossLIist[i].GetComponent<BossUnitControl>().myView.SetActive(true);
                    gameManagement.bossManager.bossLIist[i].GetComponent<BossObject>().myAirWall.SetActive(false);

                    gameManagement.bossManager.bossLIist[i].GetComponent<BossObject>().hp = gameManagement.bossManager.bossLIist[i].GetComponent<BossObject>().maxHp;
                }
                else if (gameManagement.bossManager.bossLIist[i].GetComponent<Boss2UnitControl>())
                {
                    gameManagement.bossManager.bossLIist[i].GetComponent<Boss2UnitControl>().myView.SetActive(true);
                    gameManagement.bossManager.bossLIist[i].GetComponent<BossObject>().myAirWall.SetActive(false);
                    gameManagement.bossManager.bossLIist[i].GetComponent<BossObject>().hp = gameManagement.bossManager.bossLIist[i].GetComponent<BossObject>().maxHp;
                }
                else if (gameManagement.bossManager.bossLIist[i].GetComponent<Boss3UnitControl>())
                {
                    gameManagement.bossManager.bossLIist[i].GetComponent<Boss3UnitControl>().myView.SetActive(true);
                    gameManagement.bossManager.bossLIist[i].GetComponent<BossObject>().myAirWall.SetActive(false);
                   gameManagement.bossManager.bossLIist[i].GetComponent<BossObject>().hp = gameManagement.bossManager.bossLIist[i].GetComponent<BossObject>().maxHp;


                }
                else if (gameManagement.bossManager.bossLIist[i].GetComponent<Boss4UnitControl>())
                {
                    gameManagement.bossManager.bossLIist[i].GetComponent<Boss4UnitControl>().myView.SetActive(true);
                    gameManagement.bossManager.bossLIist[i].GetComponent<BossObject>().myAirWall.SetActive(false);
                    gameManagement.bossManager.bossLIist[i].GetComponent<BossObject>().hp = gameManagement.bossManager.bossLIist[i].GetComponent<BossObject>().maxHp;

                }
            }
        }
        hp = maxHp;
    }
}
