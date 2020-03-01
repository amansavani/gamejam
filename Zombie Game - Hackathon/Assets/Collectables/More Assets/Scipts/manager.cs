using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manager : MonoBehaviour
{
    public AudioSource myAudio;
    public AudioSource scream;
    public AudioSource helicopter;
    public bool canEscape = false;
    public Slider health;
    public int totalPickUps = 0;
    public Text timer;
    public Text Collected;
    private float totalTime = 30f; //2 minutes
    private float starttime;
    private string fullText = "You were not supposed to be here.";
    private float delay = 0.1f;
    private bool end;
    public GameObject fullscreen;

    void Start()
    {
        starttime = System.DateTime.Now.Second;
        end = false;
    }

    void Update()
    {
        if (!end)
        {
            if (totalPickUps == 5)
            {
                canEscape = true;
                EndGame();
            }
            totalTime -= Time.deltaTime;
            if (totalTime >= 0)
            {
                UpdateLevelTimer(totalTime);
            }
            if (totalTime <= 0)
            {
                end = true;
                EndGame();
            }
            Collected.text = "Collected: " + totalPickUps/2 + "/5";
            if (health.value <= 0)
            {
                end = true;
                EndGame();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
                        // Application.Quit() does not work in the editor so
                        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                        UnityEditor.EditorApplication.isPlaying = false;
            #else
                                    Application.Quit();
            #endif
        }
    }

    public void EndGame()
    {
        health.gameObject.SetActive(false);
        Collected.gameObject.SetActive(false);
        if (!canEscape)
        {
            scream.Play();
            fullscreen.SetActive(true);
            timer.GetComponent<RectTransform>().sizeDelta = new Vector2(timer.GetComponent<RectTransform>().sizeDelta.x, timer.GetComponent<RectTransform>().sizeDelta.y + 400f);
            timer.GetComponent<RectTransform>().position = new Vector3(timer.GetComponent<RectTransform>().position.x, timer.GetComponent<RectTransform>().position.y - 300, timer.GetComponent<RectTransform>().position.z);
            timer.fontSize = 8;
            StartCoroutine(Type());
        }
        else
        {
            helicopter.Play();
            timer.gameObject.SetActive(false);
            fullscreen.SetActive(true);
        }
    }
    
    IEnumerator Type()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            timer.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(delay);
        }
    }

    public void UpdateLevelTimer(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.RoundToInt(totalSeconds % 60f);
        string formatedSeconds = seconds.ToString();
        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }
        timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject);

        if (col.gameObject.tag == "wall")
        {
            if (health.value < 1)
            {
                health.value += 0.0035f;
            }
        }

        //if (Input.GetKeyDown(KeyCode.F))
        else
        {
            myAudio.Play();
            if (col)
            {
                StartCoroutine(destroy(col));
            }

        }
    }

    IEnumerator destroy(Collider col)
    {
        yield return new WaitForSeconds(myAudio.clip.length);
        totalPickUps = totalPickUps + 1;
        Destroy(col.gameObject);
    }
}
