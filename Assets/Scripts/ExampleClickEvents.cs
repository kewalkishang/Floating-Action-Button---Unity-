using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExampleClickEvents : MonoBehaviour {
    public Image im;
    public Sprite[] sp;
    public void Button1()
    {
        im.sprite = sp[0];
        Debug.Log("Buttons1");
    }
    public void Button2()
    {
        im.sprite = sp[1];
        Debug.Log("Buttons2");
    }
    public void Button3()
    {
        im.sprite = sp[2];
        Debug.Log("Buttons3");
    }
    public void Button4()
    {
        im.sprite = sp[3];
        Debug.Log("Buttons4");
    }
  
}
