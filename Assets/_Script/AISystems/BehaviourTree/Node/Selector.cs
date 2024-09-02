namespace BHSSolo.DungeonDefense.AISystem
{
    /// <summary>
    ///
    /// </summary>
    public class Selector : Composite
    {
        public Selector(BehaviourTree tree) : base(tree)
        {
        }

        public override Result Invoke()
        {
            Result result = Result.Failure;

            for (int ix = currentChildIndex; ix < children.Count; ix++)
            {
                result = children[ix].Invoke();

                switch (result)
                {
                    case Result.Success:
                        {
                            currentChildIndex = 0;
                            return result;
                        }

                    case Result.Failure:
                        {
                            currentChildIndex++; //Used At Running
                        }
                        break;

                    case Result.Running:
                        {
                            return result;
                        }

                    default:
                        throw new System.Exception($"Invalid result code : {result}");
                }
            }

            currentChildIndex = 0; //All Failed.
            return result;
        }
    }
}