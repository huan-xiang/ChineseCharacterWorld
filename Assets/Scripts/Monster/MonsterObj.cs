using Cainos.Character;
using Cainos.Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterObj : MonoBehaviour
{
    /// <summary>
    /// 游戏管理器
    /// </summary>
    public GameManagement gameManagement;
    /// <summary>
    /// 怪物本体
    /// </summary>
    public PixelMonster pixelMonster;
    /// <summary>
    /// 怪物动作控制器
    /// </summary>
    private MonsterController monsterController;
    /// <summary>
    /// 目标敌人
    /// </summary>
    public GameObject enemy = null;
    /// <summary>
    /// 现在的位置
    /// </summary>
    public Vector3 nowPosition;
    /// <summary>
    /// 怪物ai控制器
    /// </summary>
    private MonsterUnitControl control;
    /// <summary>
    /// 怪物运动左边界
    /// </summary>
    public Vector3 leftBorder;
    /// <summary>
    /// 怪物运动右边界
    /// </summary>
    public Vector3 rightBorder;
    /// <summary>
    /// 最大追击距离
    /// </summary>
    public float maxAttackDistance = 5.0f;
    /// <summary>
    ///可以攻击的距离
    /// </summary>
    public float readyAttackDistance;
    /// <summary>
    /// 是否处于被冻结状态
    /// </summary>
    private bool isFreezed=false;
    /// <summary>
    /// 怪物初始最大速度
    /// </summary>
    private float monsterMaxSpeed;
    /// <summary>
    /// 是否处于麻痹状态
    /// </summary>
    public  bool isNumb = false;
    private Vector3 lastInputMove;
    /// <summary>
    /// 位移倍数，为0时无法移动
    /// </summary>
    public float inputX = 1f;
    /// <summary>
    /// 怪物名称
    /// </summary>
    public string monsterName;
    /// <summary>
    /// 血量
    /// </summary>
    public float hp;
    /// <summary>
    /// 最大血量
    /// </summary>
    public float maxHp;
    /// <summary>
    /// 魔力
    /// </summary>
    public float mp;
    /// <summary>
    /// 最大魔力
    /// </summary>
    public float maxMp;
    /// <summary>
    /// 攻击力
    /// </summary>
    public float atk;
    /// <summary>
    /// 可以得到的exp
    /// </summary>
    public float canGetExp;
    /// <summary>
    /// 被攻击时会被延长的攻击间隔
    /// </summary>
    public int interruptValue;
    /// <summary>
    /// 攻击间隔
    /// </summary>
    public float attackInterval = 1.5f;
    /// <summary>
    /// 判断是否看到敌人
    /// </summary>
    private bool isSeeEnemy;
    /// <summary>
    /// 看到的墙
    /// </summary>
    public GameObject sawWall;
    /// <summary>
    /// 怪物可以跳跃的高度，决定怪物遇到墙跳不跳
    /// </summary>
    public float jumpHeight;
    /// <summary>
    /// 怪物现在的朝向
    /// </summary>
    public float nowDir;
    /// <summary>
    /// 离墙多远起跳
    /// </summary>
    public float readyJumpDistance = 2.5f;
    /// <summary>
    /// AttackRange物体
    /// </summary>
    private GameObject attackRange;
    /// <summary>
    /// 汉字库中所有的字
    /// </summary>
    private GameObject[] allCharacterObj;
    public int carryingCharactersNum = 3;
    public bool IsSeeEnemy
    {
        get { return isSeeEnemy; }
        set
        {
            isSeeEnemy = value;
        }
    }
    /// <summary>
    /// 怪物武器
    /// </summary>
    public MonsterWeaponObj monsterWeaponObj;
    void Start()
    {
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        pixelMonster = GetComponent<PixelMonster>();
        monsterController = GetComponent<MonsterController>();
        control = GetComponent<MonsterUnitControl>();
        monsterMaxSpeed = monsterController.walkSpeedMax;
        enemy = GameObject.Find("InitEnemy");
        monsterWeaponObj = GetComponentInChildren<MonsterWeaponObj>();
        //得到AttackRange物体
        attackRange = transform.GetChild(2).gameObject;
        allCharacterObj = Resources.LoadAll<GameObject>("ChineseCharacter/"); 
        //随机生成汉字
        for (int i=0;i< carryingCharactersNum; i++)
        {
            int r = Random.Range(0, allCharacterObj.Length);
            var go = Instantiate(allCharacterObj[r]);
            go.transform.SetParent(attackRange.transform);
        }
        //leftBorder = new Vector3(-2.0f, 0, 0);
        //rightBorder = new Vector3(2.0f, 0, 0);
    }

    void FixedUpdate()
    {
        nowPosition = GetComponent<Transform>().position;
        InjuredTwinkle();
        isMonsterFreezeed();
        isMonsterNumb();
    }
    /// <summary>
    /// 是否被冻结
    /// </summary>
    private void isMonsterFreezeed()
    {
        if (isFreezed)
        {
            gameObject.GetComponentInChildren<Animator>().enabled = false;
            monsterController.walkSpeedMax = 0f;
        }
        else
        {
            gameObject.GetComponentInChildren<Animator>().enabled = true;
            monsterController.walkSpeedMax = monsterMaxSpeed;
        }
    }
    /// <summary>
    /// 是否麻痹
    /// </summary>
    private void isMonsterNumb()
    {
        if(isNumb==false)
        {
            lastInputMove = monsterController.inputMove;
        }
        if (isNumb)
        {
            inputX = 0;
            //monsterController.inputMove = new Vector3(0, 0, 0);
            //monsterController.walkSpeedMax = 0f;
        }
        else
        {
            inputX = 1f;
            //monsterController.inputMove = lastInputMove;
        }
    }
    /// <summary>
    /// 冻结
    /// </summary>
    public void FreezeMonster()
    {
        isFreezed = !isFreezed;
    }
    /// <summary>
    /// 麻痹
    /// </summary>
    public void NumbMonster(bool value)
    {
        isNumb = value;
    }
    /// <summary>
    /// 复活或者重生
    /// </summary>
    public void KillOrReviveMonster()
    {
        var mc = monsterController;
        if (mc) mc.IsDead = !mc.IsDead;
    }
    /// <summary>
    /// 收到伤害
    /// </summary>
    public void BeAttacked(GameObject attacker)
    {
        isAttacked = true;
        /*从左边被打，并且朝左*/
        if (attacker == null
            || (attacker.transform.position.x <= transform.position.x && pixelMonster.Facing <= 0)
            || (attacker.transform.position.x > transform.position.x && pixelMonster.Facing > 0))
        {
            pixelMonster.InjuredFront();
        }
        else
        {
            pixelMonster.InjuredBack();
            enemy = attacker;
        }
    }
    public void GetDamage(float damage)
    {
        if (hp - damage > 0)
        {
            hp -= damage;
        }
        else
        {
            hp = 0;
            Die();
        }
    }
    /// <summary>
    /// 怪物死亡
    /// </summary>
    public void Die()
    {
        monsterController.IsDead = true;
        //Destroy(gameObject);
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
        if(twinkleNow % 3 != 0)
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
    /// <summary>
    /// 最后一次给怪物造成伤害的物体
    /// </summary>
    public GameObject lastAttackObject;
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    /// <summary>
    /// 死亡后事件
    /// </summary>
    private void OnDestroy()
    {
   
        if (monsterController.IsDead)
        {
            Debug.Log("Ondestroy");
            GameObject gold = Instantiate(gameManagement.goldPrefab);
            gold.transform.SetParent(gameManagement.sceneObjectRoot.transform);
            gold.transform.position = gameObject.transform.position + new Vector3(0,0.5f,0);
            gold.GetComponent<MoveToGameObject>().aimObj = lastAttackObject;
            CharacterStates active = gameManagement.characterStates[0];
            /*死后直接附加*/
            for (int i = 0; i < carryingCharactersNum; i++)
            {
                ChineseCharacter newCharacter = gameManagement.chineseCharacterManagement.CreateChineseCharacter(attackRange.transform.GetChild(i).GetComponent<ChineseCharacter>().characterName);
                active.chineseCharacters.Add(newCharacter);
            }
            gameManagement.monsterManagement.allMonsterList.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
