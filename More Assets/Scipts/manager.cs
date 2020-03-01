using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{


    
    public AudioSource myAudio;
    public bool canEscape = false;

    public int totalPickUps=0;
    void Start()
    {

        myAudio = GetComponent<AudioSource>();
        myAudio.clip = Resources.Load<AudioClip>("coin");

    }


    void Update()
    {
        Debug.Log(totalPickUps);
        if (totalPickUps == 8)
        {
            canEscape = true;
        }
        if (canEscape == true)
        {

        }


        
    }
    void OnTriggerStay(Collider col)
    {
        Debug.Log(col.gameObject);

        if (Input.GetKeyDown(KeyCode.F))
        {
            myAudio.Play();
            totalPickUps = totalPickUps + 1;
            StartCoroutine(destory(col));

        }
    }
    IEnumerator destory(Collider col)
    {
        yield return new WaitForSeconds(myAudio.clip.length);
        Destroy(col.gameObject);
    }
}
