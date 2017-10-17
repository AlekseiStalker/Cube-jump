using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainControl : MonoBehaviour
{
    public float speed;
    private float checkPos = 0;
    private RectTransform rectNameGame;

    public Transform LeaderGroup;
    public GameObject LeaderInfo;
    public InputField InputName;

    public Text PointsText;
    int points;
    int colorPrice;
    
    private void Awake()
    {
        points = PlayerPrefs.GetInt("Points", 0);
        UpdatePointText();
        UpdateLeaderBoard();

        Time.timeScale = 1;
        rectNameGame = GameObject.Find("GameName").GetComponent<RectTransform>();

        //PlayerPrefs.SetInt("Points", 0);// Вкл перед запуском, чтобы обнулить point
        //ElementPrice.ResetBougthElement(); //Вкл пепед запуском, чтобы не было купленных
    }
    private void Update()
    {
        if (rectNameGame.offsetMin.y != checkPos)
        {
            rectNameGame.offsetMin += new Vector2(rectNameGame.offsetMin.x, speed);
            rectNameGame.offsetMax += new Vector2(rectNameGame.offsetMax.x, speed);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
   
    public void OnExitClick()
    {
        Application.Quit();
    }

    void UpdateLeaderBoard()
    {
        foreach (Transform leaderInfo in LeaderGroup)
        {
            if (leaderInfo != LeaderInfo.transform)
            {
                Destroy(leaderInfo.gameObject);
            }
        }
        if (File.Exists(Application.persistentDataPath + "/score.txt"))
        {
            string[] fileInfo = File.ReadAllLines(Application.persistentDataPath + "/score.txt");
            for (int i = 0; i < fileInfo.Length; i++)
            {
                GameObject newLeaderInfo = Instantiate(LeaderInfo, LeaderGroup, false);
                newLeaderInfo.SetActive(true);

                Text[] textFields = newLeaderInfo.GetComponentsInChildren<Text>();
                textFields[0].text = (i + 1).ToString();

                string[] playerInfo = fileInfo[i].Split(new char[] { ' ' });
                textFields[1].text = playerInfo[0];
                textFields[2].text = playerInfo[1];
            }
        }
    } 

    public void OnPlayerBaseClick(string baseName)
    {
        if (PlayerPrefs.GetString(baseName) == "Bougth")
        {
            PlayerPrefs.SetString("PlayerBaseName", baseName);
        }
        else
        {
            colorPrice = ElementPrice.IdentifyColor(baseName);
            if (points >= colorPrice)
            {
                points -= colorPrice;
                PlayerPrefs.SetInt("Points", points);
                PlayerPrefs.SetString(baseName, "Bougth");
                PlayerPrefs.SetString("PlayerBaseName", baseName);
            }
        }
        UpdatePointText();
    }
    private void UpdatePointText()
    {
        PointsText.text = points.ToString();
    }
    public void UpdateName()
    {
        if (!string.IsNullOrEmpty(InputName.text))
        {
            PlayerPrefs.SetString("PlayerName", InputName.text);
        }
    }
    public void MusicOn()
    {
        if (SoundManager.instance != null)
            SoundManager.instance.musicIsActive = true;

    }
    public void MusicOff()
    {
        if (SoundManager.instance != null)
            SoundManager.instance.musicIsActive = false;
    }
}

public static class ElementPrice
{
    public const int RED = 10;
    public const int VIOLET = 20;
    public const int BLACK = 30;
    public const int BROWN = 30;
    public const int BLUE = 50;
    public const int YELLOW = 100;

    public static int IdentifyColor(string colorName)
    {
        switch (colorName)
        {
            case "PlayerBlack":
                return BLACK;
            case "PlayerViolet":
                return VIOLET;
            case "PlayerYellow":
                return YELLOW;
            case "PlayerBrown":
                return BROWN;
            case "PlayerRed":
                return RED;
            case "PlayerBlue":
                return BLUE;
            default:
                Debug.Log("Default!!!");
                return 0;
        }
    }
    public static void ResetBougthElement()
    {
        PlayerPrefs.SetString("PlayerRed", "");
        PlayerPrefs.SetString("PlayerBlack", "");
        PlayerPrefs.SetString("PlayerViolet", "");
        PlayerPrefs.SetString("PlayerBrown", "");
        PlayerPrefs.SetString("PlayerYellow", "");
        PlayerPrefs.SetString("PlayerBlue", "");
    }
}



//switch (baseName)
//{
//    case "PlayerBlack":
//        if (PlayerPrefs.GetString("PlayerBlack", "") != "")
//        {
//            PlayerPrefs.SetString("PlayerBaseName", "PlayerBlack");
//        }
//        else
//        {
//            if (points >= ElementPrice.BLACK)
//            {
//                points -= ElementPrice.BLACK;
//                PlayerPrefs.SetInt("Points", points);
//                PlayerPrefs.SetString("PlayerBlack", "Buyght");
//                PlayerPrefs.SetString("PlayerBaseName", "PlayerBlack");
//            }
//        }
//        break;
//    case "PlayerViolet":
//        if (PlayerPrefs.GetString("PlayerViolet", "") != "")
//        {
//            PlayerPrefs.SetString("PlayerBaseName", "PlayerViolet");
//        }
//        else
//        {
//            if (points >= ElementPrice.VIOLET)
//            {
//                points -= ElementPrice.VIOLET;
//                PlayerPrefs.SetInt("Points", points);
//                PlayerPrefs.SetString("PlayerViolet", "Buyght");
//                PlayerPrefs.SetString("PlayerBaseName", "PlayerViolet");
//            }
//        }
//        break;
//    case "PlayerBrown":
//        if (PlayerPrefs.GetString("PlayerBrown", "") != "")
//        {
//            PlayerPrefs.SetString("PlayerBaseName", "PlayerBrown");
//        }
//        else
//        {
//            if (points >= ElementPrice.BROWN)
//            {
//                points -= ElementPrice.BROWN;
//                PlayerPrefs.SetInt("Points", points);
//                PlayerPrefs.SetString("PlayerBrown", "Buyght");
//                PlayerPrefs.SetString("PlayerBaseName", "PlayerBrown");
//            }
//        }
//        break;
//    case "PlayerBlue":
//        if (PlayerPrefs.GetString("PlayerBlue", "") != "")
//        {
//            PlayerPrefs.SetString("PlayerBaseName", "PlayerBlue");
//        }
//        else
//        {
//            if (points >= ElementPrice.BLUE)
//            {
//                points -= ElementPrice.BLUE;
//                PlayerPrefs.SetInt("Points", points);
//                PlayerPrefs.SetString("PlayerBlue", "Buyght");
//                PlayerPrefs.SetString("PlayerBaseName", "PlayerBlue");
//            }
//        }
//        break;
//    case "PlayerYellow":
//        if (PlayerPrefs.GetString("PlayerYellow", "") != "")
//        {
//            PlayerPrefs.SetString("PlayerBaseName", "PlayerYellow");
//        }
//        else
//        {
//            if (points >= ElementPrice.YELLOW)
//            {
//                points -= ElementPrice.YELLOW;
//                PlayerPrefs.SetInt("Points", points);
//                PlayerPrefs.SetString("PlayerYellow", "Buyght");
//                PlayerPrefs.SetString("PlayerBaseName", "PlayerYellow");
//            }
//        }
//        break;
//    case "PlayerRed":
//        if (PlayerPrefs.GetString("PlayerRed", "") != "")
//        {
//            PlayerPrefs.SetString("PlayerBaseName", "PlayerRed");
//        }
//        else
//        {
//            if (points >= ElementPrice.RED)
//            {
//                points -= ElementPrice.RED;
//                PlayerPrefs.SetInt("Points", points);
//                PlayerPrefs.SetString("PlayerRed", "Buyght");
//                PlayerPrefs.SetString("PlayerBaseName", "PlayerRed");
//            }
//        }
//        break;
//    default:
//        Debug.Log("Default!");
//        break;
//}