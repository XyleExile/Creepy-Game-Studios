using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{   
    public GameObject gameOverUI;
    public GameObject winUI;

    public GameObject tempDeathUI;
    public PlayerMove playerMove;
    public PlayerHealth playerHealth;

    public Vector3 checkpoint;
    public float playerPreviousHealth;

    public SharkController sharkController;

    public GlassController glassController;
    public GemController gemController;
    public LadderScript ladder;

    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
        winUI.SetActive(false);
        tempDeathUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
        playerMove.enabled = false;
    }

    public void winGame()
    {
        winUI.SetActive(true);
        playerMove.enabled = false;
    }

    public void tempDeathScreen()
    {
        checkpoint = playerHealth.currentCheckpoint.transform.position;
        playerPreviousHealth = playerHealth.previousHealth;
        tempDeathUI.SetActive(true);
        playerMove.enabled = false;
    }

    public void mainMenu() 
    {
        SceneManager.LoadSceneAsync("StartScene");
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restart");
    }
    
    public void checkPointRestart()
    {   
        playerHealth.checkpointPosition = checkpoint;
        playerHealth.previousHealth = playerPreviousHealth; 
        sharkController.resetPosition();
        glassController.resetGlass();
        gemController.resetGem();
        ladder.resetLadder();
        playerHealth.respawn();
        tempDeathUI.SetActive(false);
        playerMove.enabled = true;
        Debug.Log("Restart");
    }

    public void QuitButton() 
    {
        Application.Quit();
    }
}
