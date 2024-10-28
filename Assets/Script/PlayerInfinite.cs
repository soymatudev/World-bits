using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using System.Net;

public class PlayerInfinite : MonoBehaviour
{
    public float velocidad = 3.5f;
    public float velocidadCorrer = 5f;
    public float salto = 6.5f;
    public int monedas = 0;
    public int vida = 3;
    public float time = 0.0f;
    bool tocandoSuelo = true;
    bool isHurt = false;
    float hurtDuration = 1.0f; // Tiempo en segundos para recuperarse del daño
    float hurtTimer = 0.0f;
    bool stair = false;

    public Vector3 checkpoint;
    public Vector2 originalPosition;

    public Transform camera_tr;

    Rigidbody2D player_rb;
    Transform player_tr;
    Animator player_anim;

    Text contadorVidas;

    float tiempoMaximo;
    float monedasMaximas;

    void Start()
    {
        transform.position = new Vector3(-0.7244456f, -2.08f, -2.0f);

        player_rb = GetComponent<Rigidbody2D>();
        player_tr = GetComponent<Transform>();
        player_anim = GetComponent<Animator>();

        contadorVidas = GameObject.FindWithTag("vida").GetComponent<Text>();
        contadorVidas.text = vida + "";
        checkpoint.z = -2.0f;

        tiempoMaximo = PlayerPrefs.GetFloat("TiempoMaximo", Mathf.Infinity);
        monedasMaximas = PlayerPrefs.GetFloat("MonedasMaximas", 0);
    }

    void Update()
    {
        Move();
        TextTime();
        PlayerHurt();
        Stair();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.X)) { velocidad = velocidadCorrer; } else { velocidad = 3.5f; }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            player_rb.velocity = new Vector2(velocidad, player_rb.velocity.y);
            player_tr.rotation = new Quaternion(0, 0f, 0, 0);
            player_anim.SetBool("run", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            player_rb.velocity = new Vector2(-velocidad, player_rb.velocity.y);
            player_tr.rotation = new Quaternion(0, 180f, 0, 0);
            player_anim.SetBool("run", true);
        }
        else
        {
            player_anim.SetBool("run", false);
        }

        if (Input.GetKey(KeyCode.C) && tocandoSuelo)
        {
            player_rb.velocity = new Vector2(player_rb.velocity.x, salto);
            tocandoSuelo = false;
            player_anim.SetBool("jump", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            tocandoSuelo = true;
            player_anim.SetBool("jump", false);
            player_anim.SetBool("hurt", false);
        }

        if (collision.gameObject.tag == "enemigo")
        {
            if (!isHurt)
            {
                vida--;
                contadorVidas.text = vida + "";
                if (vida <= 0)
                {
                    transform.position = checkpoint;
                    vida = 1;
                    contadorVidas.text = vida + "";
                }
                else
                {
                    isHurt = true;
                    player_anim.SetBool("hurt", true);
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Moneda")
        {
            monedas++;
            Destroy(collision.gameObject);
            Text contadorMonedas = GameObject.FindWithTag("ContadorMonedas").GetComponent<Text>();
            contadorMonedas.text = monedas + "";
        }

        if (collision.gameObject.tag == "heart")
        {
            vida++;
            Destroy(collision.gameObject);
            contadorVidas.text = vida + "";
        }

        if (collision.gameObject.tag == "enemigo" || collision.gameObject.tag == "deadzone")
        {
            if (collision.gameObject.tag == "deadzone")
            {
                vida = 0;
            }

            if (!isHurt)
            {
                vida--;
                contadorVidas.text = vida + "";
                if (vida <= 0)
                {
                    if (monedas >= PlayerPrefs.GetInt("MonedasMaximas"))
                    {
                        PlayerPrefs.SetInt("MonedasMaximas", monedas);
                        PlayerPrefs.SetInt("TiempoMaximo", ((int)time));
                    }
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("Levels", LoadSceneMode.Single);
                }
                else
                {
                    isHurt = true;
                    player_anim.SetBool("hurt", true);
                }
            }

        }
        if (collision.gameObject.tag == "stair")
        {
            stair = true;
        }
        else { stair = false; }
    }

    public void Stair()
    {
        if (stair && Input.GetKey(KeyCode.UpArrow))
        {
            player_rb.velocity = new Vector2(player_rb.velocity.x, velocidad);
            tocandoSuelo = false;
        }
    }

    private void PlayerHurt()
    {
        if (isHurt)
        {
            hurtTimer += Time.deltaTime;

            if (hurtTimer >= hurtDuration)
            {
                isHurt = false;
                hurtTimer = 0.0f;
                player_anim.SetBool("hurt", false);
            }
        }
    }
    private void TextTime()
    {
            time += Time.deltaTime;
            Text contadorTiempo = GameObject.FindWithTag("ContadorTiempo").GetComponent<Text>();
            contadorTiempo.text = ((int)time) + "";
    }
}
