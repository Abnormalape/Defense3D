using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;


namespace BHSSolo.DungeonDefense.Controller
{
    public class SampleEnemy : EnemyController_, IController
    {
        public IManagerClass OwnerManager { get; set; }
        public override NPCCurrentStatus EnemyStatus_ { get; set; }
        public override NpcBaseStatus EnemyBaseStatus_ { get; set; }
        public override EnemyType EnemyEnum_ { get; set; }
        public override EnemyManager_ EnemyManager_ { get; set; }
        public override int level { get; set; }
        public override int Enemy_ID { get; set; }
        public override BattleManager_ BattleManager_ { get; set; }

        public void ControllerInitializer(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            InitializeEnemyController();

        }


        public override void InitializeEnemyController()
        {
            EnemyManager_ = OwnerManager.OwnerManager.EnemyManager_;
            Debug.Log("Overrided EnemyController Initializer.");
            base.InitializeEnemyController();
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}
