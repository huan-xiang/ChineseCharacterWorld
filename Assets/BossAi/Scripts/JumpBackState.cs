using Anthill.AI;
using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBackState : AntAIState
{
    public BossObject bossObject;
    public PixelCharacter character;
    public PixelCharacterController controller;
    public BossUnitControl bossUnitControl;
    public Transform bossTransform;
    private float speed=30f;
    public int count = 0;
    private Vector3 target;
    public override void Create(GameObject aGameObject)
    {
        character = aGameObject.GetComponent<PixelCharacter>();
        controller = aGameObject.GetComponent<PixelCharacterController>();
        bossObject = aGameObject.GetComponent<BossObject>();
        bossUnitControl = aGameObject.GetComponent<BossUnitControl>();
    }

    public override void Enter()
    {
        if(count==0)
        {
            controller.inputH = 0f;
            controller.inputJump = true;
            controller.inputJumpDown = true;
            GetComponentInParent<Rigidbody2D>().velocity = new Vector2((-character.Facing) * speed, 1f);
        }
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {

        count++;
        if (bossObject.isGround==false)
        {
            controller.inputJump = false;
            controller.inputJumpDown = false;
            bossUnitControl.JumpBack = false;
            bossUnitControl.FireBall = true;
            count = 0;
            Finish();
        }
    }
}
