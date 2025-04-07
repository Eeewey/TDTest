using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MagicTower : Tower
{
    public GuidedProjectile projectilePrefab;

    public override bool RotateTower(Vector3 aimPoint)
    {
        return true;
    }

    public override void Attack(GameObject target)
    {
        var projectile =
            Instantiate(projectilePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        
        projectile.m_target = target;
        projectile.m_speed = projectileSpeed;
        projectile.m_damage = projectileDamage;
    }
}
