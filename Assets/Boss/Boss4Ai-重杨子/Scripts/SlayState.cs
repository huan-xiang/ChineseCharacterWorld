using Anthill.AI;
using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlayState : AntAIState
{
    public BossObject bossObject;
    public PixelCharacter character;
    public PixelCharacterController controller;
    public Boss4UnitControl bossUnitControl;
    public GameManagement gameManagement;
    public GameObject slayUser;
    private bool dir = true;
    private float timer = 4f;
    private int count;

    public override void Create(GameObject aGameObject)
    {
        character = aGameObject.GetComponent<PixelCharacter>();
        controller = aGameObject.GetComponent<PixelCharacterController>();
        bossObject = aGameObject.GetComponent<BossObject>();
        bossUnitControl = aGameObject.GetComponent<Boss4UnitControl>();
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        slayUser = aGameObject.gameObject;
        count = 0;
    }

    public override void Enter()
    {
        controller.inputH = 0;
        gameManagement.skillManagement.Slay(slayUser);
        //if (dir)
        //{
        //    target = new Vector2((bossObject.leftBorder.x), bossObject.leftBorder.y);
        //    gameManagement.skillManagement.Dodge(flashUser, target);
        //}
        //else
        //{
        //    target = new Vector2((bossObject.rightBorder.x), bossObject.leftBorder.y);
        //    gameManagement.skillManagement.Dodge(flashUser, target);
        //}
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        timer -= aDeltaTime;
        if (timer <= 0)
        {
            if(count==0)
            {
                bossUnitControl.DownForward = true;
                bossUnitControl.Slay = false;
                count++;
                timer = 4f;
                Finish();
            }
            else
            {
                bossUnitControl.JumpBack = true;
                bossUnitControl.Slay = false;
                count = 0;
                timer = 4f;
                Finish();
            }
            
        }

    }
}
