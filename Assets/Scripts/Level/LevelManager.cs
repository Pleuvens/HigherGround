using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public GameObject player;
    public Text scoreText;
    public int score;
    public Button retry;
    public Button menu;
    public GameObject[] basket;
    public int currentBasket;
    public int prevBasket;

	// Use this for initialization
	void Start () {
        score = 0;
        retry.gameObject.SetActive(false);
        menu.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        int roundedheight = Mathf.RoundToInt(player.transform.position.y);
        if (roundedheight > score)
        {
            score = roundedheight;
            scoreText.text = score.ToString();
        }
        if (player.GetComponent<PlayerScript>().isDead)
        {
            int hscore = PlayerPrefs.GetInt("highscore", 0);
            if (hscore < score)
                PlayerPrefs.SetInt("highscore", score);
            retry.gameObject.SetActive(true);
            menu.gameObject.SetActive(true);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void UpdateMap()
    {
        //basket[prevBasket].SetActive(true);
        float width = this.GetComponent<RectTransform>().rect.width * this.GetComponent<RectTransform>().localScale.x;
        float cury = basket[currentBasket].transform.position.y;
        float curx = basket[prevBasket].transform.position.x;
        float posx = curx;
        float posy = Random.Range(cury + 2.5f, cury + 3.5f);

        basket[prevBasket].transform.position = new Vector3(posx, posy, basket[prevBasket].transform.position.z);

        int tmp = prevBasket;
        prevBasket = currentBasket;
        currentBasket = tmp;
    }
}
