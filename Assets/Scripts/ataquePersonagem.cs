using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class ataquePersonagem : MonoBehaviour
{
    public personagem statusPersonagem;
    public Animator Animador;
    public PostProcessVolume postProcessing;
    public ColorGrading GrayScale;
    public float tempoMorto = 0;
    public bool podeReviver = false;
    public Text tempoMorte;

    void Start()
    {
        statusPersonagem = GetComponent<personagem>();
        postProcessing.profile.TryGetSettings(out GrayScale);
        tempoMorte.enabled = false;
    }
    void Update()
    {
        Morrer();
        Reviver();
    }


    void Morrer()
    {
        if (statusPersonagem.hpAtual == 0)
        {
            Animador.SetBool("isMorto", true);
            GetComponent<movimento>().Corpo.destination = transform.position;
            TempoMorto();
            GrayScale.saturation.value = -100f;
            tempoMorte.enabled = true;
            tempoMorte.text = (20 - (int)tempoMorto).ToString();
        }
    }

    void Reviver()
    {
        if (podeReviver == true)
        {
            tempoMorte.enabled = false;
            podeReviver = false;
            GrayScale.saturation.value = 0f;
            statusPersonagem.hpAtual = statusPersonagem.hpTotal[statusPersonagem.level - 1];
            GetComponent<movimento>().Corpo.Warp(new Vector3(0.7153803f, 1.19789f, 1.295348f));
            GetComponent<movimento>().Corpo.destination = transform.position;
            Animador.SetBool("isMorto", false);
        }
    }

    void TempoMorto()
    {
        tempoMorto += Time.deltaTime;
        if (tempoMorto >= 20f)
        {
            tempoMorto = 0;
            podeReviver = true;
        }
    }
}
