using UnityEngine;

[CreateAssetMenu(fileName = "SimpleMusicEvent", menuName = "Audio/Music Event")]
public class SimpleMusicEvent : ScriptableObject
{
    [SerializeField] private AudioClip clip;
    private bool loop = true;
    [Range(0f, 1f)] public float volume = 1f;
    public void Play(AudioSource source)
    {
        if (clip == null) return;

        source.clip = clip;
        source.loop = loop;
        source.volume = volume;
        source.Play();
    }

    public void Stop(AudioSource source)
    {
        source.Stop();
    }
}
