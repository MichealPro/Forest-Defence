using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : BaseProjectile
{
    public float ExplosionRadius = 10.0f;
    public GameObject hitEffect = null;

    GameObject m_launcher;
    Enemy m_target;
    int m_damage;

    void Update()
    {
        if (m_target)
        {
            HitTarget();
        }
    }
    void HitTarget()
    {
        if (ExplosionRadius > 0f)
        {
            Explode();
        }
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }
    void Damage(Transform enemy)
    {
        if (!hitEffect)
        {
            Instantiate(hitEffect, enemy.position, Quaternion.identity);
        }

        Enemy e = m_target.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(m_damage);
        }
    }

    public override void FireProjectile(GameObject launcher, Enemy target, int damage, float attackSpeed)
    {
        if (launcher)
        {
            m_launcher = launcher;
            transform.SetParent(m_launcher.transform);

            m_target = target;
            m_damage = damage;
        }
    }
}
