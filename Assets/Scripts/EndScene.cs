using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
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

    // MovePoint References
    public Transform movePoint;
    private bool atPoint;
    
    // Player Animation References
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // Find the Player Transform
        // playerObject = GameObject.Find("Player").GetComponent<Transform>();
        // playerObject.position = new Vector3(-3, -1, 0);

        // Find the PlayerMovement script
        // playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        // playerMovement.enabled = false;

        // Find the Player script
        // newPlayer = GameObject.Find("Player").GetComponent<Player>();

        // Find the Player Animator
        playerAnimator = GameObject.Find("PlayerCopy").GetComponent<Animator>();

        // Setup the UI and button
        // if (newPlayer.headline != "Headline")
        //     bodyText.text = newPlayer.reputationTitle + " " + newPlayer.headline;
        // else
        //     bodyText.text = newPlayer.reputationTitle + " recently seen in village.";
        
        newsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Determines which point to move the player
        if(atPoint)
        {
                // newPlayer.headline = "Headline";
                // playerMovement.enabled = true;
                // playerCopy.SetActive(false);
        } else
            playerCopyObject.position = Vector2.MoveTowards(playerCopyObject.position, movePoint.position, 5 * Time.deltaTime); // Position of player, position of move point, player movement speed

        // Display the NewsPanel once the player is at the first move point
        if (playerCopyObject.position.x > 11 && !atPoint)
        {
            atPoint = true;
            newsPanel.SetActive(true);
            playerAnimator.Play("hat-man-idle");
        }
    }
}
