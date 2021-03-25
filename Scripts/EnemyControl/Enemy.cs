using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent NavMeshAgent;

    public float startSpeed = 2f;
    public int startHealth = 100;
    public int value = 50;

    [SerializeField] WayPoints path;
    public GameObject DestroyEffect;

    [HideInInspector]
    public float speed;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;
    private float health;
    private Vector3 target;
    private int wavepointIndex = 0;

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        target = path.GetWaypoint(0);

        health = startHealth;
        speed = startSpeed;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target) <= 1f)
        {
            GetNextWaypoint();
        }
        MoveTo(target);
        UpdateAnimator();
    }


    public void MoveTo(Vector3 destination)
    {
        NavMeshAgent.destination = destination;
        NavMeshAgent.speed = speed;
        NavMeshAgent.isStopped = false;
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = NavMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        float speed = localVelocity.z;

        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= path.transform.childCount - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = path.GetWaypoint(wavepointIndex);
    }

    void EndPath()
    {
        PlayerStats.Lives -= 1;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    private void Die()
    {
        isDead = true;

        PlayerStats.Money += value;
        GetComponent<Animator>().SetTrigger("die");

        GameObject effect = (GameObject)Instantiate(DestroyEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject, 1f);
    }
}
