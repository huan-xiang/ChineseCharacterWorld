using Anthill.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterView : MonoBehaviour
{
    /// <summary>
    /// ������Ϣ
    /// </summary>
    public MonsterObj monsterObj;
    /// <summary>
    /// ����ai������
    /// </summary>
    public MonsterUnitControl monsterUnitControl;
    /// <summary>
    /// ����Ŀǰλ��
    /// </summary>
    public Transform ownTranform;
    /// <summary>
    /// �����������ĵ���
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
        //Ŀ�궪ʧ����
        if(monsterObj.enemy!=GameObject.Find("InitEnemy"))
        {
            if(monsterObj.enemy!=null)
            {
                //��Ŀ�곬�������׷�ٵ�������ʱĿ�궪ʧ�������ǻ�״̬
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
