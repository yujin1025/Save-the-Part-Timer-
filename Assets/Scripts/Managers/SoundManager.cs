using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Defines.Sound.MaxCount];

    AudioMixerGroup[] masterAudioMixerGroups = new AudioMixerGroup[1];
    AudioMixerGroup[] effectAudioMixerGroups = new AudioMixerGroup[1];
    AudioMixerGroup[] backgroundAudioMixerGroups = new AudioMixerGroup[1];

    public AudioMixer audioMixer;

    string masterGroupName = "Master";
    string effectGroupName = "Effect";
    string bgmGroupName = "BGM";

    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    // MP3 Player   -> AudioSource
    // MP3 음원     -> AudioClip
    // 관객(귀)     -> AudioListener

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);


            audioMixer = Managers.resourceManagerProperty.Load<AudioMixer>("Sounds/MainMixer");
            masterAudioMixerGroups = audioMixer.FindMatchingGroups(masterGroupName);
            effectAudioMixerGroups = audioMixer.FindMatchingGroups(effectGroupName);
            backgroundAudioMixerGroups = audioMixer.FindMatchingGroups(bgmGroupName);


            string[] soundNames = System.Enum.GetNames(typeof(Defines.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Defines.Sound.Bgm].loop = true;
            _audioSources[(int)Defines.Sound.Bgm].outputAudioMixerGroup = backgroundAudioMixerGroups[0];
            _audioSources[(int)Defines.Sound.Effect].outputAudioMixerGroup = effectAudioMixerGroups[0];
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }

    public void Play(string path, Defines.Sound type = Defines.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Defines.Sound type = Defines.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Defines.Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Defines.Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Defines.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    AudioClip GetOrAddAudioClip(string path, Defines.Sound type = Defines.Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (type == Defines.Sound.Bgm)
        {
            audioClip = Managers.resourceManagerProperty.Load<AudioClip>(path);

        }
        else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.resourceManagerProperty.Load<AudioClip>(path);

                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");

        return audioClip;
    }
}
