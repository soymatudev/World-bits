using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearLs : MonoBehaviour
{
    public float salto = 7.5f;
    public float speed = 1f;
    public float distance = 5.0f;
    private Vector3 startPosition;
    private int direction = -1; 
    Transform enemy_tr;

    private float tiempoDesdeUltimoSalto = 0f;
    public float intervaloDeSalto = 3f; // Intervalo de tiempo deseado entre saltos


    private void Start()
    {
        enemy_tr = GetComponent<Transform>();
        startPosition = transform.position;
    }

    private void Update()
    {

        // Actualiza el tiempo desde el último salto
        tiempoDesdeUltimoSalto += Time.deltaTime;

        // Si ha pasado el intervalo de salto deseado, haz que el enemigo salte
        if (tiempoDesdeUltimoSalto >= intervaloDeSalto)
        {
            Saltar();
        }

        // Calcula la nueva posición
        Vector3 newPosition = transform.position + Vector3.right * speed * direction * Time.deltaTime;
        
        // Si se excede la distancia en la dirección actual, cambia la dirección
        if (Mathf.Abs(newPosition.x - startPosition.x) > distance)
        {
            direction *= -1;
        }
        if (direction == 1) { enemy_tr.rotation = new Quaternion(0, 0f, 0, 0); }
        else
        {
            enemy_tr.rotation = new Quaternion(0, 180f, 0, 0);
        }

        // Actualiza la posición del enemigo
        transform.position = newPosition;

    }


    private void Saltar()
    {
        // Reinicia el tiempo desde el último salto
        tiempoDesdeUltimoSalto = 0f;

         Rigidbody2D rb = GetComponent<Rigidbody2D>();
         rb.AddForce(Vector2.up * salto, ForceMode2D.Impulse);
    }
}
