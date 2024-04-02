using Anthill.AI;
using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashAttackState : AntAIState
{
    public BossObject bossObject;
    public PixelCharacter character;
    public PixelCharacterController controller;
    public Boss3UnitControl bossUnitControl;
    public GameManagement gameManagement;
    public GameObject flashUser;
    private float timer = 1.5f;
    private Vector2 target;

    public override void Create(GameObject aGameObject)
    {
        character = aGameObject.GetComponent<PixelCharacter>();
        controller = aGameObject.GetComponent<PixelCharacterController>();
        bossObject = aGameObject.GetComponent<BossObject>();
        bossUnitControl = aGameObject.GetComponent<Boss3UnitControl>();
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        flashUser = aGameObject.gameObject;
    }

    public override void Enter()
    {
        controller.inputH = 0;
        if (bossObject.player_enemy.GetComponent<PixelCharacter>().Facing==1)
        {
            target = new Vector2(bossObject.player_enemy.transform.position.x - 0.8f, bossObject.player_enemy.transform.position.y);
            character.Facing = 1;
        }
        else
        {
            target = new Vector2(bossObject.player_enemy.transform.position.x + 0.8f, bossObject.player_enemy.transform.position.y);
            character.Facing = -1;
        }
        gameManagement.skillManagement.Dodge(flashUser, target);
        controller.inputAttack = true;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        controller.inputAttack = false;
        timer -= aDeltaTime;
        if(timer<=0)
        {
            bossUnitControl.FlashAttack = false;
            bossUnitControl.FlashBack = true;
            
            timer = 1.5f;
            Finish();
        }

    }
}
