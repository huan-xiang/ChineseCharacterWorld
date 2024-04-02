using Anthill.AI;
using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2RestState : AntAIState
{
    public BossObject bossObject;
    public PixelCharacter character;
    public PixelCharacterController controller;
    public Boss2UnitControl bossUnitControl;
    public int count = 0;
    private float timer = 5f;
    public override void Create(GameObject aGameObject)
    {
        count = 0;
        character = aGameObject.GetComponent<PixelCharacter>();
        controller = aGameObject.GetComponent<PixelCharacterController>();
        bossObject = aGameObject.GetComponent<BossObject>();
        bossUnitControl = aGameObject.GetComponent<Boss2UnitControl>();
    }

    public override void Enter()
    {
        controller.inputH = 0;
        controller.inputCrounch = true;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        controller.inputCrounch = true;
        timer -= aDeltaTime;
        if (timer <= 0)
        {
            Debug.Log(controller.inputCrounch);
            controller.inputCrounch = false;
            bossUnitControl.Rest = false;
            bossUnitControl.Thunder = true;
            timer = 5f;
            Finish();
        }
    }
}
