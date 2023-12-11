using BehaviorTree;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : Node
{
    private AIPath aiPath;
    private AIDestinationSetter aiDest;
    private Transform player;
    public ChaseTarget(Node parent) {
        this.parent = parent;

        aiPath = (AIPath)GetData("AIPath");
        aiDest = (AIDestinationSetter)GetData("AIDest");
        player = (Transform)GetData("PlayerDest");
    }

    public override NodeState Eval()
    {
        object hurt = GetData("IsHurt");
        object isAttacking = GetData("IsAttacking");
        aiPath.canMove = (hurt is not null ? !(bool)hurt : true) && (isAttacking is not null ? !(bool)isAttacking : true);
        aiDest.target = player;
        return NodeState.SUCCESS;
    }
}
