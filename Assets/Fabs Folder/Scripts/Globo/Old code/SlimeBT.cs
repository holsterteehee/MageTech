using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using Pathfinding;
using UnityEngine;

public class SlimeBT : BehaviorTree.Tree
{
    public AIPath AI;
    public float WanderingRadius = 10.0f;
    public float DetectionRadius = 5.0f;

    public Animator Animator;

    public Transform Trans, PlayerTrans;
    public SpriteRenderer SR;

    public LayerMask PlayerMask, ObstacleMask;

    protected override Node setUpTree()
    {
        Node root = new Selector(new List<Node>{
            new Sequence(new List<Node>{
                new CheckIfRange(PlayerMask, ObstacleMask, DetectionRadius, Trans, PlayerTrans),
                new GotoTargetTask(AI, Trans),
            }),
            new PatrolTask(AI, WanderingRadius, ref Animator, ref Trans, ref SR),
        });

        return root;
    }
}
