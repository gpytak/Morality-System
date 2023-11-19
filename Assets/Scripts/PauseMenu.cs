using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused; // Checks if the game is paused

    // Panel reference
    public GameObject pausePanel;

    // Button References
    public GameObject[] pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        
        pauseButton[0].SetActive(false);
        pauseButton[1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        isPaused = true;

        pauseButton[0].SetActive(true);
        pauseButton[1].SetActive(true);
        pauseButton[0].GetComponent<Button>().Select();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        isPaused = false;
    }

    public void Option(int optionNum)
    {
        foreach (GameObject button in pauseButton)
        {
            button.SetActive(false);
        }
        if(optionNum == 0)
        {
            ResumeGame();
        }
        if(optionNum == 1)
        {
            Application.Quit();
        }
    }
}
