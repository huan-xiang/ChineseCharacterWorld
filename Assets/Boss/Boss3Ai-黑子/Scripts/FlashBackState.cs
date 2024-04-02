using Anthill.AI;
using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBackState : AntAIState
{
    public BossObject bossObject;
    public PixelCharacter character;
    public PixelCharacterController controller;
    public Boss3UnitControl bossUnitControl;
    public GameManagement gameManagement;
    public GameObject flashUser;
    private bool dir = true;
    private float timer = 1.7f;
    private Vector2 target;

    public override void Create(GameObject aGameObject)
    {
        character = aGameObject.GetComponent<PixelCharacter>();
        controller = aGameObject.GetComponent<PixelCharacterController>();
        bossObject = aGameObject.GetComponent<BossObject>();
        bossUnitControl = aGameObject.GetComponent<Boss3UnitControl>();
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        flashUser = aGameObject.gameObject;
        timer = bossUnitControl.flashAttackInterval;
    }

    public override void Enter()
    {
        controller.inputH = 0;
        if(dir)
        {
            target = new Vector2((bossObject.leftBorder.x), bossObject.leftBorder.y);
            gameManagement.skillManagement.Dodge(flashUser, target);
        }
        else
        {
            target = new Vector2((bossObject.rightBorder.x), bossObject.leftBorder.y);
            gameManagement.skillManagement.Dodge(flashUser, target);
        }
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        controller.inputAttack = false;
        timer -= aDeltaTime;
        //if (Vector3.Distance(flashUser.transform.position, bossObject.enemy.transform.position) < 2f)
        //{
        //    bossUnitControl.FlashBack = false;
        //    bossUnitControl.FlashAttack = true;
        //    dir = !dir;
        //    Finish();
        //}
        if (timer <= 0)
        {
            bossUnitControl.FlashBack = false;
            bossUnitControl.FlashAttack = true;
            dir = !dir;
            timer = Random.Range(1.8f, 4f);
            Finish();
        }

    }
}
