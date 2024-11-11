using System.Collections; // Required for IEnumerator
using UnityEngine;

public class MonsterSoundEffects : MonoBehaviour
{
    public AudioClip[] idleSounds;
    public AudioClip[] chaseSounds;
    public float soundDistanceThreshold = 7f;    
    public float maxVolumeDistance = 3f;
    public float minVolumeDistance = 7f;

    private AudioSource audioSource;
    private Coroutine currentIdleCoroutine;
    private Coroutine currentChaseCoroutine;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        UpdateSoundVolume(distanceToPlayer);

        // Start idle sound only if not in chase
        if (distanceToPlayer > soundDistanceThreshold && currentChaseCoroutine == null)
        {
            if (currentIdleCoroutine == null)
            {
                StartIdleSound();
            }
        }
        else
        {
            StopAllSounds();
            StartChaseSound();
        }
    }

    public void StartIdleSound()
    {
        StopAllSounds();
        currentIdleCoroutine = StartCoroutine(PlayIdleSounds());
    }

    public void StartChaseSound()
    {
        StopAllSounds();
        currentChaseCoroutine = StartCoroutine(PlayChaseSounds());
    }

    public void StopAllSounds()
    {
        if (currentIdleCoroutine != null)
        {
            StopCoroutine(currentIdleCoroutine);
            currentIdleCoroutine = null;
        }

        if (currentChaseCoroutine != null)
        {
            StopCoroutine(currentChaseCoroutine);
            currentChaseCoroutine = null;
        }

        audioSource.Stop();
    }

    private IEnumerator PlayIdleSounds()
    {
        while (true)
        {
            PlayRandomSound(idleSounds);
            if (audioSource.clip != null) // Check if the clip is not null
            {
                yield return new WaitForSeconds(audioSource.clip.length + 0.5f);
            }
            else
            {
                Debug.LogWarning("Audio clip is null!");
                yield break; // Exit the coroutine if clip is null
            }
        }
    }

    private IEnumerator PlayChaseSounds()
    {
        while (true)
        {
            PlayRandomSound(chaseSounds);
            if (audioSource.clip != null) // Check if the clip is not null
            {
                yield return new WaitForSeconds(audioSource.clip.length + 0.5f);
            }
            else
            {
                Debug.LogWarning("Audio clip is null!");
                yield break; // Exit the coroutine if clip is null
            }
        }
    }

    private void PlayRandomSound(AudioClip[] soundArray)
    {
        if (soundArray.Length > 0)
        {
            int randomIndex = Random.Range(0, soundArray.Length);
            audioSource.clip = soundArray[randomIndex];
            audioSource.Play();
            Debug.Log($"Playing sound: {audioSource.clip.name} with length: {audioSource.clip.length}");
        }
        else
        {
            Debug.LogWarning("Sound array is empty!");
        }
    }

    private void UpdateSoundVolume(float distanceToPlayer)
    {
        if (distanceToPlayer > soundDistanceThreshold)
        {
            audioSource.volume = 0f;
        }
        else
        {
            float volume = Mathf.Clamp01(1 - (distanceToPlayer - maxVolumeDistance) / (minVolumeDistance - maxVolumeDistance));
            audioSource.volume = volume;
        }
    }
}
