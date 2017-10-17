using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour //проверить можно ли повесить на телоепорт класс gamecontroll с методом OnTriggerEnter
{
    public static NextLevel Instanse;
    public int curPlatform;
    public int curLevel;
    int countPlatform = 0;
    
    public GameObject panelWinner;
    private void Awake()
    {
        Instanse = this;
        curPlatform = 1;
        curLevel = 1;
        //panelWinner = GameObject.Find("WinPanel");
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        ++curLevel;
        GameManagerScript.Instance.UpdateLevelText(curLevel);
        foreach (GameObject item in GameManagerScript.Instance.platform)
        {
            if (item.activeInHierarchy)
            {
                item.SetActive(false);
            }
        }
        CreateNextPlatform();
    }
    void CreateNextPlatform()
    {
        if (GameManagerScript.Instance.platform.Count - 1 == curPlatform)
        {
            panelWinner.SetActive(true);
            Time.timeScale = 0.1f;
            return;
        }
        for (; curPlatform < GameManagerScript.Instance.platform.Count - 1;)
        {
            ++curPlatform;
            GameManagerScript.Instance.platform[curPlatform].SetActive(true);

            ++countPlatform;
            if (countPlatform >= 2)
            {
                countPlatform = 0;
                break;
            }
        }
    }
}
