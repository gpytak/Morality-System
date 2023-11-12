using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Headline : MonoBehaviour
{
    // Player Reference
    private Player newPlayer;

    // Player References
    private Transform playerObject;
    public Transform playerCopyObject;
    public GameObject playerCopy;
    private PlayerMovement playerMovement;

    // UI Reference
    public TMP_Text bodyText; // Body text of headline
    public GameObject newsPanel;

    // Button References
    [SerializeField]
    public GameObject NewsButton;

    private float buttonTimer = 6.0f; // Timer before button appears

    // MovePoint References
    public Transform[] movePoint;
    private bool nextPoint;
    

    // Start is called before the first frame update
    void Start()
    {
        // Find the Player Transform
        playerObject = GameObject.Find("Player").GetComponent<Transform>();
        playerObject.position = new Vector3(-3, -1, 0);

        // Find the PlayerMovement script
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerMovement.enabled = false;

        // Find the Player script
        newPlayer = GameObject.Find("Player").GetComponent<Player>();

        // Setup the UI and button
        bodyText.text = newPlayer.reputationTitle + " " + newPlayer.headline;
        newsPanel.SetActive(false);
        NewsButton.SetActive(false);
        NewsButton.GetComponent<Button>().Select();
    }

    // Update is called once per frame
    void Update()
    {
        // Determines which point to move the player
        if(nextPoint)
        {
            // Move the player towards the move point
            playerCopyObject.position = Vector2.MoveTowards(playerCopyObject.position, movePoint[1].position, 5 * Time.deltaTime); // Position of player, position of move point, player movement speed

            if(playerCopyObject.position.x > 24)
            {
                playerMovement.enabled = true;
                playerCopy.SetActive(false);
                SceneManager.LoadScene("VillageScene");
            }
        } else
            playerCopyObject.position = Vector2.MoveTowards(playerCopyObject.position, movePoint[0].position, 5 * Time.deltaTime); // Position of player, position of move point, player movement speed

        // Display the NewsPanel once the player is at the first move point
        if (playerCopyObject.position.x > 12 && !nextPoint)
            newsPanel.SetActive(true);

        // Update buttonTimer
        if (buttonTimer < 0 && !nextPoint)
            NewsButton.SetActive(true);
        else
            buttonTimer -= Time.deltaTime;
    }

    public void Option(int optionNum)
    {
        if(optionNum == 0)
        {
            newsPanel.SetActive(false);
            NewsButton.SetActive(false);
            nextPoint = true;
        }
    }
}
