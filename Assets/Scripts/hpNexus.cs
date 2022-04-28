using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpNexus : MonoBehaviour
{
    public RectTransform imagemHP;
    public float porcentagemHP;
    public inimigo status;
    

    void Start()
    {
        imagemHP = GetComponent<RectTransform>();
    }

    void Update()
    {
        porcentagemHP = (float)status.hpAtual / status.hpTotal * 100;
        imagemHP.sizeDelta = new Vector2(porcentagemHP, 15);
    }
}
