﻿using System;

namespace BHSSolo.DungeonDefense.AISystem
{
    public class Execution : Node
    {
        public Execution(BehaviourTree tree, Func<Result> execute) : base(tree)
        {
            _execute = execute;
        }

        private Func<Result> _execute;


        public override Result Invoke()
        {
            return _execute.Invoke();
        }
    }
}
