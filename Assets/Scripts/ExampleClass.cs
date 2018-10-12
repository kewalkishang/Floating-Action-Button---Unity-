using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animation))]
public class ExampleClass : MonoBehaviour
{
    [Range(2, 6)]
    public int NoButton = 2;
    public Animation anim;
     public void Expand()
    {
        float exp = 0.65f * NoButton;
        anim = GetComponent<Animation>();
        AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f,0.4f,exp);
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;
        clip.SetCurve("", typeof(Transform), "localScale.x", curve);
        clip.SetCurve("", typeof(Transform), "localScale.y", curve);
        anim.AddClip(clip, "test");
        anim.Play("test");
    }
}