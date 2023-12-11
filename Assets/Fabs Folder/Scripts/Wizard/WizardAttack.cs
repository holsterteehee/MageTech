using BehaviorTree;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttack : BehaviorTree.Node
{
    private float attackCooldown;
    private float cooldownTimer;
    private float attackRange;

    private bool isAttacking;
    private bool onCooldown;

    private AIPath aiPath;

    private Transform self;
    private Transform target;

    private LayerMask playerMask;


    public WizardAttack(Node parent) {
        this.parent = parent;

        attackCooldown = (float)GetData("AttackCooldown");
        attackRange = (float)GetData("AttackRange");
        cooldownTimer = 0.0f;

        isAttacking = false;
        onCooldown = false;

        aiPath = (AIPath)GetData("AIPath");

        self = (Transform)GetData("Position");
        target = (Transform)GetData("PlayerDest");
    }

    public override NodeState Eval()
    {
        SetUpperMostParentData("IsAttacking", isAttacking);

        object hurt = GetData("IsHurt");
        aiPath.canMove = false;

        if ((bool)hurt) {
            cooldownTimer = 0.0f;
            onCooldown = false;
            isAttacking = false;
            return NodeState.FAILURE;
        }

        if (!onCooldown)
        {
            state = NodeState.RUNNING;
            Shoot();
        }
        else {
            state = NodeState.SUCCESS;

            cooldownTimer += Time.fixedDeltaTime;
            if (cooldownTimer >= attackCooldown) {
                onCooldown = false;
                cooldownTimer = 0.0f;
            }
        }

        return state;
    }

    private void Shoot() {
        isAttacking = true;
        onCooldown = true;
        Vector3 dir = target.position - self.position;
        SpawnManager sm = SpawnManager.Instance;
        GameObject fireball = sm.SpawnFireball();
        fireball.transform.position = self.position;
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(dir.normalized.x * 15.0f, dir.normalized.y * 15.0f);
        Fireball fb = fireball.GetComponent<Fireball>();
        fb.target = target.position;
        
    }
}
