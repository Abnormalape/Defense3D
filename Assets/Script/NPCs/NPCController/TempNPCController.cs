using BHSSolo.DungeonDefense.AISystem;
using BHSSolo.DungeonDefense.NPCs;
using UnityEngine;

[RequireComponent(typeof(BehaviourTree))] //Make Component.
public class TempNPCController : MonoBehaviour
{
    private void Awake()
    {
        BehaviourTree tree = GetComponent<BehaviourTree>();
        tree.Build();
        Selector selector1 = new(tree);
        tree.root.child = selector1;
        Sequence sequence1 = new(tree);
        Selector selector2 = new(tree);
        selector1.children.Add(sequence1);
        selector1.children.Add(selector2);
        Execution seek = new(tree, () =>
        {
            Debug.Log("Start Seek");
            return Result.Success;
        });
        sequence1.children.Add(seek);
        Decorator isInAttackRange = new(tree, () =>
        {
            Debug.Log("Check target is attack range");
            return true;
        });
        Execution attack = new(tree, () =>
        {
            Debug.Log("Start Attack");
            this.transform.position += Vector3.forward * Time.deltaTime;
            return Result.Success;
        });
        isInAttackRange.child = attack;
        sequence1.children.Add(isInAttackRange);
    }
}
