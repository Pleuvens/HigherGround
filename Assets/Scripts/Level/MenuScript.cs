using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public Text highscore;

    void Start()
    {
        int score = PlayerPrefs.GetInt("highscore", 0);
        highscore.text += score.ToString();
        PlayerPrefs.DeleteAll();
    }

	public void Play()
    {
        SceneManager.LoadScene("Level");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
