using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSetTrigger : MonoBehaviour
{
    static Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("DiagStartTrigger");
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetTrigger("DiagStartTrigger");
    }
}
