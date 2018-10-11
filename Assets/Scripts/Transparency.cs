using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Transparency : MonoBehaviour {
    Color col;

    float t = 0; 
    public float duration= 1f;
    void Start()
    {
     
        col= GetComponent<Image>().color;
       


    }
    void Update()
    {
        t += Time.deltaTime / duration;
        col.a = Mathf.Lerp(0, 1, t);
        GetComponent<Image>().color = col;
    }
  }
