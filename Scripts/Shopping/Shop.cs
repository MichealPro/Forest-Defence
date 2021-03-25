using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TurretBluePrint towerCrystal;
    public TurretBluePrint towerCross;
    public TurretBluePrint towerTesla;
    public TurretBluePrint towerCannon;

    BuildManger buildManager;

    public Text CrystalText;
    public Text CrossbowText;
    public Text TeslaText;
    public Text CannonText;

    public Button CrystalButton;
    public Button CrossbowButton;
    public Button TeslaButton;
    public Button CannonButton;


    private void Start()
    {
        buildManager = BuildManger.instance;

        CrystalText.text = "$" + towerCrystal.cost;
        CrossbowText.text = "$" + towerCross.cost;
        TeslaText.text = "$" + towerTesla.cost;
        CannonText.text = "$" + towerCannon.cost;
    }
    private void Update()
    {
        if (PlayerStats.Money >= towerCrystal.cost && !CrystalButton.interactable)
        {
            CrystalButton.interactable = true;
        }
        if (PlayerStats.Money >= towerCross.cost && !CrossbowButton.interactable)
        {
            CrossbowButton.interactable = true;
        }
        if (PlayerStats.Money >= towerTesla.cost && !TeslaButton.interactable)
        {
            TeslaButton.interactable = true;
        }
        if (PlayerStats.Money >= towerCannon.cost && !CannonButton.interactable)
        {
            CannonButton.interactable = true;
        }
    }
    public void SelectCrystalTower()
    {
        if (PlayerStats.Money >= towerCrystal.cost)
        {
            buildManager.SelectTowerToBuild(towerCrystal);
        }
        else
        {
            CrystalButton.interactable = false;
        }
    }
    public void SelectCrossTower()
    {
        if (PlayerStats.Money >= towerCross.cost)
        {

            buildManager.SelectTowerToBuild(towerCross);
        }
        else
        {
            CrossbowButton.interactable = false;
        }
    }
    public void SelectTeslaTower()
    {
        if (PlayerStats.Money >= towerTesla.cost)
        {

            buildManager.SelectTowerToBuild(towerTesla);
        }
        else
        {
            TeslaButton.interactable = false;
        }
    }
    public void SelectCannonTower()
    {
        if (PlayerStats.Money >= towerCannon.cost)
        {

            buildManager.SelectTowerToBuild(towerCannon);
        }
        else
        {
            CannonButton.interactable = false;
        }
    }
}
