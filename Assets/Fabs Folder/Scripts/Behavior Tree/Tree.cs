using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {

        public Node root = null;


        // Start is called before the first frame update
        void Start()
        {
            root = setUpTree();
        }

        // Update is called once per frame
        void Update()
        {
            if (root != null)
            {
                root.Eval();
            }
        }

        protected abstract Node setUpTree();
    }
}