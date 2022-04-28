using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigo : MonoBehaviour
{
    public int level = 1;
    public int hpTotal = 100;
    public int hpAtual;
    public int dano = 10;

    void Start()
    {
        hpAtual = hpTotal;
    }
}
