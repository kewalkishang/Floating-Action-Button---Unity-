using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Animation))]
public class Transparency : MonoBehaviour {

    public Animation anim;
    void Start()
    {
        anim = GetComponent<Animation>();
        AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 2.0f, 1.0f);
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;
        clip.SetCurve("", typeof(GameObject), "renderer.material.color.a", curve);
        anim.AddClip(clip, "test");
        anim.Play("test");

        Color col = GetComponent<Image>().color;
        col.a = 1;
        GetComponent<Image>().color = col;


    }
    void Update()
    {

    }
    }
