using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        TurretType type = GetTurretTypeFromBlueprint(blueprint);
        turret = TurretFactory.CreateTurret(type, GetBuildPosition(), Quaternion.identity);

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret build!");
    }

    private TurretType GetTurretTypeFromBlueprint(TurretBlueprint blueprint)
    {
        if (blueprint.prefab.name == "StandardTurret")
        {
            return TurretType.Standard;
        }
        else if (blueprint.prefab.name == "MissileLauncher")
        {
            return TurretType.Missile;
        }
        else if (blueprint.prefab.name == "LaserBeamer")
        {
            return TurretType.Laser;
        }

        Debug.LogError("Unknown turret type for blueprint: " + blueprint.prefab.name);
        return TurretType.Standard;
    }

    public void UpgradeTurret ()
	{
		if (PlayerStats.Money < turretBlueprint.upgradeCost)
		{
			Debug.Log("Not enough money to upgrade that!");
			return;
		}

		PlayerStats.Money -= turretBlueprint.upgradeCost;

        Transform headTransform = turret.transform.Find("PartToRotate/Head");

        if (headTransform != null)
        {
            MeshRenderer meshRenderer = headTransform.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.materials = turretBlueprint.upgradedMaterials;;
            }
            else
            {
                Debug.LogError("MeshRenderer not found on Head object.");
            }
        }
        else
        {
            Debug.LogError("Head object not found inside turret.");
        }

        TurretDecorator rangeDecorator = gameObject.AddComponent<RangeUpgradeDecorator>();
        TurretDecorator fireRateDecorator = gameObject.AddComponent<FireRateUpgradeDecorator>();
        TurretDecorator damageOverTimeDecorator = gameObject.AddComponent<DamageOverTimeUpgradeDecorator>();
         
        if (turret != null)
        {
            Turret turretScript = turret.GetComponent<Turret>();
            TurretType type = GetTurretTypeFromBlueprint(turretBlueprint);
            rangeDecorator.SetBaseTurret(turretScript);
            fireRateDecorator.SetBaseTurret(turretScript);
            damageOverTimeDecorator.SetBaseTurret(turretScript);

            if (type == TurretType.Standard)
            {
                rangeDecorator.ApplyUpgrade();
                fireRateDecorator.ApplyUpgrade();
            }
            else if (type == TurretType.Missile)
            {
                rangeDecorator.ApplyUpgrade();
                fireRateDecorator.ApplyUpgrade();
            }
            else if (type == TurretType.Laser)
            {
                rangeDecorator.ApplyUpgrade();
                damageOverTimeDecorator.ApplyUpgrade();
            }
            else
            {
                Debug.LogError("Unknown turret type: " + type);
                return;
            }

            isUpgraded = true;

            Debug.Log("Turret upgraded!");
        }
	}

	public void SellTurret ()
	{
		PlayerStats.Money += turretBlueprint.GetSellAmount();

		GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		Destroy(turret);
		turretBlueprint = null;
	}

	void OnMouseEnter ()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (!buildManager.CanBuild)
			return;

        rend.material.color = buildManager.HasMoney ? hoverColor : notEnoughMoneyColor;
	}

	void OnMouseExit ()
	{
		rend.material.color = startColor;
    }

}
