using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class pickUp : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem ps;
    private GameObject g;
    audioPlay ap;
    manager m;


    void Start()
    {   
        //g=GameObject.Find()

        ap = gameObject.GetComponent<audioPlay>();
        m= GameObject.Find("person").GetComponent<manager>();

    }

    // Update is called once per frame
    void Update()
    {

        
    }
    void OnTriggerStay(Collider col)
    {
        Debug.Log(col.gameObject);
        if (Input.GetKeyDown(KeyCode.F))
        {

            ps.enableEmission = false;
            ap.PlayAudio();
            m.totalPickUps += 1;



            StartCoroutine(destory());
            
        }
    }
    IEnumerator destory()
    {
        yield return new WaitForSeconds(ap.myAudio.clip.length);
  
        Destroy(this.gameObject);
    }
  

}
