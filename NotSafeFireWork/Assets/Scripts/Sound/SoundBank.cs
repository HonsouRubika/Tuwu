using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound Bank", menuName = "Sound Bank", order = 50)]
public class SoundBank : ScriptableObject
{
    public List<SoundClip> clips = new List<SoundClip>();
}

[System.Serializable]
public struct SoundClip
{
    public string name;
    public AudioClip clip;
    public float volume;
}
