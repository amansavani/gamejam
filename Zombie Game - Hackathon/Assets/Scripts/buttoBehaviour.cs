using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttoBehaviour : MonoBehaviour
{
    public GameObject text_field;
   public void close_this_canvas()
    {
        text_field.SetActive(false);    
        gameObject.SetActive(false);
    }
}
