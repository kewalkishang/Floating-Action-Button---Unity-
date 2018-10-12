using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPlace : MonoBehaviour {
    [Range(2, 6)]
    public int NoButton=2;
    public GameObject button;
    public GameObject MainButton;
    // Use this for initialization
    public void EnableButtons()
    {

        float radius = 25f * NoButton;
        float Buoffset = 90 / NoButton;

        for (int i = 0; i < NoButton; i++)
        {
            float angleRadians = i * Buoffset * Mathf.Deg2Rad + 90 * Mathf.Deg2Rad + (Buoffset / 2) * Mathf.Deg2Rad;

            // get the 2D dimensional coordinates
            float x = radius * Mathf.Cos(angleRadians);
            float y = radius * Mathf.Sin(angleRadians);

            Vector3 newPos = new Vector2(x, y);
           // Debug.Log("x : " + x + " y : " + y);
            GameObject go = Instantiate(button, newPos, Quaternion.identity);
            go.transform.parent = MainButton.transform;
            go.transform.localPosition = newPos;

        }


    }
}
