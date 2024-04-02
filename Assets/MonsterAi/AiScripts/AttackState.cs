using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anthill.AI;
using Anthill.Utils;
using Cainos.Monster;

public class AttackState : AntAIState
{
    /// <summary>
    /// 怪物ai控制器
    /// </summary>
    public MonsterUnitControl control;
    /// <summary>
    /// 怪物行为控制器
    /// </summary>
    public MonsterController controller;
    /// <summary>
    /// 怪物属性脚本
    /// </summary>
    public MonsterObj monsterObj;
    public float timer;

    public override void Create(GameObject aGameObject)
    {
        control = aGameObject.GetComponent<MonsterUnitControl>();
        controller = aGameObject.GetComponent<MonsterController>();
        monsterObj = aGameObject.GetComponent<MonsterObj>();
        timer = 0.1f;
    }

    public override void Enter()
    {
        /*不要一碰到就打*/
        //controller.inputAttack = true;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        timer -= Time.deltaTime;
        if (monsterObj.isAttacked)
        {
            monsterObj.isAttacked = false;
            if(timer + monsterObj.interruptValue >= monsterObj.attackInterval)
            {
                timer = monsterObj.attackInterval;
            }
            else
            {
                timer += monsterObj.interruptValue;
            }
        }
        if(timer<=0)
        {
            Debug.Log("攻击");
            controller.inputAttack = true;
            timer = monsterObj.attackInterval;
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(5f);
        controller.inputAttack = true;
    }
}
