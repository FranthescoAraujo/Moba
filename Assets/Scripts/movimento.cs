using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movimento : MonoBehaviour
{
    public NavMeshAgent Corpo;
    public float DistanciaDestino;
    public Animator Animador;
    public personagem statusPersonagem;

    void Start()
    {
        Corpo = GetComponent<NavMeshAgent>();
        statusPersonagem = GetComponent<personagem>();
    }

    void Update()
    {
        Mover();
        Parar();
    }

    void Mover()
    {
        if (Input.GetMouseButtonDown(1) && !Animador.GetCurrentAnimatorStateInfo(0).IsName("arthur_attack_01") && !(statusPersonagem.hpAtual==0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                Corpo.destination = hit.point;
            }
        }
    }


    void Parar()
    {
        DistanciaDestino = Vector3.Distance(transform.position, Corpo.destination);
        if (DistanciaDestino < 2)
        {
            Corpo.destination = transform.position;
            Animador.SetBool("isAndando", false);
            return;
        } 
        Animador.SetBool("isAndando", true);
    }
}
    