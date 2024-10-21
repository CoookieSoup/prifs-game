using UnityEngine;

public class Audio : MonoBehaviour
{
    static AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public static void Play(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}