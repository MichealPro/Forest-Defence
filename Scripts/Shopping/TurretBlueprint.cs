using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBluePrint
{
    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefabTwo;
    public int upgradeCostTwo;

    public GameObject upgradedPrefabThree;
    public int upgradeCostThree;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
