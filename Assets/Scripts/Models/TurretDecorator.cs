using UnityEngine;

public abstract class TurretDecorator : MonoBehaviour
{
    protected Turret baseTurret;

    public void SetBaseTurret(Turret turret)
    {
        baseTurret = turret;
    }

    public abstract void ApplyUpgrade();
}

public class RangeUpgradeDecorator : TurretDecorator
{
    public override void ApplyUpgrade()
    {
        baseTurret.range += 10;
        Debug.Log("Range upgraded to: " + baseTurret.range);
    }
}

public class FireRateUpgradeDecorator : TurretDecorator
{
    public override void ApplyUpgrade()
    {
        baseTurret.fireRate += 0.4f;
        Debug.Log("Fire rate upgraded to: " + baseTurret.fireRate);
    }
}

public class DamageOverTimeUpgradeDecorator : TurretDecorator
{
    public override void ApplyUpgrade()
    {
        baseTurret.damageOverTime += 10;
        Debug.Log("Fire rate upgraded to: " + baseTurret.damageOverTime);
    }
}
