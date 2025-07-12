using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
        [Range(0.1f, 3f)] public float pitch = 1f;
        public bool loop;

        [Header("Delay before playing this sound (seconds)")]
        [Min(0f)] public float startDelay = 0f;

        [HideInInspector] public AudioSource source;
    }

    public List<Sound> sounds;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = 0f;  // sonido 2D

            Debug.Log($"[AudioManager] Configurado '{s.name}' → Vol:{s.volume}, Pitch:{s.pitch}, Loop:{s.loop}, Delay:{s.startDelay}s");
        }
    }

    void Start()
    {
        // Arrancamos una coroutine independiente para cada sonido:
        foreach (var s in sounds)
        {
            StartCoroutine(PlayWithDelay(s));
        }
    }

    IEnumerator PlayWithDelay(Sound s)
    {
        if (s.startDelay > 0f)
            Debug.Log($"[AudioManager] '{s.name}' se reproducirá en {s.startDelay}s");
        yield return new WaitForSeconds(s.startDelay);

        Debug.Log($"[AudioManager] Play('{s.name}') llamado tras {s.startDelay}s");
        s.source.Play();
        Debug.Log($"[AudioManager] Reproduciendo '{s.name}'");
    }

    public void Play(string name)
    {
        var s = sounds.Find(x => x.name == name);
        if (s != null)
        {
            Debug.Log($"[AudioManager] Play('{name}') llamado");
            s.source.Play();
            Debug.Log($"[AudioManager] Reproduciendo '{name}'");
        }
        else Debug.LogWarning($"[AudioManager] Sound not found: {name}");
    }

    public void Stop(string name)
    {
        var s = sounds.Find(x => x.name == name);
        if (s != null)
        {
            Debug.Log($"[AudioManager] Stop('{name}') llamado");
            s.source.Stop();
        }
        else Debug.LogWarning($"[AudioManager] Sound not found: {name}");
    }

    public void Pause(string name)
    {
        var s = sounds.Find(x => x.name == name);
        if (s != null)
        {
            Debug.Log($"[AudioManager] Pause('{name}') llamado");
            s.source.Pause();
        }
        else Debug.LogWarning($"[AudioManager] Sound not found: {name}");
    }

    public void SetVolume(string name, float volume)
    {
        var s = sounds.Find(x => x.name == name);
        if (s != null)
        {
            s.source.volume = Mathf.Clamp01(volume);
            Debug.Log($"[AudioManager] Volumen de '{name}' ajustado a {s.source.volume}");
        }
        else Debug.LogWarning($"[AudioManager] Sound not found: {name}");
    }
}
