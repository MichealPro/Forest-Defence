using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public Image black;

    public Animator anim;
    public Animator anim1;
    public Animator anim2;

    public AudioSource buttonSound;
    public AudioSource bgm;

    public Button g2;
    public Button g3;

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        if (GlobalControl.Instance.level1Pass)
        {
            LoadLevel(anim1, g2);
        }
        if (GlobalControl.Instance.level2Pass)
        {
            LoadLevel(anim2, g3);
        }
        if (GlobalControl.Instance.level3Pass)
        {
            bgm.Play();
            gameObject.SetActive(false);
        }
    }
    public void ClickSound()
    {
        buttonSound.Play();
    }
    IEnumerator Fading(int i)
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(i);
    }
    public void ChangeScene(int l)
    {
        //uiFade.UI_FadeOut_Event();
        StartCoroutine(Fading(l));
    }
    IEnumerator Loading(Animator a,Button l)
    {
        a.SetTrigger("pass");
        yield return new WaitForSeconds(0.5f);
        l.interactable = true;
    }
    public void LoadLevel(Animator curAnim, Button nextLevel)
    {
        StartCoroutine(Loading(curAnim, nextLevel));
    }
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
