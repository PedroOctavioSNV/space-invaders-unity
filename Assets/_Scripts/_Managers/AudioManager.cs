using System.Collections;
using UnityEngine;

/// <summary>
/// Gerencia a reprodução de música de batalha e efeitos sonoros.
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] AudioSource battleMusicSource; // fonte da música de batalha
    [SerializeField] AudioSource sfxSource;         // fonte de efeitos sonoros

    bool muted;
    bool isPlaying;
    float delay;

    const float delayThick = 0.05f;

    static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // garante singleton
        }
    }

    void Start()
    {
        muted = PlayerPrefs.GetInt("MUTED") == 1;

        if (muted)
        {
            AudioListener.pause = true;
        }
    }

    /// <summary>
    /// Alterna entre mutado e não mutado.
    /// </summary>
    public void ToogleMute()
    {
        muted = !muted;

        if (muted)
        {
            PlayerPrefs.SetInt("MUTED", 1);
        }
        else
        {
            PlayerPrefs.SetInt("MUTED", 0);
        }

        AudioListener.pause = muted;
    }

    /// <summary>
    /// Inicia a reprodução contínua da música de batalha.
    /// </summary>
    public static void PlayBattleMusic()
    {
        instance.delay = 1;
        instance.isPlaying = true;
        instance.StartCoroutine(instance.BattleSound());
    }

    /// <summary>
    /// Para a música de batalha.
    /// </summary>
    public static void StopBattleMusic()
    {
        instance.isPlaying = false;
        instance.StopCoroutine(instance.BattleSound());
    }

    /// <summary>
    /// Reproduz um efeito sonoro específico.
    /// </summary>
    public static void PlaySoundEffect(AudioClip clip)
    {
        if (!instance.muted)
        {
            instance.sfxSource.PlayOneShot(clip);
        }
    }

    /// <summary>
    /// Atualiza o intervalo de tempo entre cada reprodução da música de batalha.
    /// </summary>
    public static void UpdateBattleMusicDelay(int i)
    {
        float delaytime = i * delayThick;

        if (delaytime < 0.2f) delaytime = 0.2f;
        if (delaytime > 1) delaytime = 1;

        instance.delay = delaytime;
    }

    IEnumerator BattleSound()
    {
        while (isPlaying)
        {
            yield return new WaitForSeconds(delay);
            battleMusicSource.Play();
        }
    }
}