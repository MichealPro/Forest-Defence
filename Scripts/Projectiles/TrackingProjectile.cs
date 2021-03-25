using UnityEngine;
using System.Collections;
using System;

public class TrackingProjectile : BaseProjectile
{

    public GameObject hitEffect = null;

    Enemy m_target;
    GameObject m_launcher;
    int m_damage;

    //Vector3 m_lastKnownPosition;

    private void Start()
    {
        transform.LookAt(GetAimLocation());
    }

    void Update()
    {
        if (m_target)
        {
            transform.LookAt(GetAimLocation());
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = m_target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null)
        {
            return m_target.transform.position;
        }
        return m_target.transform.position + Vector3.up * targetCapsule.height / 2;
    }

    public override void FireProjectile(GameObject launcher, Enemy target, int damage, float attackSpeed)
    {
        if (target)
        {
            m_target = target;
            m_launcher = launcher;
            m_damage = damage;

            Destroy(gameObject, 1.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_target)
        {

            HitEnemy();
        }
    }

    void HitEnemy()
    {
        if (hitEffect != null)
        {
            GameObject hitEff = Instantiate(hitEffect, m_target.transform.position, transform.rotation) as GameObject;
            Destroy(hitEff, 2f);
        }

        Damage(m_target.gameObject);

        Destroy(gameObject, 1.0f);
    }

    void Damage(GameObject m_target)
    {
        Enemy e= m_target.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(m_damage);
        }
    }
}
