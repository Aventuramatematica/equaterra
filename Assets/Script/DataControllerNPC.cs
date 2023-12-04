using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class DataControllerNPC : MonoBehaviour
{
    private RoundDataNPC[] todasAsRodadas;

    public int rodadaIndex;
    private int playerHighScore;

    private string gameDataFileName = "data.json";

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadGameData();
        SceneManager.LoadScene("Menu");

        // Configura vidaMaximaAtual com base no rodadaIndex
        ConfigurarVidaMaximaAtual();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRoundData (int round)
    {
        rodadaIndex = round;
    }

    public RoundDataNPC GetCurrentRoundData()
    {
        return todasAsRodadas[rodadaIndex];
    }

    private void LoadGameData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        try
        {
            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                GameDataNPC loadedData = JsonUtility.FromJson<GameDataNPC>(dataAsJson);
                todasAsRodadas = loadedData?.todasAsRodadas ?? new RoundDataNPC[0];
            }
            else
            {
                Debug.LogWarning("O arquivo de dados não existe. Criando um novo conjunto de dados.");
                todasAsRodadas = new RoundDataNPC[0];
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao carregar dados: {e.Message}");
        }
    }

    public void EnviarNovoHighScore(int newScore) 
    { 
        if ( newScore > playerHighScore)
        {
            playerHighScore = newScore;
            SavePLayerProgress();
        } 
    }

    private void ConfigurarVidaMaximaAtual()
    {
        PlayerHealthController playerHealthController = FindObjectOfType<PlayerHealthController>();

        if (playerHealthController != null)
        {
            switch (rodadaIndex)
            {
                case 0:
                    playerHealthController.DefinirVidaMaximaAtual(10);
                    break;
                case 1:
                    playerHealthController.DefinirVidaMaximaAtual(7);
                    break;
                case 2:
                    playerHealthController.DefinirVidaMaximaAtual(4);
                    break;
                case 3:
                    playerHealthController.DefinirVidaMaximaAtual(1);
                    break;
                    // Adicione mais casos conforme necessário
            }
        }
    }

    public int GetHighScore()
    {
        return playerHighScore;
    }

    private void LoadPlayerProgress()
    {
        if (PlayerPrefs.HasKey("highScore"))
        {
            playerHighScore = PlayerPrefs.GetInt("highScore");
        }
    }

    private void SavePLayerProgress()
    {
        PlayerPrefs.SetInt("highScore", playerHighScore);
    }
}
