using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCharacter : MonoBehaviour
{
    [SerializeField] AudioSource footStepsAudioSource = null;
    [Header("Audio Clips")]
    [SerializeField] AudioClip[] gramaSteps = null;
    [SerializeField] AudioClip[] ladrilhoSteps = null;
    [SerializeField] AudioClip[] terraSteps = null;
    [SerializeField] AudioClip[] areiaSteps = null;
    [SerializeField] AudioClip[] cidadeSteps = null;
    [SerializeField] AudioClip[] madeiraSteps = null;

    [Header("Steps")]
    [SerializeField] float timer = 0.5f;

    private float stepsTimer;

    public void PlaySteps(GroundType groundType, float speedNormalized)
    {
        Debug.Log("PlaySteps called. GroundType: " + groundType + ", Speed: " + speedNormalized);

        if (groundType == GroundType.None)
            return;

        stepsTimer += Time.fixedDeltaTime * speedNormalized;

        if (stepsTimer >= timer)
        {
            AudioClip[] steps;

            // Utilizando ternários corretamente para selecionar o array de áudios baseado no tipo de chão.
            steps = (groundType == GroundType.Grama) ? gramaSteps :
                    (groundType == GroundType.Ladrilho) ? ladrilhoSteps :
                    (groundType == GroundType.Terra) ? terraSteps :
                    (groundType == GroundType.Areia) ? areiaSteps :
                    (groundType == GroundType.Cidade) ? cidadeSteps:
                    madeiraSteps;

            int index = Random.Range(0, steps.Length);
            footStepsAudioSource.PlayOneShot(steps[index]);

            stepsTimer = 0;
        }
    }
}
