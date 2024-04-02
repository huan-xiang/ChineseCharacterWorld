using Anthill.AI;
using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4DownForwardState : AntAIState
{
    public BossObject bossObject;
    public PixelCharacter character;
    public PixelCharacterController controller;
    public Boss4UnitControl bossUnitControl;
    private float speed = 30f;
    private int count = 0;
    private float timer = 1f;
    public override void Create(GameObject aGameObject)
    {
        count = 0;
        character = aGameObject.GetComponent<PixelCharacter>();
        controller = aGameObject.GetComponent<PixelCharacterController>();
        bossObject = aGameObject.GetComponent<BossObject>();
        bossUnitControl = aGameObject.GetComponent<Boss4UnitControl>();
    }

    public override void Enter()
    {
        controller.inputCrounch = false;
        controller.inputH = 0f;
        GetComponentInParent<Rigidbody2D>().velocity = new Vector2(character.Facing * speed, 0f);
        controller.inputAttack = true;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        timer -= Time.deltaTime;
        controller.inputAttack = false;
        if (timer <= 0)
        {
            bossUnitControl.DownForward = false;
            bossUnitControl.Slay = true;
            character.Facing = -character.Facing;
            count = 0;
            timer = 1f;
            Finish();
        }
    }
}
