using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Zombie : MonoBehaviour
{
    public GameObject player;
    public AnimationClip[] animations;
    public Slider healthBar;
    public Image fillBar;
    protected Animator animator;
    protected AnimatorOverrideController animatorOverrideController;
    private int follow = 0;
    private float zombieSpeed = 0.5f;
    private int hitwall = 0;
    private Rigidbody rigidbody;
    private GameObject followPlayer;
    Vector3 playerrot;
    private float updateMotion;
    private Vector3 initialpos;
    public AudioSource attack;
    private float exitRoom;
    private float startPlaying;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;
        rigidbody = GetComponent<Rigidbody>();
        System.Random rnd = new System.Random();
        int index = rnd.Next(0, 2);
        animatorOverrideController["Z_Attack"] = animations[index];
        updateMotion = System.DateTime.Now.Second;
        exitRoom = System.DateTime.Now.Second;
        initialpos = transform.position;
        hitwall = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2.3f && hitwall==0)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezePosition;
            animatorOverrideController["Z_Attack"] = animations[3];
            Vector3 playerpos = new Vector3(transform.position.x, -2.886f, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, playerpos, zombieSpeed * Time.deltaTime);
            playerrot = new Vector3(0, player.transform.rotation.y, 0);
            transform.LookAt(player.transform);
            rigidbody.constraints = RigidbodyConstraints.FreezePosition;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            healthBar.value -= 0.0035f;
            if (!attack.isPlaying)
            {
                attack.Play();
                startPlaying = System.DateTime.Now.Second;
            }
            follow = 0;
        }
        
        else if (Vector3.Distance(transform.position, player.transform.position) <= 15 && hitwall == 0)
        {
            //attack.Play();
            if(System.DateTime.Now.Second - startPlaying >= 7 && !attack.isPlaying)
            {
                attack.Play();
                startPlaying = System.DateTime.Now.Second;
            }
            animatorOverrideController["Z_Attack"] = animations[2];
            Vector3 playerpos = new Vector3(player.transform.position.x, -2.886f, player.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, playerpos, zombieSpeed * Time.deltaTime);
            follow = 1;
            playerrot = new Vector3(0, player.transform.rotation.y, 0);
            transform.LookAt(player.transform);
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
        }
        
        else if (System.DateTime.Now.Second - updateMotion >= 100)
        {
            System.Random rnd = new System.Random();
            int index = rnd.Next(0, 2);
            animatorOverrideController["Z_Attack"] = animations[index];
            updateMotion = System.DateTime.Now.Second;
        }
        if(System.DateTime.Now.Second-exitRoom >= 14)
        {
            hitwall = 0;
        }
        if (follow == 1 && hitwall==0)
        {
            animatorOverrideController["Z_Attack"] = animations[2];
            Vector3 playerpos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, playerpos, zombieSpeed * Time.deltaTime);
            follow = 1;
            playerrot = new Vector3(0, player.transform.rotation.y, 0);
            transform.LookAt(player.transform);
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
        }
        if (healthBar.value <= 0.3f)
        {
            fillBar.color = Color.red;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wall")
        {
            hitwall = 1;
            follow = 0;
            rigidbody.constraints = RigidbodyConstraints.FreezePosition;
            transform.Rotate(0, 180 + transform.rotation.y, 0);
            animatorOverrideController["Z_Attack"] = animations[1];
            transform.position = Vector3.Lerp(transform.position, initialpos, zombieSpeed * Time.deltaTime);
            rigidbody.constraints = RigidbodyConstraints.None;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        exitRoom = System.DateTime.Now.Second;
        follow = 0;
    }
}
