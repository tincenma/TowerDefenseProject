using UnityEngine;
using System.Collections;

[System.Serializable]
public class TurretBlueprint {

	public GameObject prefab;
	public int cost;

    public Material[] upgradedMaterials;
    public int upgradeCost;

    public int GetSellAmount ()
	{
		return cost / 2;
	}

}
