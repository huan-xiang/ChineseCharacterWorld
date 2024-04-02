using Anthill.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterView : MonoBehaviour
{
    /// <summary>
    /// 怪物信息
    /// </summary>
    public MonsterObj monsterObj;
    /// <summary>
    /// 怪物ai控制器
    /// </summary>
    public MonsterUnitControl monsterUnitControl;
    /// <summary>
    /// 怪物目前位移
    /// </summary>
    public Transform ownTranform;
    /// <summary>
    /// 怪物所锁定的敌人
    /// </summary>
    public Transform enemy;
    public List<GameObject> wallList;
    private void Start()
    {
        monsterObj = GetComponentInParent<MonsterObj>();
        monsterUnitControl = GetComponentInParent<MonsterUnitControl>();
        ownTranform = monsterObj.gameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        //目标丢失函数
        if(monsterObj.enemy!=GameObject.Find("InitEnemy"))
        {
            if(monsterObj.enemy!=null)
            {
                //当目标超过怪物可追踪到最大距离时目标丢失，进入徘徊状态
                if (AntMath.Distance(ownTranform.position, monsterObj.enemy.transform.position) > monsterObj.maxAttackDistance)
                {
                    monsterObj.enemy = null;
                    monsterObj.IsSeeEnemy = false;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            monsterObj.enemy = collision.gameObject;
            monsterObj.IsSeeEnemy = true;
        }
        if(collision.gameObject.tag=="Wall")
        {
            GameObject tempwall = null;
            float minDis = 9999f;
            if(!wallList.Contains(collision.gameObject))
            {
                wallList.Add(collision.gameObject);
            }
            foreach(var go in wallList)
            {
                if(Mathf.Abs(monsterObj.gameObject.transform.position.x-go.transform.position.x)<minDis)
                {
                    tempwall = go;
                    minDis = Mathf.Abs(monsterObj.gameObject.transform.position.x - go.transform.position.x);
                }
            }
            

            monsterObj.sawWall = tempwall.gameObject;
        }
    }
}
