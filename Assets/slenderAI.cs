using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SlenderAI : MonoBehaviour
{
    //variables for Teleport IEnumerator

    //teleportPositions is a list of the different position within the workspace
    public Transform[] teleportPositions;
    //A quick transform variable for the player to ensure that whenever slender teleports he's looking at the player
    public Transform player;
    //Time between teleports
    public float teleportRate = 10.0f;
    
    //Was made for further progression of the code, basically just that whenever youre not looking at slender he would tp to another place
    public bool canTeleport = true;

    void Start()
    {
        StartCoroutine(Teleport());
    }

    IEnumerator Teleport()
    {
        while (canTeleport == true)
        {
            //this referes to whatever the script is placed on, in this instance it's the slender model, this is just a function that turns the model towards the player
            this.transform.LookAt(new Vector3(player.position.x, this.transform.position.y, player.position.z));

            yield return new WaitForSeconds(teleportRate);

            //randomizes which location slender teleports to
            int randomIndex = Random.Range(0, teleportPositions.Length);
            Transform randomPosition = teleportPositions[randomIndex];

            transform.position = randomPosition.position;
        }
    }
}
