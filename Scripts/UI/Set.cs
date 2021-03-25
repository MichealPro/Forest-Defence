using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Set : MonoBehaviour
{
    public AudioSource buttonSound;
    public AudioMixer audioMixer;
    public Panel p;

    public void ClickSound()
    {
        buttonSound.Play();
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("playing", volume);
    }
    public void Back()
    {
        //uiFade.UI_FadeOut_Event();
        gameObject.SetActive(false);
        Time.timeScale = p.t;
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        WaveSpawner.EnemiesAlive = 0;
        Time.timeScale = 1;
    }
}
