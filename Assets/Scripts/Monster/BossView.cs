using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossView : MonoBehaviour
{
     /// <summary>
    /// ������Ϣ
    /// </summary>
    public BossObject bossObject;
    /// <summary>
    /// ����Ŀǰλ��
    /// </summary>
    public Transform ownTranform;
    /// <summary>
    /// �����������ĵ���
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
        ////Ŀ�궪ʧ����
        //if(bossObject.enemy!=GameObject.Find("InitEnemy"))
        //{
        //    if(bossObject.enemy!=null)
        //    {
        //        //��Ŀ�곬�������׷�ٵ�������ʱĿ�궪ʧ�������ǻ�״̬
        //        if (Vector3.Distance(ownTranform.position, bossObject.enemy.transform.position) > bossObject.maxAttackDistance)
        //        {
        //            bossObject.enemy = null;
        //            //�л�Ϊ�ǻ�״̬
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
