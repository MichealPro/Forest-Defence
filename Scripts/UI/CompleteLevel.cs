using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{

    public int nextScene;

    public void BigMap()
    {
        if (nextScene == 2)
        {
            GlobalControl.Instance.level1Pass = true;
        }
        if (nextScene == 3)
        {
            GlobalControl.Instance.level2Pass = true;
        }
        if (nextScene == 4)
        {
            GlobalControl.Instance.level3Pass = true;
        }
        SceneManager.LoadScene(4);
    }
}
