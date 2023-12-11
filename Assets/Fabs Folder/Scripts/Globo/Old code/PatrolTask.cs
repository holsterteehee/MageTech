using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine.UIElements;
public class PatrolTask : Node
{
    private IAstarAI ai;
    private float wanderingRadius;
    private Animator animator;
    private float lastX;
    private Transform trans;
    private Vector3 spawnPoint;
    private float stoppingDistance = 0.3f;
    private bool toSpawn = false;
    private SpriteRenderer sr;
    private bool wait = false;
    private float waitCounter = 0.0f;
    private float waitTime = 2.0f;
    public PatrolTask(IAstarAI ai, float wanderingRadius, ref Animator animator, ref Transform trans, ref SpriteRenderer sr)
    {
        this.ai = ai;
        this.wanderingRadius = wanderingRadius;
        this.animator = animator;
        this.trans = trans;
        spawnPoint = trans.position;
        lastX = trans.position.x;
        this.sr = sr;
    }


    private Vector3 randomPointInRadius()
    {
        var newPos = spawnPoint;
        if (!toSpawn)
        {
            newPos = Random.insideUnitSphere * wanderingRadius + spawnPoint;
            newPos.z = 0;
        }
        toSpawn = false;
        return newPos;
    }

    public override NodeState Eval()
    {
        if (lastX < trans.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
        float positionThreshold = 0.001f; // Adjust this threshold based on your specific needs

        if (Mathf.Abs(lastX - trans.position.x) < positionThreshold)
        {
            Debug.Log("Something went wrong");
        }

        lastX = trans.position.x;
        if (wait)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                wait = false;
                //animator.SetBool("Moving", true);
            }
        }
        else
        {
            // if the ai has no pending path and has reached the end of its path or has no path then
            if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
            {
                //animator.SetBool("Moving", true);
                ai.destination = randomPointInRadius();
                ai.SearchPath();
            }
            else if (ai.hasPath && ai.remainingDistance < stoppingDistance)
            {

                Debug.Log("PATROLTASK > Reached Path");
                waitCounter = 0.0f;
                //animator.SetBool("Moving", false);
                toSpawn = true;
                wait = true;
            }
        }


        return NodeState.RUNNING;
    }

}
