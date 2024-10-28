using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using System.Linq;

public class EventosController : MonoBehaviour
{
    public float tiempoTotal = 40f;
    private SpriteRenderer[] spriteRenderers;
    private Color colorMañana = new Color(1f, 1f, 0.658f, 1f); // Mañana
    private Color colorDia = new Color(1f, 1f, 1f, 1f); // Día
    private Color colorTarde = new Color(1f, 0.658f, 0.961f, 1f); // Tarde
    private Color colorNoche = new Color(0.451f, 0.361f, 0.722f, 1f); // Noche

    private void Start()
    {
        spriteRenderers = GameObject.FindGameObjectsWithTag("back")
            .Select(go => go.GetComponent<SpriteRenderer>())
            .ToArray();
    }

    private void Update()
    {
        tiempoTotal += Time.deltaTime;
        float t = Mathf.PingPong(tiempoTotal / 120f, 1f); // Valor entre 0 y 1 para el ciclo

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            Color colorActual = Color.Lerp(colorMañana, colorDia, t);
            if (t > 0.33f && t <= 0.66f)
            {
                colorActual = Color.Lerp(colorDia, colorTarde, (t - 0.33f) / 0.33f);
            }
            else if (t > 0.66f)
            {
                colorActual = Color.Lerp(colorTarde, colorNoche, (t - 0.66f) / 0.34f);
            }

            spriteRenderer.color = colorActual;
        }
    }
}
