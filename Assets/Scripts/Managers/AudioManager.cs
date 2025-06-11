using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private bool muted;

    public AudioSource battleMusicSource;
    public AudioSource sfxSource;

    private bool isPlaying;
    private float delay;

    private const float delayThick = 0.05f;

    private static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        muted = PlayerPrefs.GetInt("MUTED") == 1;

        if (muted)
        {
            AudioListener.pause = true;
        }
    }

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

        if (muted)
        {
            AudioListener.pause = true;
        }
    }

    public static void PlayBattleMusic()
    {
        instance.delay = 1;
        instance.isPlaying = true;
        instance.StartCoroutine(instance.BattleSound());
    }

    public static void StopBattleMusic()
    {
        instance.isPlaying = false;
        instance.StopCoroutine(instance.BattleSound());
    }

    public static void PlaySoundEffect(AudioClip clip)
    {
        if (!instance.muted)
        {
            instance.sfxSource.PlayOneShot(clip);
        }
    }

    public static void UpdateBattleMusicDelay(int i)
    {
        float delaytime = i * delayThick;

        if (delaytime < 0.2f)
        {
            delaytime = 0.2f;
        }

        if (delaytime > 1)
        {
            delaytime = 1;
        }

        instance.delay = delaytime;
    }

    private IEnumerator BattleSound()
    {
        while (isPlaying)
        {
            yield return new WaitForSeconds(delay);
            battleMusicSource.Play();
        }
    }
}