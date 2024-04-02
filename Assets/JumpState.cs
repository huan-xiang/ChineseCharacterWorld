using Anthill.AI;
using Cainos.Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AntAIState
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
    private Vector2 inputMove;
    private bool isJumped = false;
    public float timer = 0f;

    public override void Create(GameObject aGameObject)
    {
        control = aGameObject.GetComponent<MonsterUnitControl>();
        controller = aGameObject.GetComponent<MonsterController>();
        monsterObj = aGameObject.GetComponent<MonsterObj>();
    }

    public override void Enter()
    {
        timer = 0f;
        inputMove.x = monsterObj.nowDir;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        inputMove.x = monsterObj.nowDir;
        controller.inputMove = inputMove;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            controller.inputJump = true;
            timer = 3f;
        }
    }
}
