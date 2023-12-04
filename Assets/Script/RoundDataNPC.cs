using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoundDataNPC
{
    public string dificuldade;

    public int limiteDeTempo;

    public int pontosPorAcerto;

    public int pontosPorErro;

    public int danoPorAcerto;

    public int danoPorErro;

    public QuestionDataNPC[] perguntas;
}
