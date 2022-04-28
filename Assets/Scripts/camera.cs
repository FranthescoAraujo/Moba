using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform Corpo;

    public float velocidadeHorizontal;
    public float velocidadeVertical;
    public float velH;
    public float velV;
    void Start()
    {
        transform.position = Corpo.transform.position;
    }

    void Update()
    {
        Mover();
        Seguir();
    }

    public void Seguir()
    {
        transform.position = Corpo.transform.position;
    }

    public void Mover()
    {
        velocidadeHorizontal = Input.GetAxis("Horizontal");
        velH += velocidadeHorizontal * 0.4f;

        transform.eulerAngles = new Vector3(transform.rotation.x, -velH, 0);

        velocidadeVertical = Input.GetAxis("Vertical");
        if (velocidadeVertical < 0 && Camera.main.fieldOfView >= 50)
        {
            Camera.main.fieldOfView -= 0.2f;
        }
        else if (velocidadeVertical > 0 && Camera.main.fieldOfView <= 80)
        {
            Camera.main.fieldOfView += 0.2f;
        }
    }
}
