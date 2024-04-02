using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3UnitScene : MonoBehaviour, ISense
{
    public Boss3UnitControl bossUnitControl;

    private void Awake()
    {
        bossUnitControl = GetComponent<Boss3UnitControl>();
    }
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);
        {
            aWorldState.Set(Boss3Scenario.IsLive, true);
            aWorldState.Set(Boss3Scenario.FlashBack, bossUnitControl.FlashBack);
            aWorldState.Set(Boss3Scenario.FlashAttack, bossUnitControl.FlashAttack);
            aWorldState.Set(Boss3Scenario.IsLoopFinished, false);
        }
        aWorldState.EndUpdate();
    }



}
