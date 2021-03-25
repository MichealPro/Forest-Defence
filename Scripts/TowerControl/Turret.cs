using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Turret : MonoBehaviour
{

	private Transform target;
	private Enemy targetEnemy;

	[Header("General")]

	public float range = 15f;

	[Header("Use Bullets (default)")]
	public GameObject bulletPrefab;
	public float fireRate = 1f;
	private float fireCountdown = 0f;
	public AudioSource CannonAudio;

	[Header("Use Laser")]
	public bool useLaser = false;

	public int damageOverTime = 30;
	public float slowAmount = .5f;

	public LineRenderer lineRenderer;
	public ParticleSystem impactEffect;
	public Light impactLight;
	public AudioSource LaserAudio;

	[Header("Use Hammer")]

	public bool useHammer = false;
	public GameObject hammerEffect = null;
	public GameObject hammerHitEffect = null;
	public GameObject hammerPoint = null;

	List<GameObject> hammerEnemies = new List<GameObject>();
	private float hammerTime = 1f;

	[Header("Use Tesla")]
	public bool useTesla = false;

	public int TeslaDamageOverTime = 30;

	public LineRenderer lRend;
	public ParticleSystem TeslaEffect;
	public Light TeslaLight;
	public AudioSource TeslaAudio;

    [Header("Unity Setup Fields")]

	public string enemyTag = "Enemy";

	public Transform partToRotate;
	public float turnSpeed = 10f;

	public Transform firePoint;

	void Start()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
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
            if (distanceToEnemy <= range)
            {
				hammerEnemies.Add(enemy);
            }
            else
            {
				hammerEnemies.Remove(enemy);
            }
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
			targetEnemy = nearestEnemy.GetComponent<Enemy>();
		}
		else
		{
			target = null;
		}

	}

	void Update()
	{
		if (target == null)
		{
			if (useLaser)
			{
				if (lineRenderer.enabled)
				{
					lineRenderer.enabled = false;
					impactEffect.Stop();
					impactLight.enabled = false;
				}
			}
			if (useTesla)
			{
				if (lRend.enabled)
				{
					lRend.enabled = false;
					TeslaEffect.Stop();
					TeslaLight.enabled = false;
				}
			}

			return;
		}

		LockOnTarget();

		if (useLaser)
		{
			Laser();
		}
		else if (useHammer)
        {
			Hammer();
        }
		else if (useTesla)
        {
			TeslaElectric();
        }
		else
		{
			if (fireCountdown <= 0f)
			{
				Shoot();
				fireCountdown = 1f / fireRate;
			}

			fireCountdown -= Time.deltaTime;
		}

	}

	void LockOnTarget()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	void Laser()
	{
		targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
		targetEnemy.Slow(slowAmount);

		if (!lineRenderer.enabled)
		{
			lineRenderer.enabled = true;
			impactEffect.Play();
			LaserAudio.Play();
			impactLight.enabled = true;
		}

		lineRenderer.SetPosition(0, firePoint.position);
		lineRenderer.SetPosition(1, target.position);

		Vector3 dir = firePoint.position - target.position;

		impactEffect.transform.position = target.position + dir.normalized;

		impactEffect.transform.rotation = Quaternion.LookRotation(dir);
	}

    void Hammer()
    {
        if (Time.time > hammerTime && hammerEffect)
        {
            Instantiate(hammerEffect, hammerPoint.transform.position, hammerPoint.transform.rotation);
        }
        foreach (GameObject enemy in hammerEnemies)
        {
            if (!hammerHitEffect)
            {
                Instantiate(hammerHitEffect, enemy.transform.position, Quaternion.identity);
            }
            enemy.GetComponent<Enemy>().TakeDamage(damageOverTime * Time.deltaTime * 0.1f);
        }

        hammerTime += 1f;
    }
	void TeslaElectric()
    {
		targetEnemy.TakeDamage(TeslaDamageOverTime * Time.deltaTime);

		if (!lRend.enabled)
		{
			lRend.enabled = true;
			TeslaEffect.Play();
			TeslaAudio.Play();
			TeslaLight.enabled = true;
		}
		lRend.SetPosition(0, firePoint.position);
		lRend.SetPosition(1, target.position);

		Vector3 dir = firePoint.position - target.position;

		TeslaEffect.transform.position = target.position + dir.normalized;

		TeslaEffect.transform.rotation = Quaternion.LookRotation(dir);
	}


	void Shoot()
	{
		CannonAudio.Play();
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();

		if (bullet != null)
			bullet.Seek(target);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}