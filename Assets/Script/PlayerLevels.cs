using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class PlayerLevels: MonoBehaviour
{
    public float velocidad = 2f;
    public float velocidadCorrer = 3.5f;
    public float salto = 5f;
    bool tocandoSuelo = true;
    bool[] ActiveNv = new bool[4] {false, false, false, false};

    Rigidbody2D player_rb;
    Transform player_tr;
    Animator player_anim;

    private GameObject objetoEmpty;
    public TextMesh Rc;
    public TextMesh Rt;
    void Start()
    {
        player_rb = GetComponent<Rigidbody2D>();
        player_tr = GetComponent<Transform>();
        player_anim = GetComponent<Animator>();

        Rc = GameObject.FindGameObjectWithTag("Rcerezas").GetComponent<TextMesh>();
        Rt = GameObject.FindWithTag("Rtiempo").GetComponent<TextMesh>();

        if (PlayerPrefs.GetInt("MonedasMaximas") != 0)
        {
            Rc.text = PlayerPrefs.GetInt("MonedasMaximas").ToString();
            Rt.text = PlayerPrefs.GetInt("TiempoMaximo").ToString();
        }

    }

    void Update()
    {
        Move();
        LoadScene();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            tocandoSuelo = true;
            player_anim.SetBool("jump", false);
            player_anim.SetBool("hurt", false);
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.X)) { velocidad = velocidadCorrer; } else { velocidad = 2f; }

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
        if (Input.GetKey(KeyCode.M))
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "flappymode")
        {
            ActiveNv[0] = true;
        }
        else { ActiveNv[0] = false; }
        if (collision.gameObject.tag == "level1")
        {
            ActiveNv[1] = true;
        }
        else { ActiveNv[1] = false; }
        if (collision.gameObject.tag == "level2")
        {
            ActiveNv[2] = true;
        }
        else { ActiveNv[2] = false; }
        if (collision.gameObject.tag == "level3")
        {
            ActiveNv[3] = true;
        }
        else { ActiveNv[3] = false; }
    }

    private void LoadScene()
    {
        if (ActiveNv[0] && Input.GetKey(KeyCode.UpArrow))
        {
            SceneManager.LoadScene("Infinite", LoadSceneMode.Single);
        }
        if (ActiveNv[1] && Input.GetKey(KeyCode.UpArrow))
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        }
        if (ActiveNv[2] && Input.GetKey(KeyCode.UpArrow))
        {
            SceneManager.LoadScene("Level2", LoadSceneMode.Single);
        }
        if (ActiveNv[3] && Input.GetKey(KeyCode.UpArrow))
        {
            SceneManager.LoadScene("Level3", LoadSceneMode.Single);
        }
    }

}
