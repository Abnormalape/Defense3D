namespace BHSSolo.DungeonDefense.NPCs
{
    abstract public class State
    {
        private object _stateCaller;
        public object StateCaller { get => _stateCaller; set => _stateCaller = value; }

        public State()
        {

        }

        public State(object stateCaller)
        {
            StateCaller = stateCaller;
        }

        public void OnStateEnter()
        {

        }

        public void OnStateUpdate()
        {

        }

        public void OnStateExit() 
        { 
            
        }
    }
}
