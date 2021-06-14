using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager m_soundManager;
    [SerializeField] AudioClip m_bakuhatsu = default;
    [SerializeField] AudioClip m_bakuhatsuCoverHit = default;
    [SerializeField] AudioClip m_doukasen = default;
    [SerializeField] AudioClip[] m_coverSelect = default;
    [SerializeField] AudioClip m_ending = default;

    static AudioSource m_audio;

    private void Start()
    {
        m_audio = GetComponent<AudioSource>();
    }

    public void PlayEnding()
    {
        m_audio.PlayOneShot(m_ending);
    }

    public void PlayCoverHit()
    {
        int index = Random.Range(0, m_coverSelect.Length);
        m_audio.PlayOneShot(m_coverSelect[index]);
    }

    public void PlayDoukasen()
    {
        m_audio.PlayOneShot(m_doukasen);
    }

    public void PlayBakuhatsuCoverHit()
    {
        m_audio.PlayOneShot(m_bakuhatsuCoverHit);
    }

    public void PlayBakuhatsu()
    {
        m_audio.PlayOneShot(m_bakuhatsu);
    }


}
