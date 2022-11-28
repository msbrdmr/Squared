using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText2;
    public static int score = 0;
    
    private void Update()
    {
        scoreText.text = "Score: " + score + "";
        scoreText2.text = "Score: " + score + "";
    }

    public void incrementScore(int size)
    {
        score += size * 1200;
    }

    public void restartgame()
    {
        score = 0;
        Cube.currentnumber=Cube.defaultnumber;

        SceneManager.LoadScene(1);


    }

    public void loadmenu()
    {
        score = 0;
        Cube.currentnumber=Cube.defaultnumber;
        SceneManager.LoadScene(0);

    }

}
