using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float velocidadEnemigo = 1f;
    public Transform player;
    Transform enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), velocidadEnemigo * Time.deltaTime);
        if (player.position.x > transform.position.x)
        {
            enemy.rotation = new Quaternion(0, 180f, 0, 0);
        }
        else
        {
            enemy.rotation = new Quaternion(0, 0f, 0, 0);
        }
    }
}
