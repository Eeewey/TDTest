using UnityEngine;

public class CannonballTower : Tower
{
    public float aimSpeed = 20f;
    public CannonProjectile projectilePrefab;
    
    public Transform TurretTr;
    public Transform GunTr;
    
    public override bool RotateTower(Vector3 aimPoint)
    {
        var targetRotation = Quaternion.LookRotation(aimPoint - TurretTr.position);

        var newTurretRot = Quaternion.Slerp(TurretTr.localRotation, targetRotation,
            aimSpeed * Time.deltaTime);

        newTurretRot.x = 0;
        newTurretRot.z = 0;
        
        targetRotation.x = 0;
        targetRotation.z = 0;

        TurretTr.localRotation = newTurretRot;

        var gunRot = Quaternion.LookRotation(aimPoint - GunTr.position);

        var newGunRot = Quaternion.Slerp(GunTr.localRotation, gunRot, aimSpeed * Time.deltaTime);

        newGunRot.y = 0;
        newGunRot.z = 0;
        
        gunRot.y = 0;
        gunRot.z = 0;

        GunTr.localRotation = newGunRot;

        return Mathf.Abs(TurretTr.localRotation.y - targetRotation.y) <= 0.03f
               && Mathf.Abs(GunTr.localRotation.x - gunRot.x) <= 0.03f;
    }

    public override void Attack(GameObject target = null)
    {
        var projectile = Instantiate(projectilePrefab);

        projectile.m_speed = projectileSpeed;
        projectile.m_damage = projectileDamage;

        projectile.transform.position = ShootPointTransform.position;
        projectile.transform.rotation = ShootPointTransform.rotation;
    }
}