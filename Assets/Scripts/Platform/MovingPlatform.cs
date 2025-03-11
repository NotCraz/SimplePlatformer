using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //NoahCorreia


    private Animator anim;

    public bool isActive = false;

    private Lever lever;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        lever = GameObject.Find("Lever").GetComponent<Lever>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lever.switched == true)
        {
            isActive = true;

        }
        else
        {
            isActive = false;
        }
        anim.SetBool("isActive", isActive);
    }
}
