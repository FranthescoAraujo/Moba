using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potion : MonoBehaviour
{
    public int hp = 50;
    public float tempoRecuperar = 0;
    public bool podeRecuperar = true;
    public GameObject Pocao;
    public GameObject Personagem;

    private void Start()
    {
        Personagem = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        TempoRecuperar();
        Colisao();
    }

    void Colisao()
    {
        if (Vector3.Distance(transform.position, Personagem.transform.position) <= 1 && podeRecuperar)
        {
            Pocao.GetComponent<MeshRenderer>().enabled = false;
            podeRecuperar = false;
            if (Personagem.GetComponent<personagem>().hpAtual + hp >= Personagem.GetComponent<personagem>().hpTotal[Personagem.GetComponent<personagem>().level - 1])
            {
                Personagem.GetComponent<personagem>().hpAtual = Personagem.GetComponent<personagem>().hpTotal[Personagem.GetComponent<personagem>().level - 1];
                return;
            }
            Personagem.GetComponent<personagem>().hpAtual += hp;
        }
    }

    void TempoRecuperar()
    {
        if (!Pocao.GetComponent<MeshRenderer>().enabled)
        {
            tempoRecuperar += Time.deltaTime;
            if (tempoRecuperar >= 30f)
            {
                tempoRecuperar = 0;
                podeRecuperar = true;
                Pocao.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
