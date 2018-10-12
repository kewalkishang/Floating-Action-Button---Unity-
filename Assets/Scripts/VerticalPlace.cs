using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlace : MonoBehaviour {
    [Range(2, 6)]
    public int NoButton = 2;
    public float ButDis = 50f;
    public GameObject button;
    public GameObject MainButton;
    bool place = false;
    List<GameObject> selected = new List<GameObject>();
    int mov = 0;
    // Use this for initialization
    public void PlaceVertical () {
        for (int i = 0; i < NoButton; i++)
        {  GameObject go = Instantiate(button, MainButton.transform.position, Quaternion.identity);
            go.transform.parent = MainButton.transform;
            selected.Add(go);
        }
        //Debug.Log("Sel" + selected.Count);
        target = MainButton; 
        place = true;

	}
    float t = 0;
    GameObject target; 
	// Update is called once per frame
	void Update () {
		if(place)
        {
            t = t + Time.deltaTime;
            float distance = Vector2.Distance(selected[mov].transform.position, target.transform.position + new Vector3(0, ButDis, 0));

           
           if (distance > 0.1f)
            selected[mov].transform.position = Vector3.Lerp(selected[mov].transform.position,new Vector3(target.transform.position.x, target.transform.position.y + ButDis, target.transform.position.z), t);
            else
            {
                if (mov < NoButton-1)
                {
                    target = selected[mov];
                    mov += 1;
                   
                    t = 0;
                }
                else
                {
                    place = false;
                }
               
                }
            Debug.Log("mov :" + mov + " place :" + place + " dis :" + distance + " target pos : " + target.transform.position.y + ButDis);

        }
	}



}
