using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject gamePosition;
    public GameObject homePosition;
    public GameObject player;

    public void TeleportToGame()
    {
        Vector3 newPosition = player.transform.position;
        newPosition.x = gamePosition.transform.position.x;
        newPosition.z = gamePosition.transform.position.z;

        Quaternion newRotation = gamePosition.transform.rotation;

        player.transform.position = newPosition;
        player.transform.rotation = newRotation;

    }

    public void TeleportHome()
    {
        Vector3 newPosition = player.transform.position;
        newPosition.x = homePosition.transform.position.x;
        newPosition.z = homePosition.transform.position.z;

        Quaternion newRotation = homePosition.transform.rotation;

        player.transform.position = newPosition;
        player.transform.rotation = newRotation;
    }
}
