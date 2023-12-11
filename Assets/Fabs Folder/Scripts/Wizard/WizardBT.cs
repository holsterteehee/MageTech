using BehaviorTree;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBT : BehaviorTree.Tree
{
    public Transform Player;

    public LayerMask PlayerMask, ObstacleMask;

    public float RoamDistance = 4.0f;

    public float DetectionRange = 14.0f;

    public float AttackRange = 10.0f;
    public float AttackCooldown = 3.0f;

    protected override BehaviorTree.Node setUpTree()
    {
        WrapperNode wrapper = new WrapperNode();
        wrapper.SetData("Position", transform);

        wrapper.SetData("PlayerDest", Player);

        wrapper.SetData("AIPath", GetComponent<AIPath>());
        wrapper.SetData("AIDest", GetComponent<AIDestinationSetter>());

        wrapper.SetData("PlayerMask", PlayerMask);
        wrapper.SetData("ObstacleMask", ObstacleMask);

        wrapper.SetData("RoamDistance", RoamDistance);

        wrapper.SetData("DetectionRange", DetectionRange);

        wrapper.SetData("AttackRange", AttackRange);
        wrapper.SetData("AttackCooldown", AttackCooldown);

        wrapper.SetData("IsHurt", false);
        wrapper.SetData("IsAttacking", false);

        wrapper.child = new Selector(new List<Node>{ 
            new Sequence(new List<Node> { 
                new CheckInRange(wrapper),
                new ChaseTarget(wrapper),
            }),
            new WizardRoam(wrapper),
        });

        wrapper.child.parent = wrapper;
        return wrapper;
    }
}
