using BehaviorTree;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloboBT : BehaviorTree.Tree
{
    public float WanderingDistance = 4.0f;
    public float AttackRange = 4.0f;
    public float DetectionRange = 8.0f;
    public float DashSpeed = 8.0f, DashTime = 1.0f;
    public float AttackCooldown = 3.0f;

    public LayerMask playerMask, obstacleMask;
    public Transform playerTransform;
    protected override Node setUpTree()
    {
        WrapperNode wrapper = new WrapperNode();
        wrapper.SetData("Position", GetComponent<Transform>());
        wrapper.SetData("PlayerMask", playerMask);
        wrapper.SetData("ObstacleMask", obstacleMask);
        wrapper.SetData("AIPath", GetComponent<AIPath>());
        wrapper.SetData("AIDest", GetComponent<AIDestinationSetter>());
        wrapper.SetData("PlayerDest", playerTransform);
        wrapper.SetData("IsDamaged", false);
        wrapper.SetData("WanderRange", WanderingDistance);
        wrapper.SetData("AttackRange", AttackRange);
        wrapper.SetData("DetectionRange", DetectionRange);
        wrapper.SetData("IsHurt", false);
        wrapper.SetData("DashSpeed", DashSpeed);
        wrapper.SetData("DashTime", DashTime);
        wrapper.SetData("AttackCooldown", AttackCooldown);
        wrapper.SetData("IsAttacking", false);
        wrapper.child = new Selector(new List<Node> {
            new Sequence(new List<Node>{ 
                new CheckAttackRange(wrapper),
                new GloboAttack(wrapper),
            }),
            new Sequence(new List<Node>{
                new CheckInRange(wrapper),
                new ChaseTarget(wrapper),
            }),
            new GloboPatrol(wrapper),
        });
        wrapper.child.parent = wrapper;
        return wrapper;
    }
}
