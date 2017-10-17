using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameControl : MonoBehaviour
{
    private bool pauseIsActive = false;

    public void OnBackMainClick()
    {
        SceneManager.LoadScene(0);
        GameManagerScript.Instance.SaveResultToFile();
        SoundManager.instance.BGM.Stop();
    }
    public void PauseEnter()
    {
        if (!pauseIsActive)
        {
            Time.timeScale = 0;
            pauseIsActive = true;
            SoundManager.instance.BGM.Pause();
        }
        else
        {
            Time.timeScale = 1;
            pauseIsActive = false;
            SoundManager.instance.BGM.Play();
        }
    }
}
