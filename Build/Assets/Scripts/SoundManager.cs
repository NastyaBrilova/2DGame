using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] soundType;

    public void PlaySound(int id = 0)
    {
        GetComponent<AudioSource>().clip = soundType[id];
        GetComponent<AudioSource>().Play();
    }
}