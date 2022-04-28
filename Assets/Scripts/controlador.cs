using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class controlador : MonoBehaviour
{
    public Image ImagemIniciar;
    public Image ImagemVitoria;
    public Image ImagemDerrota;
    public Button BotaoIniciar;
    public Button BotaoSair;

    public GameObject NexusInimigo;
    public GameObject NexusAliado;

    public float tempo = 0;
    public bool podeReiniciar = false;

    void Start()
    {
        Time.timeScale = 0;
        ImagemIniciar.enabled = true;
        ImagemVitoria.enabled = false;
        ImagemDerrota.enabled = false;
        BotaoIniciar.enabled = true;
        BotaoSair.enabled = true;
    }

    void Update()
    {
        TelaVitoria();
        TelaDerrota();
    }

    public void IniciarBotao()
    {
        Time.timeScale = 1;
        ImagemIniciar.enabled = false;
        ImagemVitoria.enabled = false;
        ImagemDerrota.enabled = false;
        BotaoIniciar.gameObject.SetActive(false);
        BotaoSair.gameObject.SetActive(false);
    }

    public void SairBotao()
    {
        Application.Quit();
    }

    void TelaVitoria()
    {
        if (NexusInimigo.GetComponent<inimigo>().hpAtual == 0)
        {
            Tempo();
            ImagemIniciar.enabled = false;
            ImagemVitoria.enabled = true;
            ImagemDerrota.enabled = false;
            BotaoIniciar.gameObject.SetActive(false);
            BotaoSair.gameObject.SetActive(false);

            if (podeReiniciar == true)
            {
                podeReiniciar = false;
                Reiniciar();
            }
        }
    }

    void TelaDerrota()
    {
        if (NexusAliado.GetComponent<inimigo>().hpAtual == 0)
        {
            Tempo();
            Time.timeScale = 0;
            ImagemIniciar.enabled = false;
            ImagemVitoria.enabled = false;
            ImagemDerrota.enabled = true;
            BotaoIniciar.gameObject.SetActive(false);
            BotaoSair.gameObject.SetActive(false);

            if (podeReiniciar == true)
            {
                podeReiniciar = false;
                Reiniciar();
            }
        }
    }

    void Tempo()
    {
        tempo += Time.unscaledDeltaTime;
        if (tempo > 10f){
            tempo = 0;
            podeReiniciar = true;
        }
    }

    void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 0;
        ImagemIniciar.enabled = true;
        ImagemVitoria.enabled = false;
        ImagemDerrota.enabled = false;
        BotaoIniciar.gameObject.SetActive(true);
        BotaoSair.gameObject.SetActive(true);
    }
}
