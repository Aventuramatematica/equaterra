using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackgroundMusicController : MonoBehaviour
{
    public AudioClip[] backgroundMusicClips;
    public AudioMixer audioMixer;
    public string mixerGroupName = "background";

    private AudioSource audioSource;
    private bool isMusicPlaying = false;

    private static BackgroundMusicController instance;

    void Awake()
    {
        // Garante que apenas uma inst�ncia do BackgroundMusicController exista
        if (instance == null)
        {
            instance = this;
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;

            // Define o grupo do Audio Mixer
            if (audioMixer != null)
            {
                audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups(mixerGroupName)[0];
            }

        }
    }

    void Start()
    {
        // Inicia a reprodu��o da m�sica de fundo
        PlayRandomBackgroundMusic();
    }

    void PlayRandomBackgroundMusic()
    {
        if (backgroundMusicClips.Length == 0)
        {
            Debug.LogWarning("Nenhuma m�sica de fundo atribu�da ao array.");
            return;
        }

        // Escolhe uma m�sica aleat�ria do array
        AudioClip randomClip = backgroundMusicClips[Random.Range(0, backgroundMusicClips.Length)];

        // Define a m�sica no AudioSource
        audioSource.clip = randomClip;

        // Inicia a reprodu��o
        audioSource.Play();
        isMusicPlaying = true;
    }

    void SceneChanged(Scene scene, LoadSceneMode mode)
    {
        // Se a cena mudou e a m�sica estava tocando, pause a m�sica
        if (isMusicPlaying && audioSource != null)
        {
            audioSource.Pause();
            isMusicPlaying = false;
        }
    }


    void Update()
    {
        // Exemplo: pressione a tecla "Up" para aumentar o volume e "Down" para diminuir o volume
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeVolume(0.1f); // Aumenta o volume em 10%
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeVolume(-0.1f); // Diminui o volume em 10%
        }
    }

    void ChangeVolume(float amount)
    {
        // Ajusta o volume no Audio Mixer
        if (audioMixer != null)
        {
            audioMixer.SetFloat("Background", Mathf.Log10(amount) * 20);
        }
    }

    void SetMusicVolume(float volume)
    {
        // Define o volume da m�sica com base no valor do slider
        ChangeVolume(volume);
    }
}
