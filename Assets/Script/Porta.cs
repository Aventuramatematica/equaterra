using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta : MonoBehaviour
{
    [SerializeField]
    private string nomeProximaFase;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IrProxFase();
    }

    private void IrProxFase()
    {
        SceneManager.LoadScene(this.nomeProximaFase);
    }
}
