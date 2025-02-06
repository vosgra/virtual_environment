using UnityEngine;

public class NPCOnRaft : MonoBehaviour
{
    public GameObject raft; // The raft GameObject the NPC will follow
    public float standingHeight = 1.5f; // Height of the NPC when standing
    public float layingHeight = 0.5f; // Height of the NPC when laying
    private Vector3 raftOffset; // The offset between the NPC and the raft's position
    private bool isStanding = true; // Boolean to track whether the NPC is standing

    void Start()
    {
        // Calculate initial offset when NPC is standing
        raftOffset = transform.position - raft.transform.position;
    }

    void Update()
    {
        // Make the NPC follow the raft's movement
        FollowRaft();

        
    }

    void FollowRaft()
    {
        // Update the position to follow the raft with the initial offset
        transform.position = raft.transform.position + raftOffset;

        // Adjust NPC's height based on its state
        Vector3 currentPosition = transform.position;
        if (isStanding)
        {
            currentPosition.y = standingHeight; // Set NPC to standing height
        }
        else
        {
            currentPosition.y = layingHeight; // Set NPC to laying height
        }
        transform.position = currentPosition;
    }

}
