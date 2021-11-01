using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControls : MonoBehaviour
{
    static Animator anim;
    //private InputDevice Controller;
    //private XRNode

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // if(InputgetButtonDown("Jump"))
        {
            anim.SetTrigger("DiagStartTrigger");
        }
        
    }
}
