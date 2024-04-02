using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnitScene : MonoBehaviour, ISense
{
    public GameObject enemy;
    public BossUnitControl bossUnitControl;

    private void Awake()
    {
        bossUnitControl = GetComponent<BossUnitControl>();
    }
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);
        {
            aWorldState.Set(BossScenario.IsLive, true);
            aWorldState.Set(BossScenario.JumpBack, bossUnitControl.JumpBack);
            aWorldState.Set(BossScenario.FireBall, bossUnitControl.FireBall);
            aWorldState.Set(BossScenario.JumpForward, bossUnitControl.JumpForward);
            aWorldState.Set(BossScenario.DownForward, bossUnitControl.DownForward);
            aWorldState.Set(BossScenario.Rest, bossUnitControl.Rest);
            aWorldState.Set(BossScenario.IsLoopFinished, false);
        }
        aWorldState.EndUpdate();
    }

}
