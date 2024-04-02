using Anthill.AI;
using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4FireBallState : AntAIState
{
    public BossObject bossObject;
    public PixelCharacter character;
    public PixelCharacterController controller;
    public Boss4UnitControl bossUnitControl;
    public GameManagement gameManagement;
    public GameObject fireBallUser;
    private bool isFire = true;
    private float timer = 1.8f;
    private int count = 0;


    public override void Create(GameObject aGameObject)
    {
        character = aGameObject.GetComponent<PixelCharacter>();
        controller = aGameObject.GetComponent<PixelCharacterController>();
        bossObject = aGameObject.GetComponent<BossObject>();
        bossUnitControl = aGameObject.GetComponent<Boss4UnitControl>();
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        fireBallUser = aGameObject.gameObject;
    }

    public override void Enter()
    {
        gameManagement.skillManagement.FireBall(fireBallUser);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        timer -= aDeltaTime;
        if (timer <= 0)
        {
            if (count == 0)
            {
                bossUnitControl.JumpBack = true;
                bossUnitControl.FireBall = false;
                count++;
                timer = 1.8f;
                Finish();
            }
            else
            {
                bossUnitControl.FireBall = false;
                bossUnitControl.Thunder = true;
                count = 0;
                timer = 1.8f;
                Finish();
            }
        }
        Finish();

    }
}
