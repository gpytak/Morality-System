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
    private PlayerMovement playerMovement;

    // MovePoint References
    public Transform movePoint;
    private bool atPoint;
    
    // Player Animation References
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // Find the Player Transform
        playerObject = GameObject.Find("Player").GetComponent<Transform>();
        playerObject.position = new Vector3(0, -1, 0);

        // Find the PlayerMovement script
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerMovement.enabled = false;

        // Find the Player Animator
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Determines when playerCopy reaches the point
        if(!atPoint && playerObject.position.x < 11)
        {
            playerObject.position = Vector2.MoveTowards(playerObject.position, movePoint.position, 5 * Time.deltaTime); // Position of player, position of move point, player movement speed
            playerAnimator.Play("hat-man-walk");
        }
        else
        {
            atPoint = true;
            playerAnimator.Play("hat-man-idle");
        }
    }
}
