using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] AudioClip[] backgroundMusic;

    private int currentTrackIndex = 0;

    void Start()
    {
        PlayNextTrack();
    }

    void Update()
    {
        // Verifica se a música atual terminou
        if (!musicPlayer.isPlaying)
        {
            PlayNextTrack();
        }
    }

    void PlayNextTrack()
    {
        // Certifica-se de que temos pelo menos uma faixa de música
        if (backgroundMusic.Length == 0)
        {
            Debug.LogWarning("Nenhuma música de fundo configurada.");
            return;
        }

        // Atualiza o índice da faixa
        currentTrackIndex = (currentTrackIndex + 1) % backgroundMusic.Length;

        // Carrega e reproduz a próxima faixa
        musicPlayer.clip = backgroundMusic[currentTrackIndex];
        musicPlayer.Play();
    }
}
