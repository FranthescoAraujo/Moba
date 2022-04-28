using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ataqueInimigo : MonoBehaviour
{
    public NavMeshAgent Agente;
    public GameObject Personagem;
    public Animator Animador;
    public GameObject NexusAliado;
    public inimigo statusInimigo;
    public float tempoAtaque = 0;
    public bool podeAtacar = true;
    public float tempoMorrer = 0;
    public float velocidade = 2f;

    void Start()
    {
        Agente = GetComponent<NavMeshAgent>();
        Personagem = GameObject.FindGameObjectWithTag("Player");
        NexusAliado = GameObject.FindGameObjectWithTag("NexusAliado");
        statusInimigo = GetComponent<inimigo>();
    }

    void Update()
    {
        Mover();
        Ataque();
        TempoAtaque();
        Morrer();
    }

    void Mover()
    {
        if (!Animador.GetCurrentAnimatorStateInfo(0).IsName("Basic Attack") && statusInimigo.hpAtual > 0)
        {
            if (Vector3.Distance(transform.position, Personagem.transform.position) <= 5 && !(Personagem.GetComponent<personagem>().hpAtual == 0))
            {
                Agente.destination = transform.position;
                Animador.SetBool("isCorrendo", false);
                Animador.SetBool("isDormindo", false);
                return;
            }
            if (Vector3.Distance(transform.position, Personagem.transform.position) <= 15 && !(Personagem.GetComponent<personagem>().hpAtual == 0))
            {
                Agente.speed = velocidade;
                Animador.SetBool("isCorrendo", true);
                Animador.SetBool("isDormindo", false);
                Agente.destination = Personagem.transform.position;
                return;
            }
            Agente.destination = transform.position;
            Animador.SetBool("isCorrendo", false);
            Animador.SetBool("isDormindo", true);
        }
    }

    void Ataque()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 5) && podeAtacar && statusInimigo.hpAtual > 0 && hit.collider.gameObject.tag == "Player")
        {
            Agente.destination = transform.position;
            Animador.SetTrigger("isAtacando");
            hit.collider.gameObject.GetComponent<personagem>().hpAtual -= statusInimigo.dano;
            if (hit.collider.gameObject.GetComponent<personagem>().hpAtual < 0)
            {
                hit.collider.gameObject.GetComponent<personagem>().hpAtual = 0;
            }
            podeAtacar = false;
        }
    }

    void TempoAtaque()
    {
        tempoAtaque += Time.deltaTime;
        if (tempoAtaque >= 3f)
        {
            podeAtacar = true;
            tempoAtaque = 0;
        }
    }

    void Morrer()
    {
        if (statusInimigo.hpAtual <= 0)
        {
            Agente.destination = transform.position;
            Animador.SetTrigger("isMorto");
            tempoMorrer += Time.deltaTime;
            if (tempoMorrer >= 3f)
            {
                Personagem.GetComponent<personagem>().level += 1;
                Personagem.GetComponent<personagem>().hpAtual = Personagem.GetComponent<personagem>().hpTotal[Personagem.GetComponent<personagem>().level - 1];
                Destroy(this.gameObject);
            }
        }
    }
}
