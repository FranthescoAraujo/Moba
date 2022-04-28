using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpImagemPersonagem : MonoBehaviour
{
    public RectTransform imagemHP;
    public float porcentagemHP;
    public personagem status;
    public Text level;

    void Start()
    {
        imagemHP = GetComponent<RectTransform>();
    }

    void Update()
    {
        level.text = "Level " + status.level.ToString();
        porcentagemHP = (float)status.hpAtual / status.hpTotal[status.level - 1] * 100;
        if (porcentagemHP <= 0) porcentagemHP = 0;
        imagemHP.sizeDelta = new Vector2(porcentagemHP, 15);
    }
}
