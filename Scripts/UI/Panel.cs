using UnityEngine;

public class Panel : MonoBehaviour
{
    public AudioSource buttonClick;
    public GameObject speedUp;
    public int t=1;

    public void ButtonClick()
    {
        buttonClick.Play();
    }
    public void SetSpeed()
    {
        if (Time.timeScale == 1)
        {
            t = 2;
            Time.timeScale = 2;
            speedUp.SetActive(true);
        }
        else
        {
            t = 1;
            Time.timeScale = 1;
            speedUp.SetActive(false);
        }
    }
    private void Inactive()
    {
        gameObject.SetActive(false);
        Time.timeScale = 0;
    }
    public void SettingPanel()
    {
        Invoke("Inactive", 0.5f);
    }
}
