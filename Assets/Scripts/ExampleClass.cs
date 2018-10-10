using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animation))]
public class ExampleClass : MonoBehaviour
{
    public Animation anim;
    void Start()
    {
        anim = GetComponent<Animation>();
        AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f,0.4f,3.0f);
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;
        clip.SetCurve("", typeof(Transform), "localScale.x", curve);
        clip.SetCurve("", typeof(Transform), "localScale.y", curve);
        anim.AddClip(clip, "test");
        anim.Play("test");
    }
}