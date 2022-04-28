using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public float tempo;
    public bool canSpawn = true;
    public GameObject Minion;
    public GameObject Particulas;
    public GameObject Particulas2;
    public bool pararSpawn = false;
    void Start()
    {
        Particulas.SetActive(false);
        Particulas2.SetActive(false);
    }

    void Update()
    {
        TempoSpawn();
        Spawn();
        Morrer();
    }

    void Spawn()
    {
        if (canSpawn && !pararSpawn)
        {
            GameObject minion = Instantiate(Minion, transform.position, Quaternion.identity);;
            canSpawn = false;
        }
    }

    void TempoSpawn()
    {
        tempo += Time.deltaTime;
        if (tempo >= 20f)
        {
            canSpawn = true;
            tempo = 0;
        }
    }

    void Morrer()
    {
        if (GetComponent<inimigo>().hpAtual == 0)
        {
            Particulas.SetActive(true);
            Particulas2.SetActive(true);
            pararSpawn = true;
        }
    }
}
