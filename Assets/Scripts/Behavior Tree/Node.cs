using System.Collections;
using System.Collections.Generic;
using System.Diagnostics; 

namespace BehaviorTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class Node
    {
        protected NodeState state;
        public Node parent;
        protected List<Node> children = new List<Node>();

        private Dictionary<string, object> ctx = new Dictionary<string, object>();

        public Node()
        {
            parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (Node child in children)
                this.pushBack(child);
        }

        private void pushBack(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Eval() => NodeState.FAILURE;

        public void SetData(string key, object val)
        {
            ctx[key] = val;
        }

        public void SetUpperMostParentData(string key, object val) {
            Node node = parent;
            while (node != null) {
                if (node.parent is null) {
                    break;
                }
                node = node.parent;
            }

            node.SetData(key, val);

        }
        public object GetData(string key)
        {
            if (ctx.ContainsKey(key))
            {
                return ctx[key];
            }
            Node node = parent;
            while (node != null)
            {

                object val = node.GetData(key);
                if (val != null)
                {
                    return val;
                }
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (ctx.ContainsKey(key))
            {
                ctx.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool clr = node.ClearData(key);
                if (clr)
                {
                    return clr;
                }
                node = node.parent;
            }

            return false;
        }

    }
}

