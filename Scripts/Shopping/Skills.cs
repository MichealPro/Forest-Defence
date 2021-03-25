using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    public SkillButton Fire;
    public SkillButton Ice;
    public SkillButton Thunder;

    public Button FireButton;
    public Button IceButton;
    public Button ThunderButton;

    public Text FireText;
    public Text IceText;
    public Text ThunderText;

    public string enemyTag = "Enemy";
    public GameObject FireEffect;
    public GameObject SnowEffect;
    public GameObject ThunderEffect;

    private GameObject[] enemies;

    private void Start()
    {
        InvokeRepeating("UpdateEnemies", 0f, 0.5f);

        FireText.text = "$" + Fire.skillCost;
        IceText.text = "$" + Ice.skillCost;
        ThunderText.text = "$" + Thunder.skillCost;

        FireButton.interactable = false;
        IceButton.interactable = false;
        ThunderButton.interactable = false;
    }
    private void Update()
    {
        if (enemies.Length<=0)
        {
            return;
        }
        else
        {
            if (PlayerStats.Money >= Fire.skillCost && !FireButton.interactable)
            {
                FireButton.interactable = true;
            }
            if (PlayerStats.Money >= Ice.skillCost && !IceButton.interactable)
            {
                IceButton.interactable = true;
            }
            if (PlayerStats.Money >= Thunder.skillCost && !ThunderButton.interactable)
            {
                ThunderButton.interactable = true;
            }
        }
    }

    private void UpdateEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    }

    public void FireSkill()
    {
        if (PlayerStats.Money >= Fire.skillCost)
        {
            PlayerStats.Money -= Fire.skillCost;
            foreach (GameObject enemy in enemies)
            {
                GameObject fire = Instantiate(FireEffect, enemy.transform.position, enemy.transform.rotation) as GameObject;
                Destroy(fire, 2f);
                enemy.GetComponent<Enemy>().TakeDamage(Fire.skillDamage);
            }

        }
        else
        {
            FireButton.interactable = false;
        }
    }

    public void IceSkill()
    {
        if (PlayerStats.Money >= Ice.skillCost)
        {
            PlayerStats.Money -= Ice.skillCost;
            StartCoroutine(SnowKill());
        }
        else
        {
            IceButton.interactable = false;
        }
    }
    IEnumerator SnowKill()
    {
        SnowEffect.SetActive(true);

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().Slow(Ice.slowPct);
        }

        yield return new WaitForSeconds(10f);

        SnowEffect.SetActive(false);

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().Slow(0);
        }

    }

    public void ThunderSkill()
    {
        if (PlayerStats.Money >= Thunder.skillCost)
        {
            PlayerStats.Money -= Thunder.skillCost;
            StartCoroutine(ThunderKill());
        }
        else
        {
            ThunderButton.interactable = false;
        }
    }

    IEnumerator ThunderKill()
    {
        ThunderEffect.SetActive(true);

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().Slow(Thunder.slowPct);
            enemy.GetComponent<Enemy>().TakeDamage(Thunder.skillDamage);
        }

        yield return new WaitForSeconds(10f);

        ThunderEffect.SetActive(false);

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().Slow(0);
        }

    }
}
