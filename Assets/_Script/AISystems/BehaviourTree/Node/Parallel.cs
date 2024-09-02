namespace BHSSolo.DungeonDefense.AISystem
{
    /// <summary>
    ///
    /// </summary>
    public class Parallel : Composite
    {
        public Parallel(BehaviourTree tree, int succesCountRequired) : base(tree)
        {
            _successCountRequired = succesCountRequired;
        }


        private int _successCountRequired; //Succes Lower Bound.
        private int _successCount;

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
                            _successCount++;
                        }
                        break;

                    case Result.Failure:
                        {
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

            result = _successCount >= _successCountRequired ? Result.Success : Result.Failure;
            _successCount = 0;
            currentChildIndex = 0;
            return result;
        }
    }
}