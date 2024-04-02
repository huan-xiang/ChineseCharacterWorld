using Cainos.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerObject : MonoBehaviour
{
    /// <summary>
    /// 游戏管理器
    /// </summary>
    public GameManagement gameManagement;
    /// <summary>
    /// 血量
    /// </summary>
    public float hp;
    /// <summary>
    /// 最大血量
    /// </summary>
    public float maxHp;
    /// <summary>
    /// 血量UI
    /// </summary>
    public Slider hpUI;
    /// <summary>
    /// 施法蓄力条
    /// </summary>
    public Slider spellTimeSlider;
    /// <summary>
    /// 攻击力
    /// </summary>
    public float atk;
    /// <summary>
    /// 是否重击
    /// </summary>
    public bool deepAttack;
    /// <summary>
    /// 武器
    /// </summary>
    public WeaponObj weaponObj;
    /// <summary>
    /// 身体物体
    /// </summary>
    public GameObject body;
    public PixelCharacter pixelCharacter;
    /// <summary>
    /// 索敌
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
    /// 更新角色UI
    /// </summary>
    public void UpdateUI()
    {
        hpUI.value = hp / maxHp;
    }
    /// <summary>
    /// 受到攻击
    /// </summary>
    public void BeAttacked(GameObject attacker)
    {
        isAttacked = true;
        /*从左边被打，并且朝左*/
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
    /// 受到伤害
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
    /// 是否被攻击
    /// </summary>
    public bool isAttacked;
    /// <summary>
    /// 受伤无敌闪烁次数
    /// </summary>
    public int twinkleTimes;
    public int twinkleNow;
    /// <summary>
    /// 受伤闪烁
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
