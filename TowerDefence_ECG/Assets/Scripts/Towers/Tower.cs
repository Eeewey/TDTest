using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public float shootInterval  = 0.5f;
    public float shootRange  = 4f;
    public float projectileSpeed = 2f;
    public int projectileDamage = 10;

    public Transform ShootPointTransform;

    public Monster currentEnemy;
    private Vector3 _previosEnemyPos;
    private Vector3 debugAim;
    
    protected float LastAttackTime;

    protected virtual void Start()
    {
        StopAllCoroutines();
        StartCoroutine(FindEnemy());
    }

    protected virtual void FixedUpdate()
    {
        var isRotated = false;
        
        if (currentEnemy != null)
        {
            var pos = GetShootPreemptionPoint(currentEnemy.transform);
            isRotated = RotateTower(pos);
            
            debugAim = pos;
        }
        
        if (Time.time >= LastAttackTime + shootInterval)
        {
            if (isRotated)
            {
                Attack(currentEnemy.gameObject);
                LastAttackTime = Time.time;
            }
        }
    }

    public virtual void Attack(GameObject target = null)
    {
        
    }

    public virtual bool RotateTower(Vector3 aimPoint)
    {
        return false;
    }
    
    protected IEnumerator FindEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            var targetCols = Physics.OverlapSphere(transform.position, shootRange);

            for (int i = 0; i < targetCols.Length; ++i)
            {
                var enemy = targetCols[i].GetComponent<Monster>();
                
                if (!enemy) continue;

                var dist = Vector3.Distance(transform.position, enemy.transform.position);

                if (dist <= shootRange && currentEnemy == null)
                {
                    currentEnemy = enemy;

                    _previosEnemyPos = currentEnemy.transform.position;
                }
            }
        }
    }
    
    protected Vector3 GetShootPreemptionPoint(Transform target)
    {
        var toTarget = target.position - ShootPointTransform.position;
        var targetPosition = target.position;

        var placement = targetPosition - _previosEnemyPos;
        var enemySpeed = placement.magnitude / Time.deltaTime;

        var dir = placement.normalized;
        var enemyVelo = dir * enemySpeed;
        _previosEnemyPos = target.position;

        float a = enemyVelo.sqrMagnitude - Mathf.Pow(projectileSpeed,2);
        float b = 2 * Vector3.Dot(toTarget, enemyVelo);
        float c = toTarget.sqrMagnitude;

        float disc = b * b - 4 * a * c;

        if (disc < 0 || Mathf.Approximately(a, 0))
        {
            return targetPosition;
        }

        var sqrDisc = Mathf.Sqrt(disc);
        var t1 = (-b + sqrDisc) / (2 * a);
        var t2 = (-b - sqrDisc) / (2 * a);

        float t = Mathf.Max(t1, t2);

        var predictedPosition = targetPosition + enemyVelo * Mathf.Abs(t);

        return predictedPosition;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(debugAim, 0.5f);
    }
}