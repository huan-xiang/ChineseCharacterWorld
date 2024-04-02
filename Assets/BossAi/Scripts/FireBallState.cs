using Anthill.AI;
using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallState : AntAIState
{
    public BossObject bossObject;
    public PixelCharacter character;
    public PixelCharacterController controller;
    public BossUnitControl bossUnitControl;
    public GameManagement gameManagement;
    public GameObject fireBallUser;
    private bool isFire = true;
    private float timer = 1.7f;

    public override void Create(GameObject aGameObject)
    {
        character = aGameObject.GetComponent<PixelCharacter>();
        controller = aGameObject.GetComponent<PixelCharacterController>();
        bossObject = aGameObject.GetComponent<BossObject>();
        bossUnitControl = aGameObject.GetComponent<BossUnitControl>();
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
        if (timer<=0)
        {
            bossUnitControl.JumpForward = true;
            bossUnitControl.FireBall = false;
            isFire = true;
            timer = 1.7f;
        }
        Finish();

    }
}
