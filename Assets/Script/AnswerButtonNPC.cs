using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButtonNPC : MonoBehaviour
{
    public Text textoDaResposta;
    private AnswerDataNPC answerData;

    private GameControllerNPC gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType <GameControllerNPC>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(AnswerDataNPC data)
    {
        answerData = data;
        textoDaResposta.text = answerData.textoResposta;
    }

    public void HandleClick()
    {
        gameController.AnswerButtonClicked(answerData.estaCorreta);
    }
}
