using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelaBoasvindasController : MonoBehaviour
{
    [SerializeField] private GameObject telaBoasVindas;
    public  void FecharTela()
    {
        telaBoasVindas.SetActive(false);
    }
}
