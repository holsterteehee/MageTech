using BehaviorTree;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GloboPatrol : BehaviorTree.Node
{

    private float wanderingDistance = 4.0f;
    private GameObject wanderPoint;
    private GameObject spawnPoint;
    private bool toSpawn;
    private AIPath aiPath;
    private AIDestinationSetter aiDest;

    // Change this if needed but I do not intend to make this
    // public. It's only use is to make sure it does not get
    // stuck in walls.

    private float stoppingDistance = 1.0f;

    // Since GlobalPatrol calls GetData before its parent node is ever
    // assigned then the parent node needs to be passed down as arg.
    // Failure to do so always leads with GloboPatrol's memebers to be
    // null.

    // Use a WrapperNode as a dummy node to fill the ctx with the apporiate
    // data as the following example shows.
    // ===============================================================
    // WrapperNode wrapper = new WrapperNode();
    // wrapper.SetData("Position", GetComponent<Transform>());
    // wrapper.SetData("PlayerMask", playerMask);
    // wrapper.SetData("ObstacleMask", obstacleMask);
    // wrapper.SetData("AIPath", GetComponent<AIPath>());
    // wrapper.SetData("AIDest", GetComponent<AIDestinationSetter>());
    // wrapper.SetData("PlayerDest", playerTransform);
    // wrapper.SetData("IsDamaged", false);
    // wrapper.SetData("WanderRange", WanderingDistance);
    // wrapper.SetData("AttackRange", AttackRange);
    // wrapper.child = new GloboPatrol(wrapper);
    // ================================================================

    public GloboPatrol(Node parent) {
        this.parent = parent;

        wanderPoint = new GameObject();
        spawnPoint = new GameObject();
        
        toSpawn = false;

        aiPath = (AIPath)GetData("AIPath");
        aiDest = (AIDestinationSetter)GetData("AIDest");
        
        wanderingDistance = (float)GetData("AttackRange");
        
        copyTransform((Transform)GetData("Position"), spawnPoint.transform);
    }

    public override NodeState Eval()
    {
        object hurt = GetData("IsHurt");
        object isAttacking = GetData("IsAttacking");
        aiPath.canMove = (hurt is not null ? !(bool)hurt : true) && (isAttacking is not null ? !(bool)isAttacking : true);

        if (!aiPath.pathPending && (aiPath.reachedEndOfPath || !aiPath.hasPath)) {
            randomPointInRadius();
            aiDest.target = wanderPoint.transform;
            aiPath.SearchPath();
        } 
        else if (aiPath.hasPath && aiPath.remainingDistance < stoppingDistance) {
            toSpawn = true;
        }
         
        return NodeState.RUNNING;
    }
    
    private void randomPointInRadius() {
        copyTransform(spawnPoint.transform, wanderPoint.transform);
        Vector3 newVec = wanderPoint.transform.position;
        if (!toSpawn) {
            newVec = Random.insideUnitSphere * wanderingDistance + spawnPoint.transform.position;
            newVec.z = 0;
        }
        toSpawn = false;
        wanderPoint.transform.position = newVec;
    }
    
    private void copyTransform(Transform source, Transform destination)
    {
        destination.position = source.position;
        destination.rotation = source.rotation;
        destination.localScale = source.localScale;
    }
}
