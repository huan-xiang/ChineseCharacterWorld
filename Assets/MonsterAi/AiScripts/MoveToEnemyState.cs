using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anthill.AI;
using Anthill.Utils;
using Cainos.Monster;

public class MoveToEnemyState : AntAIState
{
    public MonsterObj monsterObj;
    /// <summary>
    /// 怪物行为控制器
    /// </summary>
    public MonsterController controller;
    public Transform nowPos;
    private Vector2 inputMove;
    public Transform targetTrs;
    /// <summary>
    /// 怪物ai控制器
    /// </summary>
    public MonsterUnitControl control;
    public float inputX;
    public override void Create(GameObject aGameObject)
    {
        monsterObj = aGameObject.GetComponent<MonsterObj>();
        nowPos = aGameObject.GetComponent<Transform>();
        controller = aGameObject.GetComponent<MonsterController>();
        control = aGameObject.GetComponent<MonsterUnitControl>();
    }

    public override void Enter()
    {
        inputX = monsterObj.inputX;
        controller.inputAttack = false;
        if(monsterObj.enemy!=null)
        {
            targetTrs = monsterObj.enemy.GetComponent<Transform>();
        }
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        inputX = monsterObj.inputX;
        var pos = nowPos.position;
        if (targetTrs.position.x - nowPos.position.x < 0)
        {
            inputMove.x = -inputX;
            monsterObj.nowDir = inputMove.x;
        }
        if (targetTrs.position.x - nowPos.position.x >= 0)
        {
            inputMove.x = inputX;
            monsterObj.nowDir = inputMove.x;
        }
        controller.inputMove = inputMove;
        controller.inputMoveModifier = true;
        if (AntMath.Distance(pos, targetTrs.position) <= 0.5f)
        {
            Debug.Log("Finish");
            Finish();
        }
    }
}
