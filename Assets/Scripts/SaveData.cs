using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveData : MonoBehaviour
{
    public Text bestScore;
    public Text inputField;

    private void Start()
    {
        bestScore.text = "Best Score has " + PlayerPrefs.GetString("recordName") + " whith : " + PlayerPrefs.GetInt("record") + " points";
    }
    public void StartGame() 
    {
        inputField.text = PlayerPrefs.GetString("name");
        EnterName();
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        //MainManager.Instance.SaveColor();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
    public void EnterName()
    {
        string name;
        if (inputField.text != "" && inputField.text != null)
        {
            name = inputField.text;
            PlayerPrefs.SetString("name", name);
        }
        else
        {
            name = "Player";
        }
        
    }
}
