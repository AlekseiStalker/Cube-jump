using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;
    public List<GameObject> platform;

    private int point = 0;
    public string playerColor = "PlayerRed";
    public Text LevelText;

    public Sprite NoneSprite;
    public Sprite[] NumberSprite;
    public Image[] NumbersImage;
    public Material[] arrColor;

    private void Awake()
    {
        //Debug.Log(Application.persistentDataPath);
        Instance = this;
        LevelText.text = "Level " + 1; Invoke("ClearLevelText", 2f);
        ActivatePlayer();
    }
    public void UpdateLevelText(int numLevel)
    {
        LevelText.text = numLevel < 8 ? "Level " + numLevel : "";
        Invoke("ClearLevelText", 2f);
    }
    void ClearLevelText()
    {
        LevelText.text = "";
    }

    void ActivatePlayer()
    {
        string playerBaseName = PlayerPrefs.GetString("PlayerBaseName", "PlayerRed");
        playerColor = playerBaseName;

        //Debug.Log("playerBaseName " + playerBaseName);
        //GameObject playerLoad = Resources.Load<GameObject>("Players/" + playerBaseName);
    }
    public void AddPoint()
    {
        point++;
        UpdatePointPanel();
        SavePoints();
    }
    private void UpdatePointPanel()
    {
        string pointStr = point.ToString();
        int numbersCountMax = NumbersImage.Length;
        if (pointStr.Length > numbersCountMax)
        {
            pointStr = pointStr.Substring(pointStr.Length - NumbersImage.Length,numbersCountMax);
        }
        for (int i = 0; i < numbersCountMax; i++)
        {
            if (i<pointStr.Length)
            {
                NumbersImage[i].sprite = NumberSprite[pointStr[i] - '0'];
            }
            else
            {
                NumbersImage[i].sprite = NoneSprite;
            }
        }
    }
    
    public void SavePoints()
    {
        PlayerPrefs.SetInt("Points", point + PlayerPrefs.GetInt("Points", 0));
    }
    public void SaveResultToFile()
    {
        List<string> loadFile = new List<string>(File.ReadAllLines(Application.persistentDataPath + "/score.txt"));
        bool isAdd = false;
        for (int i = 0; i < loadFile.Count; i++)
        {
            string[] playerInfo = loadFile[i].Split(new char []{ ' ' });
            if (point > int.Parse(playerInfo[1]))
            {
                loadFile.Insert(i, PlayerPrefs.GetString("PlayerName", "NoName") + ' ' + point);
                isAdd = true;
                break;
            }
        }
        if (!isAdd)
        {
            loadFile.Add(PlayerPrefs.GetString("PlayerName", "NoName") + ' ' + point);
        }
        if (loadFile.Count > 20)
        {
            loadFile.RemoveAt(20);
        }
        File.WriteAllLines(Application.persistentDataPath + "/score.txt", loadFile.ToArray());
    }

    public void YouLose()
    {
        SceneManager.LoadScene(1);
        SaveResultToFile();
    }
}
