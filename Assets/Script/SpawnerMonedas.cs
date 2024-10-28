using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMonedas : MonoBehaviour
{
    public GameObject objeto;
    void Start()
    {
        int probabilidad = Random.Range(1, 6);
        if(probabilidad < 4 && probabilidad > 0 )
        {
            Instantiate( objeto, transform.position, transform.rotation );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
