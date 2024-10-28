using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("PlayerLevels") == null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, player.position.y, -5f), 10f * Time.deltaTime);
        }

        if (GameObject.FindWithTag("Player") == null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, -5f), 10f * Time.deltaTime);
        }
    }
}
