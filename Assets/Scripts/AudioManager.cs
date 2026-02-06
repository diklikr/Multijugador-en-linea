using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioManager instance;

    public AudioSource audioSource;
    public AudioClip doorClip;
    public AudioClip keyClip;
    public AudioClip attackClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayDoorSFX()
    {
        PlaySFX(doorClip);
    }

    public void PlayKeySFX()
    {
        PlaySFX(keyClip);
    }

    public void PlayAttackSFX()
    {
        PlaySFX(attackClip);
    }
}
