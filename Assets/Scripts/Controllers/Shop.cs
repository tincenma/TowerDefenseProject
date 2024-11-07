using UnityEngine;

public class Shop : MonoBehaviour {

	public TurretBlueprint standardTurret;
	public TurretBlueprint missileLauncher;
	public TurretBlueprint laserBeamer;

	BuildManager buildManager;

	void Start ()
	{
		buildManager = BuildManager.instance;
        if (buildManager == null)
        {
            Debug.LogError("BuildManager is not found in the scene. Make sure BuildManager exists and is correctly set.");
        }
    }

    public void SelectStandardTurret()
    {
        if (standardTurret == null)
        {
            Debug.LogError("Standard Turret не назначен в инспекторе.");
            return;
        }

        if (buildManager == null)
        {
            Debug.LogError("BuildManager не инициализирован.");
            return;
        }

        Debug.Log("Standard Turret Selected");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectLaserBeamer()
    {
        if (laserBeamer == null)
        {
            Debug.LogError("Laser Beamer не назначен в инспекторе.");
            return;
        }

        if (buildManager == null)
        {
            Debug.LogError("BuildManager не инициализирован.");
            return;
        }

        Debug.Log("Laser Beamer Selected");
        buildManager.SelectTurretToBuild(laserBeamer);
    }

    public void SelectMissileLauncher()
    {
        if (missileLauncher == null)
        {
            Debug.LogError("Missile Launcher turret blueprint is not assigned in the inspector.");
            return;
        }

        if (buildManager == null)
        {
            Debug.LogError("BuildManager is not initialized.");
            return;
        }

        Debug.Log("Missile Launcher Selected");
        buildManager.SelectTurretToBuild(missileLauncher);
    }

}
