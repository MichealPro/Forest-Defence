using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFade : MonoBehaviour
{
    //public Animator anim;
    //public Image black;
    IEnumerator Fading( )
    {
        GetComponent<Animator>().SetBool("Fade", true);
        yield return new WaitUntil(() => GetComponent<Image>().color.a == 1);
        SceneManager.LoadScene(0);
    }
    public void ChangeScene( )
    {
        StartCoroutine(Fading());
    }
}
