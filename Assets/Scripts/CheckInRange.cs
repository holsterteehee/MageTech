using BehaviorTree;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInRange : BehaviorTree.Node
{

    private LayerMask playerMask, obstacleMask;
    private float detectionRange;
    private Transform self;
    private Transform player;

    public CheckInRange(Node parent) {
        this.parent = parent;

        playerMask = (LayerMask)GetData("PlayerMask");
        obstacleMask = (LayerMask)GetData("ObstacleMask");
        detectionRange = (float)GetData("DetectionRange");
        self = (Transform)GetData("Position");
        player = (Transform)GetData("PlayerDest");
    }

    public override NodeState Eval()
    {
        DrawWireCircle(self.position, detectionRange, Color.green);
        
        Collider2D playerObj = Physics2D.OverlapCircle(self.position, detectionRange, playerMask);
        if (playerObj != null)
        {
            Vector2 rayDetection = player.position - self.position;
            float rayDetectionRadius = rayDetection.magnitude;
            RaycastHit2D hit = Physics2D.Raycast(self.position, rayDetection, rayDetectionRadius, obstacleMask);
            Debug.DrawRay(self.position, rayDetection.normalized * rayDetectionRadius, Color.red);

            if (hit)
            {
                SetUpperMostParentData("IsAttacking", false);
                return NodeState.FAILURE;
            }
            else
            {
                SetUpperMostParentData("Patrolling", false);
                return NodeState.SUCCESS;
            }

        }
        else {
            if (!(bool)GetData("Patrolling"))
            {
                Transform spawn = ((GameObject)GetData("SpawnPoint"))?.transform;
                AIDestinationSetter aiDest = ((AIDestinationSetter)GetData("AIDest"));
                if (aiDest is not null) aiDest.target = spawn;
            }
            
        }
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
