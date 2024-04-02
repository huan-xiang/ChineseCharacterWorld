using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anthill.AI;
using Anthill.Utils;
public class MonsterUnitScene : MonoBehaviour,ISense
{
    public Transform ownTranform;
    public Transform enemy;
    public MonsterUnitControl control;
    public MonsterObj monsterObj;
    public GameObject sawWall;
    public float readyJumpDis;
    public void Awake()
    {
        control = GetComponent<MonsterUnitControl>();
        ownTranform = GetComponent<Transform>();
        enemy = GameObject.Find("InitEnemy").GetComponent<Transform>();
        monsterObj = GetComponent<MonsterObj>();
        
    }
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        readyJumpDis = monsterObj.readyJumpDistance;
        if (monsterObj.enemy!=null)
        {
            enemy = monsterObj.enemy.GetComponent<Transform>();
        }
        if(monsterObj.sawWall!=null)
        {
            sawWall = monsterObj.sawWall;
        }
        aWorldState.BeginUpdate(aAgent.planner);
        {
            aWorldState.Set(MonsterScenario.IsLifeFinished, false);
            aWorldState.Set(MonsterScenario.SeeEnemy, monsterObj.IsSeeEnemy);
            aWorldState.Set(MonsterScenario.ReadyToAttack, IsReadyToAttack());
            aWorldState.Set(MonsterScenario.IsLive, true);
            if(sawWall!=null)
            {
                aWorldState.Set(MonsterScenario.ReadyToJump, IsReadyToJump());
            }
            else
            {
                aWorldState.Set(MonsterScenario.ReadyToJump, false);
            }
        }
        aWorldState.EndUpdate();
    }

    public bool IsReadyToAttack()
    {
        return (AntMath.Distance(ownTranform.position, enemy.position) < monsterObj.readyAttackDistance);
    }

    public bool IsReadyToJump()
    {
        if (AntMath.Distance(ownTranform.position, sawWall.transform.position) < readyJumpDis)
        {
            Debug.Log(sawWall.GetComponent<BoxCollider2D>().bounds.extents.y);
            if ((sawWall.GetComponent<BoxCollider2D>().bounds.extents.y - ownTranform.position.y) < monsterObj.jumpHeight&&(sawWall.GetComponent<BoxCollider2D>().bounds.extents.y - ownTranform.position.y)>0)
            {
                return true;
            }
        }
        return false;
    }
}
