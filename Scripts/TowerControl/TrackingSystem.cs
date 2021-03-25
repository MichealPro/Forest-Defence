using UnityEngine;
using System.Collections;

public class TrackingSystem : MonoBehaviour
{
    public float speed = 3.0f;
    //public Transform partToRotate;

    Enemy m_target = null;

    Vector3 m_lastKnownPosition = Vector3.zero;
    Quaternion m_lookAtRotation;


    void Update()
    {
        if (m_target)
        {
            if (m_lastKnownPosition != m_target.transform.position)
            {
                m_lastKnownPosition = m_target.transform.position;
                m_lookAtRotation = Quaternion.LookRotation(m_lastKnownPosition - transform.position);
            }

            if (transform.rotation != m_lookAtRotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, m_lookAtRotation, speed * Time.deltaTime);
            }
        }
    }

    public void SetTarget(Enemy target)
    {
        m_target = target;
    }
}