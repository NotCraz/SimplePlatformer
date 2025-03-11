using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    //NoahCorreia

    public bool canBeSwitched = false;
    public bool switched = false;

    // Update is called once per frame
    void Update()
    {
        if (canBeSwitched && !switched)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                switched = true;

            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            canBeSwitched = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            canBeSwitched = false;
        }
    }
}
