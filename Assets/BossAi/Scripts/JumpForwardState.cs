using Anthill.AI;
using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpForwardState : AntAIState
{
    public BossObject bossObject;
    public PixelCharacter character;
    public PixelCharacterController controller;
    public BossUnitControl bossUnitControl;
    private float speed = 30f;
    public int count = 0;
    private float timer = 1f;
    public GameObject attackRange;
    public override void Create(GameObject aGameObject)
    {
        count = 0;
        character = aGameObject.GetComponent<PixelCharacter>();
        controller = aGameObject.GetComponent<PixelCharacterController>();
        bossObject = aGameObject.GetComponent<BossObject>();
        bossUnitControl = aGameObject.GetComponent<BossUnitControl>();
        attackRange = aGameObject.transform.GetChild(2).gameObject;
    }

    public override void Enter()
    {
        controller.inputCrounch = false;

            
            controller.inputH = 0f;
            controller.inputJump = true;
            controller.inputJumpDown = true;
            GetComponentInParent<Rigidbody2D>().velocity = new Vector2(character.Facing * speed, 1f);
        attackRange.SetActive(true);

    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        count++;
        controller.inputJump = false;
        controller.inputJumpDown = false;
        timer -= aDeltaTime;
        if(timer<=0)
        {
            if (bossObject.isGround == true)
            {
                Debug.Log("Finish!");
                attackRange.SetActive(false);
                controller.inputJump = false;
                controller.inputJumpDown = false;
                bossUnitControl.JumpForward = false;
                bossUnitControl.DownForward = true;
                count = 0;
                timer = 1f;
                Finish();
            }
        }

    }
}
