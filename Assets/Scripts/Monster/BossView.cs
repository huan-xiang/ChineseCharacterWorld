using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossView : MonoBehaviour
{
     /// <summary>
    /// 怪物信息
    /// </summary>
    public BossObject bossObject;
    /// <summary>
    /// 怪物目前位移
    /// </summary>
    public Transform ownTranform;
    /// <summary>
    /// 怪物所锁定的敌人
    /// </summary>
    public Transform enemy;
    public List<GameObject> wallList;
    public BossUnitControl bossUnitControl;
    private void Start()
    {
        bossObject = GetComponentInParent<BossObject>();
        ownTranform = bossObject.gameObject.GetComponent<Transform>();
        bossUnitControl = GetComponentInParent<BossUnitControl>();
    }

    private void Update()
    {
        ////目标丢失函数
        //if(bossObject.enemy!=GameObject.Find("InitEnemy"))
        //{
        //    if(bossObject.enemy!=null)
        //    {
        //        //当目标超过怪物可追踪到最大距离时目标丢失，进入徘徊状态
        //        if (Vector3.Distance(ownTranform.position, bossObject.enemy.transform.position) > bossObject.maxAttackDistance)
        //        {
        //            bossObject.enemy = null;
        //            //切换为徘徊状态
        //            //monsterObj.IsSeeEnemy = false;
        //        }
        //    }
        //}
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            bossObject.enemy = collision.gameObject;
            GetComponentInParent<NPC_Talk>().StartTalk();
            GetComponentInParent<NPC_Talk>().StartTalk();
            //bossUnitControl.JumpBack = true;
        }
        //if(collision.gameObject.tag=="Wall")
        //{
        //    GameObject tempwall = null;
        //    float minDis = 9999f;
        //    if(!wallList.Contains(collision.gameObject))
        //    {
        //        wallList.Add(collision.gameObject);
        //    }
        //    foreach(var go in wallList)
        //    {
        //        if(Mathf.Abs(bossObject.gameObject.transform.position.x-go.transform.position.x)<minDis)
        //        {
        //            tempwall = go;
        //            minDis = Mathf.Abs(bossObject.gameObject.transform.position.x - go.transform.position.x);
        //        }
        //    }
            

        //    bossObject.sawWall = tempwall.gameObject;
        //}

    }
}
