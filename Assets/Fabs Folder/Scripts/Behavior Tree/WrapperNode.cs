using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapperNode : Node
{
    public Node child;

    public WrapperNode() {
        child = null;
    }


    public override NodeState Eval()
    {
        return child is null ? base.Eval(): child.Eval();
    }
}
