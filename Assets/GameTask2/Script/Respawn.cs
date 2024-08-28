using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public float threshold = -5f;
    public Vector3 respawnPosition = new Vector3(0.0f, 1.63f, 0.0f); // Define respawn position
    public Quaternion respawnRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f); // Define respawn rotation

    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            transform.position = respawnPosition;
            transform.rotation = respawnRotation;
        }
    }
}