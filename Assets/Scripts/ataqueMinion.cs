using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ataqueMinion : MonoBehaviour
{
    public NavMeshAgent Agente;
    public GameObject Personagem;
    public Animator Animador;
    public GameObject NexusAliado;
    public inimigo statusMinion;
    public float tempoAtaque = 0;
    public bool podeAtacar = true;
    public float tempoMorrer = 0;

    void Start()
    {
        Agente = GetComponent<NavMeshAgent>();
        Personagem = GameObject.FindGameObjectWithTag("Player");
        NexusAliado = GameObject.FindGameObjectWithTag("NexusAliado");
        statusMinion = GetComponent<inimigo>();
    }

    void Update()
    {
        Mover();
        Ataque();
        TempoAtaque();
        Morrer();
        DestruirNexus();
    }

    void Mover()
    {
        if (!Animador.GetCurrentAnimatorStateInfo(0).IsName("Basic Attack") && statusMinion.hpAtual > 0)
        {
            if (Vector3.Distance(transform.position, Personagem.transform.position) <= 3 && !(Personagem.GetComponent<personagem>().hpAtual == 0))
            {
                Agente.destination = transform.position;
                Animador.SetBool("isCorrendo", false);
                Animador.SetBool("isAndando", false);
                return;
            }
            if (Vector3.Distance(transform.position, Personagem.transform.position) <= 8 && !(Personagem.GetComponent<personagem>().hpAtual == 0))
            {
                Agente.speed = 2f;
                Animador.SetBool("isCorrendo", false);
                Animador.SetBool("isAndando", true);
                Agente.destination = Personagem.transform.position;
                return;
            }
            if (Vector3.Distance(transform.position, Personagem.transform.position) <= 20 && !(Personagem.GetComponent<personagem>().hpAtual == 0))
            {
                Agente.speed = 3.5f;
                Animador.SetBool("isCorrendo", true);
                Animador.SetBool("isAndando", false);
                Agente.destination = Personagem.transform.position;
                return;
            }
            Agente.destination = NexusAliado.transform.position;
            Animador.SetBool("isCorrendo", false);
            Animador.SetBool("isAndando", true);
        }
    }

    void Ataque()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 3) && podeAtacar && statusMinion.hpAtual > 0 && hit.collider.gameObject.tag == "Player")
        {
            Agente.destination = transform.position;
            Animador.SetTrigger("isAtacando");
            hit.collider.gameObject.GetComponent<personagem>().hpAtual -= statusMinion.dano;
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
        if (statusMinion.hpAtual <= 0)
        {
            Agente.destination = transform.position;
            Animador.SetTrigger("isMorto");
            tempoMorrer += Time.deltaTime;
            if (tempoMorrer >= 2f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void DestruirNexus()
    {
        if (Vector3.Distance(transform.position, NexusAliado.transform.position) < 10)
        {
            NexusAliado.GetComponent<inimigo>().hpAtual -= 5;
            Destroy(this.gameObject);
        }
    }
}
