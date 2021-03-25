using UnityEngine;
using System.Collections;

public abstract class BaseProjectile : MonoBehaviour
{
    public float speed = 5.0f;

    public abstract void FireProjectile(GameObject launcher, Enemy target, int damage, float attackSpeed);
}
