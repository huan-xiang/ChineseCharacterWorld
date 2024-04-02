using Cainos.Character;
using Cainos.Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterObj : MonoBehaviour
{
    /// <summary>
    /// ��Ϸ������
    /// </summary>
    public GameManagement gameManagement;
    /// <summary>
    /// ���ﱾ��
    /// </summary>
    public PixelMonster pixelMonster;
    /// <summary>
    /// ���ﶯ��������
    /// </summary>
    private MonsterController monsterController;
    /// <summary>
    /// Ŀ�����
    /// </summary>
    public GameObject enemy = null;
    /// <summary>
    /// ���ڵ�λ��
    /// </summary>
    public Vector3 nowPosition;
    /// <summary>
    /// ����ai������
    /// </summary>
    private MonsterUnitControl control;
    /// <summary>
    /// �����˶���߽�
    /// </summary>
    public Vector3 leftBorder;
    /// <summary>
    /// �����˶��ұ߽�
    /// </summary>
    public Vector3 rightBorder;
    /// <summary>
    /// ���׷������
    /// </summary>
    public float maxAttackDistance = 5.0f;
    /// <summary>
    ///���Թ����ľ���
    /// </summary>
    public float readyAttackDistance;
    /// <summary>
    /// �Ƿ��ڱ�����״̬
    /// </summary>
    private bool isFreezed=false;
    /// <summary>
    /// �����ʼ����ٶ�
    /// </summary>
    private float monsterMaxSpeed;
    /// <summary>
    /// �Ƿ������״̬
    /// </summary>
    public  bool isNumb = false;
    private Vector3 lastInputMove;
    /// <summary>
    /// λ�Ʊ�����Ϊ0ʱ�޷��ƶ�
    /// </summary>
    public float inputX = 1f;
    /// <summary>
    /// ��������
    /// </summary>
    public string monsterName;
    /// <summary>
    /// Ѫ��
    /// </summary>
    public float hp;
    /// <summary>
    /// ���Ѫ��
    /// </summary>
    public float maxHp;
    /// <summary>
    /// ħ��
    /// </summary>
    public float mp;
    /// <summary>
    /// ���ħ��
    /// </summary>
    public float maxMp;
    /// <summary>
    /// ������
    /// </summary>
    public float atk;
    /// <summary>
    /// ���Եõ���exp
    /// </summary>
    public float canGetExp;
    /// <summary>
    /// ������ʱ�ᱻ�ӳ��Ĺ������
    /// </summary>
    public int interruptValue;
    /// <summary>
    /// �������
    /// </summary>
    public float attackInterval = 1.5f;
    /// <summary>
    /// �ж��Ƿ񿴵�����
    /// </summary>
    private bool isSeeEnemy;
    /// <summary>
    /// ������ǽ
    /// </summary>
    public GameObject sawWall;
    /// <summary>
    /// ���������Ծ�ĸ߶ȣ�������������ǽ������
    /// </summary>
    public float jumpHeight;
    /// <summary>
    /// �������ڵĳ���
    /// </summary>
    public float nowDir;
    /// <summary>
    /// ��ǽ��Զ����
    /// </summary>
    public float readyJumpDistance = 2.5f;
    /// <summary>
    /// AttackRange����
    /// </summary>
    private GameObject attackRange;
    /// <summary>
    /// ���ֿ������е���
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
    /// ��������
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
        //�õ�AttackRange����
        attackRange = transform.GetChild(2).gameObject;
        allCharacterObj = Resources.LoadAll<GameObject>("ChineseCharacter/"); 
        //������ɺ���
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
    /// �Ƿ񱻶���
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
    /// �Ƿ����
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
    /// ����
    /// </summary>
    public void FreezeMonster()
    {
        isFreezed = !isFreezed;
    }
    /// <summary>
    /// ���
    /// </summary>
    public void NumbMonster(bool value)
    {
        isNumb = value;
    }
    /// <summary>
    /// �����������
    /// </summary>
    public void KillOrReviveMonster()
    {
        var mc = monsterController;
        if (mc) mc.IsDead = !mc.IsDead;
    }
    /// <summary>
    /// �յ��˺�
    /// </summary>
    public void BeAttacked(GameObject attacker)
    {
        isAttacked = true;
        /*����߱��򣬲��ҳ���*/
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
    /// ��������
    /// </summary>
    public void Die()
    {
        monsterController.IsDead = true;
        //Destroy(gameObject);
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
    /// ���һ�θ���������˺�������
    /// </summary>
    public GameObject lastAttackObject;
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    /// <summary>
    /// �������¼�
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
            /*����ֱ�Ӹ���*/
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
