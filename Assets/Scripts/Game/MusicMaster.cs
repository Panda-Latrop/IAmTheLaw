using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMaster : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] clips;
    [SerializeField]
    private int current = 0;

    public void Start()
    {
        current = Random.Range(0, clips.Length);
        source.clip = clips[current];
        source.Play();
    }
    void LateUpdate()
    {
        if (!source.isPlaying)
        {
            source.clip = clips[current + 1 >= clips.Length ? 0 : ++current];
            source.Play();
        }
    }
}
