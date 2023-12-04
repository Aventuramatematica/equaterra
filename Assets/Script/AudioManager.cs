using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    private AudioSource audioSource;

    private static AudioManager instance;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;

        // Define o grupo do Audio Mixer
        if (audioMixer != null)
        {
            audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Efeitos Sonoros")[0];
        }
    }

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject audioManager = new GameObject("AudioManager");
                instance = audioManager.AddComponent<AudioManager>();
            }
            return instance;
        }
    }

    public void PlayHoverSound(AudioClip sound)
    {
        if (sound != null && audioSource != null) // Adiciona uma verificação para garantir que o audioSource foi inicializado
        {
            audioSource.clip = sound;
            audioSource.Play();
        }
    }

    public void PlayClickSound(AudioClip sound)
    {
        if (sound != null && audioSource != null)
        {
            audioSource.clip = sound;
            audioSource.Play();
        }
    }
}
