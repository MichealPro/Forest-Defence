using UnityEngine;

public class BuildManger : MonoBehaviour
{
    public static BuildManger instance;
    public GameObject buildEffect;
    public GameObject sellEffect;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("here has been set turret");
            return;
        }
        instance = this;

        
    }

    private TurretBluePrint turretToBuild;
    private TowerBase selectedNode;

    public NodeUI nodeUI;

    public bool CanBuild { get { return turretToBuild != null; } }

    //public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void SelectNode(TowerBase Node)
    {
        if (selectedNode == Node)
        {
            DeselectNode();
            return;
        }

        selectedNode = Node;
        turretToBuild = null;

        nodeUI.SetTarget(Node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTowerToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;

        DeselectNode();
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }

}
