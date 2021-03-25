using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBase : MonoBehaviour
{
    public Vector3 positionOffset;
    //public Color hoverColor;

    private Renderer rend;
    //private Color startColor;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBluePrint turretBluePrint;
    [HideInInspector]
    public bool isUpgradedTwo = false;
    [HideInInspector]
    public bool isUpgradedThree = false;

    public AudioSource buildVoice;
    private GameObject childObj;

    BuildManger buildManger;

    private void Start()
    {
        childObj= transform.GetChild(0).gameObject;
        buildManger = BuildManger.instance;
        rend = GetComponent<Renderer>();
        //startColor = rend.material.color;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (turret != null)
        {
            buildManger.SelectNode(this);
            return;
        }

        if (!buildManger.CanBuild)
        {
            return;
        }
        BuildTurret(buildManger.GetTurretToBuild());
    }

    void BuildTurret(TurretBluePrint bluePrint)
    {
        if (PlayerStats.Money < bluePrint.cost)
        {
            Debug.Log("Not Enough Money");
            return;
        }
        PlayerStats.Money -= bluePrint.cost;

        GameObject _turret = (GameObject)Instantiate(bluePrint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBluePrint = bluePrint;

        GameObject effect = (GameObject)Instantiate(buildManger.buildEffect, GetBuildPosition(), Quaternion.identity);

        buildVoice.Play();
    }

    public void UpdateTurretTwo()
    {
        if (PlayerStats.Money < turretBluePrint.upgradeCostTwo)
        {
            Debug.Log("Not Enough Money to upgrade Two");
            return;
        }
        PlayerStats.Money -= turretBluePrint.upgradeCostTwo;

        Destroy(turret);

        GameObject _turret = (GameObject)Instantiate(turretBluePrint.upgradedPrefabTwo, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManger.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        buildVoice.Play();

        isUpgradedTwo = true;
    }

    public void UpdateTurretThree()
    {
        if (PlayerStats.Money < turretBluePrint.upgradeCostThree)
        {
            Debug.Log("Not Enough Money to upgrade Two");
            return;
        }
        PlayerStats.Money -= turretBluePrint.upgradeCostThree;

        Destroy(turret);

        GameObject _turret = (GameObject)Instantiate(turretBluePrint.upgradedPrefabThree, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManger.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        buildVoice.Play();

        isUpgradedThree = true;
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBluePrint.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(buildManger.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);

        turretBluePrint = null;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (buildManger.CanBuild)
        {
            return;
        }

        childObj.SetActive(true);
    }
    private void OnMouseExit()
    {
        rend.material.color = Color.white;

        childObj.SetActive(false);
    }
}
