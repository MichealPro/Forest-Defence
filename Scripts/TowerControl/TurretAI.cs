using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretAI : MonoBehaviour
{
    TrackingSystem m_tracker;
    ShootingSystem m_shooter;

    Enemy target;
    [Header("General")]
    public float range = 10f;

    [Header("Unity Setup Fields")]
    public bool stayStill = true;
    public string enemyTag = "Enemy";


    void Start()
    {
        m_tracker = GetComponent<TrackingSystem>();
        m_shooter = GetComponent<ShootingSystem>();
        

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void Update()
    {
        if (!target)
            return;

        if (stayStill)
        {
            m_tracker.SetTarget(target);
            m_shooter.SetTarget(target);
        }
        else
        {
            m_tracker.SetTarget(target);
            m_shooter.SetTarget(target);
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }

    }
}