using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }
    void Update()
    {
        if (GlobalControl.Instance.tipsPanel == false)
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    public void Skip()
    {
        Time.timeScale = 1;
        GlobalControl.Instance.tipsPanel = false;
    }
}
