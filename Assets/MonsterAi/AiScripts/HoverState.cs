using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anthill.AI;
using Anthill.Utils;
using Cainos.Monster;

public class HoverState : AntAIState
{
    public MonsterObj monsterObj;
    public Transform nowPos;
    public Vector3 targetPos;
    private Vector2 inputMove;
    public MonsterController controller;
    public GameObject view;
    public float inputX;
    /// <summary>
    /// 随机方向
    /// </summary>
    private bool dir;
    private bool isCrash = false;
    /// <summary>
    /// 多长时间检测碰撞一次
    /// </summary>
    private float updateCrashTime = 0.08f;
    /// <summary>
    /// 检测是否碰到墙的时间
    /// </summary>
    private float isCrashTime;
    private Vector3 lastPos;
    public override void Create(GameObject aGameObject)
    {
        isCrashTime = updateCrashTime;
        monsterObj = aGameObject.GetComponent<MonsterObj>();
        nowPos = aGameObject.GetComponent<Transform>();
        controller = aGameObject.GetComponent<MonsterController>();
        view = aGameObject.transform.GetChild(1).gameObject;
    }

    public override void Enter()
    {
        lastPos = nowPos.position;
        inputX = monsterObj.inputX;
        controller.inputAttack = false;
        if(isCrash==false)
        {
            //没撞墙就按当前方向继续前进直到走到边界
            if(monsterObj.nowDir==1)
            {
                dir = true;
            }
            else
            {
                dir = false;
            }
        }
        else
        {
            //切换方向随机一个位置
            dir = !dir;
            isCrash = false;
        }
        if(dir)
        {
            targetPos = new Vector3(nowPos.position.x + Random.Range(0, 10f), nowPos.position.y, nowPos.position.z);
        }
        else
        {
            targetPos = new Vector3(nowPos.position.x + Random.Range(-10f, 0), nowPos.position.y, nowPos.position.z);
        }
        if (targetPos.x < monsterObj.leftBorder.x)
        {
            targetPos = new Vector3(monsterObj.leftBorder.x, nowPos.position.y, nowPos.position.z);
        }
        if (targetPos.x > monsterObj.rightBorder.x)
        {
            targetPos = new Vector3(monsterObj.rightBorder.x, nowPos.position.y, nowPos.position.z);
        }
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        inputX = monsterObj.inputX;
        Vector3 pos = nowPos.position;
        if (targetPos.x-nowPos.position.x<0)
        {
            inputMove.x = -inputX;
            monsterObj.nowDir = inputMove.x;
            view.transform.localScale = new Vector3(-1, 1, 1);
        }
        if(targetPos.x-nowPos.position.x>=0)
        {
            inputMove.x = inputX;
            monsterObj.nowDir = inputMove.x;
            view.transform.localScale = new Vector3(1, 1, 1);
        }
        isCrashTime -= Time.deltaTime;
        if(isCrashTime<=0)
        {
            Vector3 tempPos = nowPos.position;
            if (tempPos.x - lastPos.x==0)
            {
                Debug.Log("碰墙了");
                isCrash = true;
                Finish();
            }
            lastPos = nowPos.position;
            isCrashTime = updateCrashTime;
        }
        controller.inputMove = inputMove;
        if (Mathf.Abs(nowPos.position.x-targetPos.x)<=0.11f)
        {
            Finish();
        }
    }
}
