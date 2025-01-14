using UnityEngine;

public class FollowNPC : MonoBehaviour
{
    public Transform npc; // The NPC (The Boss) the player will follow
    public float followDuration = 30f; // Duration the player will follow the NPC
    public float followSpeed = 3f; // How fast the player follows the NPC
    private bool isFollowing = false; // Whether the player is following the NPC
    private float followTimer = 0f; // Timer for the follow duration
    private Vector3 offset; // Initial offset between player and NPC
    private bool isNearNPC = false; // Whether the player is near the NPC
    private bool isKeyboardControlDisabled = false; // Whether player keyboard controls are disabled

    private PlayerMovement playerMovement; // Reference to the player movement script

    // Start is called before the first frame update
    void Start()
    {
        // Get the PlayerMovement script (if you have one to disable movement controls)
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player presses 'E' and is near the NPC, start following
        if (Input.GetKeyDown(KeyCode.E) && isNearNPC && !isFollowing)
        {
            StartFollowing();
        }

        // If the player is following, move towards the NPC and track the timer
        if (isFollowing)
        {
            followTimer += Time.deltaTime;

            // Disable player controls while following the NPC
            if (!isKeyboardControlDisabled)
            {
                DisablePlayerControls();
                isKeyboardControlDisabled = true;
            }

            // Move the player towards the NPC's position (smooth follow)
            transform.position = Vector3.MoveTowards(transform.position, npc.position + offset, followSpeed * Time.deltaTime);

            // Stop following after the specified duration
            if (followTimer >= followDuration)
            {
                StopFollowing();
            }
        }
    }

    // Start following the NPC
    void StartFollowing()
    {
        isFollowing = true;
        offset = transform.position - npc.position; // Calculate the initial offset between player and NPC
        followTimer = 0f;
    }

    // Stop following the NPC and restore player control
    void StopFollowing()
    {
        isFollowing = false;
        isKeyboardControlDisabled = false;
        EnablePlayerControls();
    }

    // Disable player controls (e.g., disable movement input)
    void DisablePlayerControls()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = false; // Disable player movement script
        }
    }

    // Re-enable player controls
    void EnablePlayerControls()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = true; // Re-enable player movement script
        }
    }

    // Detect if the player is near the NPC using a collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC")) // Ensure the NPC has the tag "NPC"
        {
            isNearNPC = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            isNearNPC = false;
        }
    }
}
