using UnityEngine;
using System.Collections;

public class Electric : BaseProjectile
{
    public GameObject hitEffect = null;

    private LineRenderer lRend;
    private readonly int pointsCount = 5;
    private readonly int half = 2;
    private float randomness;
    private Vector3[] points;

    GameObject m_launcher;
    Enemy m_target;
    int m_damage;
    float m_attackSpeed;
    float m_attackTimer;


    private readonly int pointIndexA = 0;
    private readonly int pointIndexB = 1;
    private readonly int pointIndexC = 2;
    private readonly int pointIndexD = 3;
    private readonly int pointIndexE = 4;

    private readonly string mainTexture = "_MainTex";
    private Vector2 mainTextureScale = Vector2.one;
    private Vector2 mainTextureOffset = Vector2.one;

    private float timer;
    private float timerTimeOut = 0.05f;

    private void Start ()
    {
        lRend = GetComponent<LineRenderer>();
        points = new Vector3[pointsCount];
        lRend.positionCount = pointsCount;
    }

    private void Update()
    {
        CalculatePoints();
    }

    private void CalculatePoints()
    {
        timer += Time.deltaTime;

        if (timer > timerTimeOut)
        {
            timer = 0;

            points[pointIndexA] = m_launcher.transform.position;
            points[pointIndexE] = m_target.transform.position;
            points[pointIndexC] = GetCenter(points[pointIndexA], points[pointIndexE]);
            points[pointIndexB] = GetCenter(points[pointIndexA], points[pointIndexC]);
            points[pointIndexD] = GetCenter(points[pointIndexC], points[pointIndexE]);

            float distance = Vector3.Distance(m_launcher.transform.position, m_target.transform.position) / points.Length;
            mainTextureScale.x = distance;
            randomness = distance / (pointsCount * half);
            mainTextureOffset.x = Random.Range(-randomness, randomness);
            lRend.material.SetTextureScale(mainTexture, mainTextureScale);
            lRend.material.SetTextureOffset(mainTexture, mainTextureOffset);


            SetRandomness();

            lRend.SetPositions(points);

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

        Destroy(gameObject, 2.0f);
    }

    void Damage(GameObject m_target)
    {
        Enemy e = m_target.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(m_damage);
        }
    }

    private void SetRandomness()
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (i != pointIndexA && i != pointIndexE)
            {
                points[i].x += Random.Range(-randomness, randomness);
                points[i].y += Random.Range(-randomness, randomness);
                points[i].z += Random.Range(-randomness, randomness);
            }
        }
    }

    private Vector3 GetCenter(Vector3 a, Vector3 b)
    {
        return (a + b) / half;
    }

    public override void FireProjectile(GameObject launcher, Enemy target, int damage, float attackSpeed)
    {
        if (launcher)
        {
            m_launcher = launcher;
            transform.SetParent(m_launcher.transform);

            m_target = target;
            m_damage = damage;
            m_attackSpeed = attackSpeed;
            m_attackTimer = 0.0f;
        }
    }
}
