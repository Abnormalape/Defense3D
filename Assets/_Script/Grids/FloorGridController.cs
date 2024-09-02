using UnityEngine;

class FloorGridController : MonoBehaviour
{
    public delegate void HasBuildingChanged(GameObject Grid, bool hasBuilding);
    public event HasBuildingChanged OnHasBuildingChanged;

    [SerializeField] private bool _hasBuilding = false;

    public bool HasBuilding
    {
        get => _hasBuilding;
        set
        {
            if (_hasBuilding != value)
            {
                _hasBuilding = value;
                OnHasBuildingChanged(this.gameObject, _hasBuilding);
            }
        }
    }

    [SerializeField] bool temp;
    [SerializeField] bool accep = false;
    private void Update()
    {
        if(accep)
        {
            accep = false;
            HasBuilding = temp;
        }
    }
}