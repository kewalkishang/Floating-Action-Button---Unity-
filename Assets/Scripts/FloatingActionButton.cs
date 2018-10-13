using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FloatingActionButton : MonoBehaviour {

  //PUBLIC VARIABLES FOR AFB
    public enum FabANIM { VerticallyUp, CircleQuadrant };
    public FabANIM FABType;
    public Button MainButton;
    public GameObject ButtonPrefab;
    public Sprite Back;
    public Sprite Menu;

    [System.Serializable]
    public class ButtonInfo
    {
        public Sprite sprite;
        public UnityEvent ButtonEvent;
    }
    public ButtonInfo[] ButtonCount2To6;


    //private variables of AFB
    int NoButton;
    bool expand = true;

    //VARIABLES FOR VERTICAL ANIM
    bool place = false;
    bool goDown = false;
    List<GameObject> selected = new List<GameObject>();
    int mov = 0;
    float t = 0;
    public float ButDis = 50f;
    private GameObject target;

    //variables for quad anim 
    public Animation anim;

    void Start()
    { 
        NoButton = ButtonCount2To6.Length; 
        if (FABType==FabANIM.VerticallyUp)
        {
            InitializeVertical();
            MainButton.onClick.AddListener(VerticallyUp);

        }
        else
        {
            Initializequad();
            MainButton.onClick.AddListener(CircleQuad);
        }
            
    }

    void VerticallyUp()
    {
        if (expand)
        {
            foreach (Transform child in MainButton.transform)
                child.gameObject.SetActive(true);
            place = true;
            expand = false;
            MainButton.gameObject.GetComponent<Image>().sprite = Back;


        }
        else
        {
            MainButton.gameObject.GetComponent<Image>().sprite = Menu;
            mov = NoButton - 1;
            foreach (Transform child in MainButton.transform)
            {
                child.gameObject.GetComponent<Button>().interactable = false;
            }

            expand = true;
            goDown = true;
     
        }
    }


    void InitializeVertical()
    {
       
        for (int i = 0; i < NoButton; i++)
        {
            GameObject go = Instantiate(ButtonPrefab, MainButton.transform.position, Quaternion.identity);
            go.GetComponent<Image>().sprite = ButtonCount2To6[i].sprite;
            go.name = i.ToString();
           

            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                int j = int.Parse(go.name);
                ButtonCount2To6[j].ButtonEvent.Invoke();
            });
            go.transform.parent = MainButton.transform;
            go.transform.localScale = MainButton.gameObject.transform.localScale;
            selected.Add(go);
        }
        foreach (Transform child in MainButton.transform)
        {
            child.gameObject.GetComponent<Button>().interactable = false;
            child.gameObject.SetActive(false);

        }
        Debug.Log("Sel" + selected.Count);
        target = MainButton.gameObject;
    }

    void Initializequad()
    {
        float radius = Screen.width / 8 * NoButton;
        float Buoffset = 90 / NoButton;

        for (int i = 0; i < NoButton; i++)
        {
            float angleRadians = i * Buoffset * Mathf.Deg2Rad + 90 * Mathf.Deg2Rad + (Buoffset / 2) * Mathf.Deg2Rad;

            // get the 2D dimensional coordinates
            float x = radius * Mathf.Cos(angleRadians);
            float y = radius * Mathf.Sin(angleRadians);

            Vector3 newPos = new Vector2(x, y);
            // Debug.Log("x : " + x + " y : " + y);
            GameObject go = Instantiate(ButtonPrefab, newPos, Quaternion.identity);
            go.GetComponent<Image>().sprite = ButtonCount2To6[i].sprite;
            go.name = i.ToString();
            
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                int j = int.Parse(go.name);
                ButtonCount2To6[j].ButtonEvent.Invoke();
            });
            go.transform.parent = MainButton.transform;
            go.transform.localScale = MainButton.gameObject.transform.localScale;
            go.transform.localPosition = newPos;

        }
        foreach (Transform child in MainButton.transform)
        {
            child.gameObject.GetComponent<Button>().interactable = false;

            child.gameObject.SetActive(false);
        }

    }

    void CircleQuad()
    {
        Expand();
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        
        
        if (expand)
        {
            MainButton.gameObject.GetComponent<Image>().sprite = Back;
            yield return new WaitForSeconds(0.5f);
            foreach (Transform child in MainButton.transform)
            {
                child.gameObject.SetActive(true);

                child.gameObject.GetComponent<Button>().interactable = true;

            }
            expand = false;
        }
        else
        {
            MainButton.gameObject.GetComponent<Image>().sprite = Menu;
            yield return new WaitForSeconds(0f);
            foreach (Transform child in MainButton.transform)
            {
                child.gameObject.SetActive(false);
                child.gameObject.GetComponent<Button>().interactable = false;


            }
            expand = true;
            Debug.Log("extract");
        }

    }


    void Update()
    {
        if (place)
        {
            t = t + Time.deltaTime;
            float distance = Vector2.Distance(selected[mov].transform.position, target.transform.position + new Vector3(0, Screen.height/8, 0));


            if (distance > 0.1f)
                selected[mov].transform.position = Vector3.Lerp(selected[mov].transform.position, new Vector3(target.transform.position.x, target.transform.position.y + Screen.height / 8, target.transform.position.z), t);
            else
            {
                if (mov < NoButton - 1)
                {
                    target = selected[mov];
                    mov += 1;
                    t = 0;
                }
                else
                {
                    place = false;
                    foreach (Transform child in MainButton.transform)
                        child.gameObject.GetComponent<Button>().interactable = true;
                }

            }
            // Debug.Log("mov :" + mov + " place :" + place + " dis :" + distance + " target pos : " + target.transform.position.y + ButDis);

        }

        if(goDown)
        {
            t = t + Time.deltaTime;
            float distance = Vector2.Distance(selected[mov].transform.position, MainButton.gameObject.transform.position);


            if (distance > 0.1f)
                selected[mov].transform.position = Vector3.Lerp(selected[mov].transform.position, MainButton.gameObject.transform.position, t);
            else
            {
                selected[mov].SetActive(false);
                if (mov > 0)
                {

                    mov -= 1;

                    t = 0;
                }
                else
                {
                    goDown = false;
                   

                }
            }
        }
    }
 
    public void Expand()
    {
        float exp = 0.83f * NoButton;
        AnimationCurve curve;
        if (expand)
        { curve = AnimationCurve.Linear(0.0f, 0.0f, 0.4f, exp);
        } else
        {
            curve = AnimationCurve.Linear(0.0f, exp, 0.4f, 0.0f);
        }

        AnimationClip clip = new AnimationClip();
        clip.legacy = true;
        clip.SetCurve("", typeof(Transform), "localScale.x", curve);
        clip.SetCurve("", typeof(Transform), "localScale.y", curve);
        anim.AddClip(clip, "test");
        anim.Play("test");
    }


}
