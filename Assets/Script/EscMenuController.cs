using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private GameObject menuEsc;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Se o menuEsc estiver ativo, fecha; caso contrário, abre.
            if (menuEsc.activeSelf)
            {
                FecharEscMenu();
            }
            else
            {
                AbrirEscMenu();
            }
        }
    }

    public void AbrirEscMenu()
    {
        menuEsc.SetActive(true);
    }

    public void FecharEscMenu()
    {
        menuEsc.SetActive(false);
    }

    public void VoltarMenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }
}
