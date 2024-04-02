using Anthill.AI;
using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderState : AntAIState
{
    public BossObject bossObject;
    public PixelCharacter character;
    public Transform nowPos;
    public Vector3 targetPos;
    private float inputMove;
    public PixelCharacterController controller;
    public GameObject view;
    public float inputX;
    /// <summary>
    /// �������
    /// </summary>
    private bool dir;
    private bool isCrash = false;
    /// <summary>
    /// �೤ʱ������ײһ��
    /// </summary>
    private float updateCrashTime = 0.08f;
    /// <summary>
    /// ����Ƿ�����ǽ��ʱ��
    /// </summary>
    private float isCrashTime;
    private Vector3 lastPos;
    public Boss2UnitControl bossUnitControl;
    public GameManagement gameManagement;
    public GameObject thunderUser;
    private float timer = 0f;
    private int count = 0;

    public override void Create(GameObject aGameObject)
    {
        character = aGameObject.GetComponent<PixelCharacter>();
        controller = aGameObject.GetComponent<PixelCharacterController>();
        bossObject = aGameObject.GetComponent<BossObject>();
        bossUnitControl = aGameObject.GetComponent<Boss2UnitControl>();
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        thunderUser = aGameObject.gameObject;
        nowPos = aGameObject.GetComponent<Transform>();
        isCrashTime = updateCrashTime;
    }

    public override void Enter()
    {
        controller.inputH = 0;
        controller.isRun = true;
        lastPos = nowPos.position;
        controller.inputH = character.Facing;
        //inputX = monsterObj.inputX;
        if (isCrash == false)
        {
            //ûײǽ�Ͱ���ǰ�������ǰ��ֱ���ߵ��߽�
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
            //�л��������һ��λ��
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
        controller.inputCrounch = false;
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
        if (isCrashTime <= 0)
        {
            Vector3 tempPos = nowPos.position;
            if (tempPos.x - lastPos.x == 0)
            {
                Debug.Log("��ǽ��");
                isCrash = true;
                controller.inputH = 0;
            }
            lastPos = nowPos.position;
            isCrashTime = updateCrashTime;
        }
        controller.inputH = inputMove;
        if (Mathf.Abs(nowPos.position.x - targetPos.x) <= 0.11f)
        {
            controller.inputH = 0;
        }
        timer -= aDeltaTime;
        if (timer <= 0)
        {
            gameManagement.skillManagement.Thunder(thunderUser);
            count++;
            if(count==11)
            {
                bossUnitControl.Rest = true;
                bossUnitControl.Thunder = false;
                count = 0;
                Finish();
            }
            timer = 1f;
            Finish();
        }
    }
}
