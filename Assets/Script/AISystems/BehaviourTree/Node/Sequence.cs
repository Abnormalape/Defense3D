namespace BHSSolo.DungeonDefense.AISystem
{
    /// <summary>
    ///
    /// </summary>
    public class Sequence : Composite
    {
        public Sequence(BehaviourTree tree) : base(tree)
        {
        }

        public override Result Invoke()
        {
            Result result = Result.Success;

            for (int ix = currentChildIndex; ix < children.Count; ix++)
            {
                result = children[ix].Invoke();

                switch (result)
                {
                    case Result.Success:
                        {
                            currentChildIndex++; //Used At Running
                        }
                        break;

                    case Result.Failure:
                        {   
                            currentChildIndex = 0;
                            return result;
                        }

                    case Result.Running:
                        {
                            return result;
                        }

                    default:
                        throw new System.Exception($"Invalid result code : {result}");
                }
            }

            currentChildIndex = 0; //All Success.
            return result;
        }
    }
}