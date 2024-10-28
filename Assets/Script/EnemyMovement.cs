using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1.1f; // Velocidad del enemigo
    public float distance = 5.0f; // Distancia que el enemigo recorre en cada dirección
    private Vector3 startPosition;
    private int direction = 1; // 1 para derecha, -1 para izquierda
    Transform enemy_tr;

    private void Start()
    {
        enemy_tr = GetComponent<Transform>();
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calcula la nueva posición
        Vector3 newPosition = transform.position + Vector3.right * speed * direction * Time.deltaTime;
        // Si se excede la distancia en la dirección actual, cambia la dirección
        if (Mathf.Abs(newPosition.x - startPosition.x) > distance)
        {
            direction *= -1;
        }
        if (direction ==1) { enemy_tr.rotation = new Quaternion(0, 180f, 0, 0); } else
        {
            enemy_tr.rotation = new Quaternion(0, 0f, 0, 0);
        }

        // Actualiza la posición del enemigo
        transform.position = newPosition;
    }
}

