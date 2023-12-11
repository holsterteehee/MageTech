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
        DrawWireCircle(self.position, attackRange, Color.red);
        Collider2D player = Physics2D.OverlapCircle(self.position, attackRange, playerMask);
        if (player) {
            return NodeState.SUCCESS;
        }
        SetUpperMostParentData("IsAttacking", false);
        return NodeState.FAILURE;
    }

    private void DrawWireCircle(Vector2 center, float radius, Color color, int segments = 20)
    {
        float angleIncrement = 2f * Mathf.PI / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle1 = i * angleIncrement;
            float angle2 = (i + 1) * angleIncrement;

            Vector2 point1 = center + new Vector2(Mathf.Cos(angle1) * radius, Mathf.Sin(angle1) * radius);
            Vector2 point2 = center + new Vector2(Mathf.Cos(angle2) * radius, Mathf.Sin(angle2) * radius);

            Debug.DrawLine(point1, point2, color);
        }
    }
}
