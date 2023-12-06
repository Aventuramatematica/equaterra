using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapaController : MonoBehaviour
{
    public GameObject miniMapa;
    public GameObject maxMapa;

    // Start is called before the first frame update
    void Start()
    {
        // Certifique-se de atribuir refer�ncias aos objetos miniMapa e maxMapa no Editor Unity.
        if (miniMapa == null || maxMapa == null)
        {
            Debug.LogError("Certifique-se de atribuir refer�ncias aos objetos miniMapa e maxMapa no Editor Unity.");
        }

        // Desativa o minimapa no in�cio.
        if (maxMapa != null)
        {
            maxMapa.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica se a tecla "M" foi pressionada.
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Verifica se os objetos miniMapa e maxMapa s�o diferentes de null.
            if (miniMapa != null || maxMapa == null)
            {
                // Alterna entre a visibilidade do minimapa e do maxMapa.
                miniMapa.SetActive(!miniMapa.activeSelf);
                maxMapa.SetActive(!maxMapa.activeSelf);
            }
        }
    }
}

