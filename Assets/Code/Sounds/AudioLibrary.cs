using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Audio/AudioLibrary")]
public class AudioLibrary : ScriptableObject
{
    public List<AudioClip> clips;

    public AudioClip GetClipByName(string name)
    {
        return clips.Find(clip => clip.name == name);
    }
}
