using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizMenuController : MonoBehaviour
{
    private DataController data;

    // Start is called before the first frame update
    void Start()
    {
        data = FindObjectOfType<DataController>();
    }

    public void startGame(int round)
    {
        data.SetRoundData(round);
        SceneManager.LoadScene("Cutscene Inicial");
    }
}
