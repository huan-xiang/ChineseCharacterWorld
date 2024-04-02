using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anthill.AI;
using Anthill.Utils;
using Cainos.Monster;

public class AttackState : AntAIState
{
    /// <summary>
    /// ����ai������
    /// </summary>
    public MonsterUnitControl control;
    /// <summary>
    /// ������Ϊ������
    /// </summary>
    public MonsterController controller;
    /// <summary>
    /// �������Խű�
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
        /*��Ҫһ�����ʹ�*/
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
            Debug.Log("����");
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
