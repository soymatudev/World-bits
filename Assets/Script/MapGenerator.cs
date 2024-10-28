using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject map;
    public float espacioMap = 70.4698f;
    public bool generate = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!generate)
            {
                Instantiate(map, new Vector3(transform.position.x + espacioMap, 8.443801f, 1.838953f), transform.rotation);
                generate = true;
            }
        }   
    }

}
