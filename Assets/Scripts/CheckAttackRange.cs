using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttackRange : BehaviorTree.Node
{
    private Transform self;
    private float attackRange;
    private LayerMask playerMask;
    public CheckAttackRange(Node parent) {
        this.parent = parent;
        self = (Transform)GetData("Position");
        attackRange = (float)GetData("AttackRange");
        playerMask = (LayerMask)GetData("PlayerMask");
    }

    public override NodeState Eval()
    {
        Collider2D player = Physics2D.OverlapCircle(self.position, attackRange, playerMask);
        if (player) {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
