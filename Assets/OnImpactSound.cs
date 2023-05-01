using System.Collections.Generic;
using UnityEngine;

public class OnImpactSound : MonoBehaviour
{
    [SerializeField] List<AudioClip> _Sounds;

    void OnCollisionEnter(Collision collision)
    {
        PLaySound();
    }

    void OnTriggerEnter(Collider other)
    {
        PLaySound();
    }

    void PLaySound()
    {
        var sound = _Sounds[Random.Range(0, _Sounds.Count)];
        GetComponent<AudioSource>().PlayOneShot(sound);
    }
}
