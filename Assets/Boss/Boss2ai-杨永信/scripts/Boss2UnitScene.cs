using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2UnitScene : MonoBehaviour, ISense
{
    public Boss2UnitControl bossUnitControl;

    private void Awake()
    {
        bossUnitControl = GetComponent<Boss2UnitControl>();
    }
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);
        {
            aWorldState.Set(Boss2Scenario.IsLive, true);
            aWorldState.Set(Boss2Scenario.Thunder, bossUnitControl.Thunder);
            aWorldState.Set(Boss2Scenario.Rest, bossUnitControl.Rest);
            aWorldState.Set(Boss2Scenario.IsLoopFinished, false);
        }
        aWorldState.EndUpdate();
    }

}
