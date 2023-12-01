using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoundData
{
    public string dificuldade;

    public int limiteDeTempo;

    public int pontosPorAcerto;

    public int danoPorAcerto;

    public int danoPorErro;

    public QuestionData[] perguntas;
}
