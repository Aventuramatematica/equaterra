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
        // Verifica se a m�sica atual terminou
        if (!musicPlayer.isPlaying)
        {
            PlayNextTrack();
        }
    }

    void PlayNextTrack()
    {
        // Certifica-se de que temos pelo menos uma faixa de m�sica
        if (backgroundMusic.Length == 0)
        {
            Debug.LogWarning("Nenhuma m�sica de fundo configurada.");
            return;
        }

        // Atualiza o �ndice da faixa
        currentTrackIndex = (currentTrackIndex + 1) % backgroundMusic.Length;

        // Carrega e reproduz a pr�xima faixa
        musicPlayer.clip = backgroundMusic[currentTrackIndex];
        musicPlayer.Play();
    }
}
