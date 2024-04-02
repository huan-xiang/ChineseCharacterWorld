using Anthill.AI;
using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc1HoverState : AntAIState
{
    public BossObject bossObject;
    public PixelCharacter character;
    public Transform nowPos;
    public Vector3 targetPos;
    private float inputMove;
    public PixelCharacterController controller;
    public float inputX;
    public GameObject conversation;
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
    private float conTimer = 3f;
    private Vector3 lastPos;
    public override void Create(GameObject aGameObject)
    {
        character = aGameObject.GetComponent<PixelCharacter>();
        isCrashTime = updateCrashTime;
        nowPos = aGameObject.GetComponent<Transform>();
        controller = aGameObject.GetComponent<PixelCharacterController>();
        bossObject = aGameObject.GetComponent<BossObject>();
        conversation = aGameObject.transform.GetChild(0).gameObject;
    }

    public override void Enter()
    {
        controller.isRun = false;
        lastPos = nowPos.position;
        controller.inputH = character.Facing;
        //inputX = monsterObj.inputX;
        if (isCrash == false)
        {
            //没撞墙就按当前方向继续前进直到走到边界
            if (character.Facing == 1)
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
        if (dir)
        {
            targetPos = new Vector3(nowPos.position.x + Random.Range(0, 10f), nowPos.position.y, nowPos.position.z);
        }
        else
        {
            targetPos = new Vector3(nowPos.position.x + Random.Range(-10f, 0), nowPos.position.y, nowPos.position.z);
        }
        if (targetPos.x < bossObject.leftBorder.x)
        {
            targetPos = new Vector3(bossObject.leftBorder.x, nowPos.position.y, nowPos.position.z);
        }
        if (targetPos.x > bossObject.rightBorder.x)
        {
            targetPos = new Vector3(bossObject.rightBorder.x, nowPos.position.y, nowPos.position.z);
        }
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        inputX = 1f;
        Vector3 pos = nowPos.position;
        if (targetPos.x - nowPos.position.x < 0)
        {
            inputMove = -inputX;
        }
        if (targetPos.x - nowPos.position.x >= 0)
        {
            inputMove = inputX;
        }
        isCrashTime -= Time.deltaTime;
        conTimer -= Time.deltaTime;
        if (isCrashTime <= 0)
        {
            Vector3 tempPos = nowPos.position;
            if (tempPos.x - lastPos.x == 0)
            {
                Debug.Log("碰墙了");
                isCrash = true;
                Finish();
            }
            lastPos = nowPos.position;
            isCrashTime = updateCrashTime;
        }
        if(conTimer<=0)
        {
            conversation.SetActive(!conversation.activeSelf);
            conTimer = 3f;
        }
        controller.inputH = inputMove;
        if (Mathf.Abs(nowPos.position.x - targetPos.x) <= 0.11f)
        {
            Finish();
        }
    }
}
