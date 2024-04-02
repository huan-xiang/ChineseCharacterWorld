using Anthill.AI;
using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestState : AntAIState
{
    public BossObject bossObject;
    public PixelCharacter character;
    public PixelCharacterController controller;
    public BossUnitControl bossUnitControl;
    public float force = 5f;
    public int count = 0;
    private float timer = 5f;
    public override void Create(GameObject aGameObject)
    {
        count = 0;
        character = aGameObject.GetComponent<PixelCharacter>();
        controller = aGameObject.GetComponent<PixelCharacterController>();
        bossObject = aGameObject.GetComponent<BossObject>();
        bossUnitControl = aGameObject.GetComponent<BossUnitControl>();
    }

    public override void Enter()
    {
        
        controller.inputCrounch = true;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        controller.inputCrounch = true;
        timer -= aDeltaTime;
        if(timer<=0)
        {
            Debug.Log(controller.inputCrounch);
            controller.inputCrounch = false;
            bossUnitControl.Rest = false;
            bossUnitControl.JumpBack = true;
            timer = 5f;
            Finish();
        }
    }
}
