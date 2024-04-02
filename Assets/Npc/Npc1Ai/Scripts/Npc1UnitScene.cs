using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc1UnitScene : MonoBehaviour, ISense
{
    public Npc1UnitControl npcUnitControl;

    private void Awake()
    {
        npcUnitControl = GetComponent<Npc1UnitControl>();
    }
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);
        {
        }
        aWorldState.EndUpdate();
    }
}
