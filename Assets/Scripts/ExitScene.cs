using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitScene : MonoBehaviour
{
    // Initiates prompt
    private bool promptInitiated;

    // UI Reference
    public GameObject exitCanvas;
    public GameObject exitOptionsPanel;

    // Button References
    [SerializeField]
    private GameObject[] exitOptionButton; // Array which holds the buttons inputed in the DialogueManger game object

    // Player Freeze
    private PlayerMovement playerMovement;

    // Player Animation References
    private Animator playerAnimator;

    void Start()
    {
        // Find PlayerMovement script
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        // Find the Player Animator
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();

        exitCanvas.SetActive(false);
        exitOptionsPanel.SetActive(false);
        exitOptionButton[0].SetActive(false);
        exitOptionButton[1].SetActive(false);
    }

    // Called as long as a Collider2D is detected within its trigger range
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !promptInitiated)
        {
            exitCanvas.SetActive(true);
            exitOptionsPanel.SetActive(true);
            exitOptionButton[0].SetActive(true);
            exitOptionButton[1].SetActive(true);
            exitOptionButton[0].GetComponent<Button>().Select();
            promptInitiated = true;
            playerMovement.enabled = false;
            playerAnimator.Play("hat-man-idle");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            exitCanvas.SetActive(false);
            exitOptionsPanel.SetActive(false);
            promptInitiated = false;
        }
    }

    public void Option(int optionNum)
    {
        foreach (GameObject button in exitOptionButton)
        {
            button.SetActive(false);
        }
        if(optionNum == 0)
        {
            exitCanvas.SetActive(false);
            playerMovement.enabled = true;
        }
        if(optionNum == 1)
        {
            playerMovement.enabled = true;
            promptInitiated = false;
            SceneManager.LoadScene("TravelScene");
        }
    }
}
