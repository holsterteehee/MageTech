using BehaviorTree;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardRoam : BehaviorTree.Node
{
    private float roamDistance = 4.0f;

    private GameObject roamTarget;

    private AIPath aiPath;
    private AIDestinationSetter aiDest;

    public WizardRoam(Node parent) { 
        this.parent = parent;
        
        roamTarget = new GameObject();
        
        roamDistance = (float)GetData("RoamDistance");

        aiPath = (AIPath)GetData("AIPath");
        aiDest = (AIDestinationSetter)GetData("AIDest");
    }

    public override NodeState Eval()
    {
        object hurt = GetData("IsHurt");
        object isAttacking = GetData("IsAttacking");
        aiPath.canMove = (hurt is not null ? !(bool)hurt : true) && (isAttacking is not null ? !(bool)isAttacking : true);

        if (!aiPath.pathPending && (aiPath.reachedEndOfPath || !aiPath.hasPath) || aiPath.remainingDistance <= 3.0f)
        {
            SetUpperMostParentData("Patrolling", true);
            nextRoamTarget();
            aiDest.target = roamTarget.transform;
            aiPath.SearchPath();
        }
        return NodeState.RUNNING;
    }

    public void nextRoamTarget() {
        Vector3 roamVec = roamTarget.transform.position;

        roamVec = Random.insideUnitSphere * roamDistance + roamVec;
        roamVec.z = 0;

        roamTarget.transform.position = roamVec;
    }
}
