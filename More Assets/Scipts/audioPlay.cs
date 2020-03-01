using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioPlay : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource myAudio;
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.clip = Resources.Load<AudioClip>("coin");
        myAudio.Play();

    }
    public void PlayAudio()
    {
        myAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
