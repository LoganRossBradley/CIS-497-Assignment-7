using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;
    public bool won = false;

    public Text txtGameOver;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        txtGameOver.text = "Get to wave 10 to win \n If you fall off you lose! \n press space to continue";
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver && won)
        {
            txtGameOver.text = "You win! Press 'R' to restart";
        }
        else if (gameOver && !won)
        {
            txtGameOver.text = "You lose! Press 'R' to restart";
        }

        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            txtGameOver.text = "";
        }
    }
}
