using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    //NoahCorreia


    //Every gameobject in Unity has a Transform component! Use this to set the target.
    private Transform target;

    //Add an offset to the view area, so that way it isn't dead on the target all the time. Default of "Vector3.zero" is the same as (0, 0, 0)
    public Vector3 offset = Vector3.zero;

    //For smooth transition between transitions, not necessary but it looks nice!
    public float smoothing = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        //On Start, look for a GameObject that has the tag "Player" and grab its transform.
        //Make sure you set the Player tag
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //This is happening in LateUpdate, which runs every frame after Update() is called. Since our Player is moving in Update, it makes sense to use
    //LateUpdate for a camera, so that way it knows where the target is by now. Leaving it in Update will cause jittery movement.
    void LateUpdate()
    {
        //Create a new Vector3 so we can set the z separately. The default position of the camera is -10 on the z. If it's set to
        //Be the target's z position, that will be 0 since this in 3D. This means the camera will be in the same z position as the target, therefore the target
        //will be invisible.
        //So, now the new position will be the target's x and y, but the current z.
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);

        //Set the camera's position using Vector3.Lerp.
        //Lerp means "Linear Interpolation" and is a smooth transition between positions at a speed that you set.
        //Parameters here are (starting position, target position, speed of transition)
        //This means the movement starts where the camera is, it moves to the target + the offset, at a speed of the smoothing rate
        //multiplied by deltaTime, which is the time between frames.
        transform.position = Vector3.Lerp(transform.position, targetPos + offset, smoothing * Time.deltaTime);
    }
}