using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShootingSystem : MonoBehaviour
{
    public float fireRate;
    public int damage;
    public float fieldOfView;
    public bool beam;
    public GameObject fireEffect;
    public GameObject projectile;
    public List<GameObject> projectileSpawns;

    public AudioSource CrossbowAudio;
    public AudioSource ElectricAudio;


    List<GameObject> m_lastProjectiles = new List<GameObject>();
    List<GameObject> m_lastFireEffect = new List<GameObject>();
    float m_fireTimer = 0.0f;
    Enemy m_target;

    void Update()
    {
        if (m_target == null)
        {
            if (beam)
            {
                RemoveLastProjectiles();
            }

            return;
        }

        if (beam && m_lastProjectiles.Count <= 0)
        {
            m_fireTimer += Time.deltaTime;
            if (m_fireTimer >= fireRate)
            {
                SpawnProjectiles();
                if (fireEffect)
                {
                    SpawnFireEffect();
                }
                m_fireTimer = 0.0f;
            }
                
        }
        else if (beam && m_lastProjectiles.Count > 1)
        {
            RemoveLastProjectiles();
            RemoveLastFireEffect();
        }
        else
        {
            m_fireTimer += Time.deltaTime;

            if (m_fireTimer >= fireRate)
            {
                float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(m_target.transform.position - transform.position));

                if (angle < fieldOfView)
                {
                    SpawnProjectiles();

                    if (fireEffect)
                    {
                        SpawnFireEffect();
                    }

                    m_fireTimer = 0.0f;
                    RemoveLastFireEffect();
                }
            }
        }
    }

    void SpawnFireEffect()
    {
        m_lastFireEffect.Clear();

        for (int i = 0; i < projectileSpawns.Count; i++)
        {
            if (projectileSpawns[i])
            {
                GameObject fireEff = Instantiate(fireEffect, projectileSpawns[0].transform.position, projectileSpawns[0].transform.rotation) as GameObject;

                m_lastFireEffect.Add(fireEff);
            }
        }
    }
    void RemoveLastFireEffect()
    {
        while (m_lastFireEffect.Count > 0)
        {
            Destroy(m_lastFireEffect[0], 1.0f);
            m_lastFireEffect.RemoveAt(0);
        }
    }

    void SpawnProjectiles()
    {
        if (!projectile)
        {
            return;
        }

        m_lastProjectiles.Clear();

        for (int i = 0; i < projectileSpawns.Count; i++)
        {
            if (projectileSpawns[i])
            {
                GameObject proj = Instantiate(projectile, projectileSpawns[i].transform.position, Quaternion.Euler(projectileSpawns[i].transform.forward)) as GameObject;
                proj.GetComponent<BaseProjectile>().FireProjectile(projectileSpawns[i], m_target, damage, fireRate);
                if (CrossbowAudio)
                {
                    CrossbowAudio.Play();
                }
                if (ElectricAudio)
                {
                    ElectricAudio.Play();
                }

                m_lastProjectiles.Add(proj);
            }
        }
    }

    void RemoveLastProjectiles()
    {
        while (m_lastProjectiles.Count > 0)
        {
            Destroy(m_lastProjectiles[0]);
            m_lastProjectiles.RemoveAt(0);
        }
    }

    public void SetTarget(Enemy target)
    {
        m_target = target;
    }
}
