using Anthill.AI;
using Cainos.Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AntAIState
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
