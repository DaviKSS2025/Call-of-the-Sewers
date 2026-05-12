using UnityEngine;

[CreateAssetMenu(fileName = "NewAudioEvent", menuName = "Audio/Simple SFX Event")] 
public class SimpleSFXEvent : ScriptableObject 
{ 
    public AudioClip[] clips; 
    public Vector2 pitchRange = new Vector2(0.8f, 1.2f); 
    [Range(0, 1)] public float volume = 1f; 
    public void Play(AudioSource source) 
    { 
        if (clips.Length == 0) return;
        // 1. Define o pitch individual deste som no canal
        source.pitch = UnityEngine.Random.Range(pitchRange.x, pitchRange.y); 
        // 2. Toca sem interromper o que já está lá (Overlap) O volume aqui é multiplicado pelo volume global do AudioSource
        source.PlayOneShot(clips[UnityEngine.Random.Range(0, clips.Length)], volume); 
    } 
}