using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObj : MonoBehaviour
{
    public GameManagement gameManagement;
    public GameObject user;
    public Vector2 direction;
    public float speed;
    public float damage;
    /// <summary>
    /// 生命时间
    /// </summary>
    public int lifeTime;
    private int time;
    /// <summary>
    /// 要附加的汉字列表
    /// </summary>
    public List<string> chineseCharacters;
    public AudioSource myAudioSource;
    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
    }
    private void FixedUpdate()
    {
        if(time < lifeTime)
        {
            time++;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// 推动
    /// </summary>
    /// <param name="direction"></param>
    public void Push(Vector2 direction, float damage)
    {
        this.damage = damage;
        float angle = Vector2.Angle(Vector2.right, direction);
        if (direction.y> 0)
        {
            transform.Rotate(new Vector3(0, 0, angle));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, -angle));

        }
        GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
    }
    public bool canBeDestory;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Map" && canBeDestory)
        {
            Destroy(gameObject);
        }
        /*玩家打怪物*/
        else if(collision.gameObject.tag == "Monster")
        {
            if (user.tag == "Player")
            {
                collision.GetComponent<MonsterObj>().lastAttackObject = user;
                collision.GetComponent<MonsterObj>().BeAttacked(user);
                collision.GetComponent<MonsterObj>().GetDamage(damage);
                foreach(string characterName in chineseCharacters)
                {
                    ChineseCharacter newCharacter = gameManagement.chineseCharacterManagement.CreateChineseCharacter(characterName);
                    newCharacter.invalidTime = 120;
                    collision.GetComponent<BuffStates>().passiveChineseCharacterList.Add(newCharacter);
                }
                if(canBeDestory)Destroy(gameObject);
            }
        }
        /*玩家打boss*/
        else if (collision.gameObject.tag == "Boss")
        {
            if (user.tag == "Player")
            {
                collision.GetComponent<BossObject>().BeAttacked(user);
                collision.GetComponent<BossObject>().GetDamage(damage);
                foreach (string characterName in chineseCharacters)
                {
                    ChineseCharacter newCharacter = gameManagement.chineseCharacterManagement.CreateChineseCharacter(characterName);
                    newCharacter.invalidTime = 120;
                    collision.GetComponent<BuffStates>().passiveChineseCharacterList.Add(newCharacter);
                }
                if (canBeDestory) Destroy(gameObject);
            }
        }
        /*Boss打玩家*/
        else if (collision.gameObject.tag == "Player")
        {
            if (user.tag =="Boss")
            {
                collision.GetComponent<PlayerObject>().BeAttacked(user);
                collision.GetComponent<PlayerObject>().GetDamage(damage);
                foreach (string characterName in chineseCharacters)
                {
                    ChineseCharacter newCharacter = gameManagement.chineseCharacterManagement.CreateChineseCharacter(characterName);
                    newCharacter.invalidTime = 120;
                    collision.GetComponent<BuffStates>().passiveChineseCharacterList.Add(newCharacter);
                }
                if (canBeDestory) Destroy(gameObject);
            }
        }
    }
}
