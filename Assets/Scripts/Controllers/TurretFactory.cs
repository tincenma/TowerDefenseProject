using UnityEngine;


public class TurretFactory
{
    public static GameObject CreateTurret(TurretType type, Vector3 position, Quaternion rotation)
    {
        GameObject turretPrefab = null;

        switch (type)
        {
            case TurretType.Standard:
                turretPrefab = Resources.Load<GameObject>("StandardTurret");
                break;

            case TurretType.Missile:
                turretPrefab = Resources.Load<GameObject>("MissileLauncher");
                break;

            case TurretType.Laser:
                turretPrefab = Resources.Load<GameObject>("LaserBeamer");
                break;

            default:
                Debug.LogError("Turret type not supported");
                break;
        }

        if (turretPrefab != null)
        {
            return GameObject.Instantiate(turretPrefab, position, rotation);
        }

        return null;
    }
}
