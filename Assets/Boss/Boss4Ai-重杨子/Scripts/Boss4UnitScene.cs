using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4UnitScene : MonoBehaviour, ISense
{
    public Boss4UnitControl bossUnitControl;

    private void Awake()
    {
        bossUnitControl = GetComponent<Boss4UnitControl>();
    }
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);
        {
            aWorldState.Set(Boss4Scenario.IsLive, true);
            aWorldState.Set(Boss4Scenario.DownForward, bossUnitControl.DownForward);
            aWorldState.Set(Boss4Scenario.Slay, bossUnitControl.Slay);
            aWorldState.Set(Boss4Scenario.JumpBack, bossUnitControl.JumpBack);
            aWorldState.Set(Boss4Scenario.FIreBall, bossUnitControl.FireBall);
            aWorldState.Set(Boss4Scenario.Thunder, bossUnitControl.Thunder);
            aWorldState.Set(Boss4Scenario.IsFinshLoop, false);
        }
        aWorldState.EndUpdate();
    }

}
