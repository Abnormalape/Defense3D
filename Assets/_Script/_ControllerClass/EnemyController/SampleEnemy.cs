using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;


namespace BHSSolo.DungeonDefense.Controller
{
    public class SampleEnemy : EnemyController_, IController
    {
        public IManagerClass OwnerManager { get; set; }


        public void ControllerInitializer(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            InitializeEnemyController();
        }

        public override void InitializeEnemyController()
        {
            enemyManager_ = OwnerManager.OwnerManager.EnemyManager_;
            Debug.Log("Overrided EnemyController Initializer.");
            base.InitializeEnemyController();
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}
