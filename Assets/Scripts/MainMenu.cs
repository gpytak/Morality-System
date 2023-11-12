using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // UI Reference
    public GameObject titleCanvas;
    public GameObject settingsCanvas;

    // Button References
    [SerializeField]
    private GameObject[] menuButton; // Array which holds the buttons inputed in the DialogueManger game object

    // Start is called before the first frame update
    void Start()
    {
        settingsCanvas.SetActive(false);
    }
    
    public void Option(int optionNum)
    {
        foreach (GameObject button in menuButton)
        {
            button.SetActive(false);
        }
        if(optionNum == 0)
        {
            titleCanvas.SetActive(false);
            settingsCanvas.SetActive(true);
            menuButton[0].SetActive(false);
            menuButton[1].SetActive(true);
        }
        if(optionNum == 1)
        {
            titleCanvas.SetActive(true);
            settingsCanvas.SetActive(false);
            menuButton[0].SetActive(true);
            menuButton[1].SetActive(false);
        }
    }
}
