using BehaviorTree;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloboAttack : BehaviorTree.Node
{
    private float dashSpeed, dashTime;
    private float attackCooldown;

    private float dashTimer, cooldownTimer;

    private bool onCooldown = false;
    private bool isAttacking = false;
    
    private AIPath aiPath;
    
    private Transform self;
    private Transform player;
    private Rigidbody2D rb;

    private float attackRange;
    private LayerMask playerMask;
    public GloboAttack(Node parent) { 
        this.parent = parent;
        dashTimer = 0.0f;
        cooldownTimer = 0.0f;
        self = (Transform)GetData("Position");
        player = (Transform)GetData("PlayerDest");
        attackCooldown = (float)GetData("AttackCooldown");
        dashSpeed = (float)GetData("DashSpeed");
        dashTime = (float)GetData("DashTime");
        aiPath = (AIPath)GetData("AIPath");
        attackRange = (float)GetData("AttackRange");
        playerMask = (LayerMask)GetData("PlayerMask");
        rb = self.GetComponent<Rigidbody2D>();
    }

    public override NodeState Eval()
    {
        Collider2D player = Physics2D.OverlapCircle(self.transform.position, attackRange, playerMask);
        if (player is null) {
            return NodeState.FAILURE;
        }
        SetUpperMostParentData("IsAttacking", isAttacking);
        object hurt = GetData("IsHurt");
        aiPath.canMove = (hurt is not null ? !(bool)hurt : true) && !isAttacking;
        //Debug.Log((bool)hurt);
        if ((bool)hurt) {
            //Debug.Log("I got hurt");
            //rb.velocity = Vector3.zero;
            dashTimer = 0.0f;
            cooldownTimer = 0.0f;
            onCooldown = false;
            isAttacking = false;
            return NodeState.FAILURE;
        }

        Dash();
        return state;
    }

    public void Dash()
    {
        state = NodeState.RUNNING;
        if (!onCooldown)
        {
            isAttacking = true;
            while (dashTimer < dashTime) {
                Vector3 dir = player.position - self.position;
                rb.velocity = new Vector2(dir.normalized.x * dashSpeed, dir.normalized.y * dashSpeed);
                dashTimer += Time.deltaTime;
                
            }

            if (dashTimer >= dashTime) {
                onCooldown = true;
                dashTimer = 0.0f;
                state = NodeState.SUCCESS;
            }
        }
        else
        {
            isAttacking = false;
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer < attackCooldown) {
                onCooldown = false;
                cooldownTimer = 0.0f;
            }
            
        }
    }
}
