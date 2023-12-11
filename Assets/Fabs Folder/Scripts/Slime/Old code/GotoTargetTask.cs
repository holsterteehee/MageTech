using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using Pathfinding;
using UnityEngine;

public class GotoTargetTask : Node
{
    private IAstarAI ai;
    private Transform trans;

    private float stoppingDistance = 0.3f;

    public GotoTargetTask(IAstarAI ai, Transform trans)
    {
        this.ai = ai;
        this.trans = trans;
    }

    public override NodeState Eval()
    {
        if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        {
            ai.destination = ((Transform)GetData("target")).position;
            ai.SearchPath();
        }
        else if (ai.hasPath && ai.remainingDistance < stoppingDistance)
        {
            Debug.Log("GOTOTASK > Reached Path");
            // return NodeState.SUCCESS;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
