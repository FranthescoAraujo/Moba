using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class ataqueHeroi : MonoBehaviour
{
    public GameObject Personagem;
    public personagem statusPersonagem;
    public Animator Animador;
    public float tempoAtaque = 0;
    public bool podeAtacar = true;
    public movimento mov;
    public bool podeOlhar = true;

    void Start()
    {
        Personagem = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Atacar();
        TempoAtaque();
    }

    void Atacar()
    {
        if (Input.GetMouseButtonDown(0) && podeAtacar && !(statusPersonagem.hpAtual == 0))
        {
            Olhar();
            Animador.SetTrigger("isAtacando");
            RaycastHit hit;
            mov.Corpo.destination = transform.position;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5) && (hit.collider.gameObject.tag == "Inimigo"))
            {
                hit.collider.gameObject.GetComponent<inimigo>().hpAtual -= statusPersonagem.dano[statusPersonagem.level - 1];
                podeAtacar = false;
            }
        }
    }

    void Olhar()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100) && !Animador.GetCurrentAnimatorStateInfo(0).IsName("arthur_attack_01"))
        {
            Personagem.transform.LookAt(hit.point);
        }
    }

    void TempoAtaque()
    {
        tempoAtaque += Time.deltaTime;
        if (tempoAtaque >= 2f)
        {
            tempoAtaque = 0;
            podeAtacar = true;
        }
    }
}
