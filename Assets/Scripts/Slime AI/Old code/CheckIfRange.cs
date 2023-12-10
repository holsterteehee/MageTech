using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CheckIfRange : Node
{
    private LayerMask playerMask, obstacleMask;
    private Animator animator;
    private float detectionRadius;

    private Transform trans;
    private Transform playerTrans;

    public CheckIfRange(LayerMask playerMask, LayerMask obstacleMask, float detectionRadius, Transform trans, Transform playerTrans)
    {
        this.playerMask = playerMask;
        this.obstacleMask = obstacleMask;
        this.detectionRadius = detectionRadius;
        this.trans = trans;
        this.playerTrans = playerTrans;
        animator = trans.GetComponent<Animator>();
    }

    public override NodeState Eval()
    {
        DrawWireCircle(trans.position, detectionRadius, Color.green);
        Collider2D player = Physics2D.OverlapCircle(trans.position, detectionRadius, playerMask);
        if (player != null)
        {
            Vector2 rayDirection = playerTrans.position - trans.position;
            float rayDetectionRadius = rayDirection.magnitude; // Calculate the distance dynamically
            RaycastHit2D hit = Physics2D.Raycast(trans.position, rayDirection, rayDetectionRadius, obstacleMask);

            Debug.DrawRay(trans.position, rayDirection.normalized * rayDetectionRadius, Color.red); // Visualize the ray for debugging

            if (hit)
            {
                Debug.Log("Hit an obstacle: " + hit.collider.gameObject.name);
                return NodeState.FAILURE;
            }
            else
            {
                parent.parent.SetData("target", player.transform);
                //animator.SetBool("Moving", true);
                return NodeState.SUCCESS;
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
