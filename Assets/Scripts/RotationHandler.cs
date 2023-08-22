using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    [SerializeField]private int rotationStep = 4;
    [SerializeField] private AudioClip click;

    [SerializeField]private int currentRotation = 0;
    private Level level;

    private float turnSpeed = 30f;
    private bool isRotating = false;
    private Quaternion targetQuaternion;

    private void Start()
    {
        level = gameObject.GetComponentInParent<Level>();
    }
    private void Update()
    {
        if (isRotating)
        {
            Rotating();
        }
    }
    private void OnMouseDown()
    {
        if (GameHandler.Instance.IsPlaying() && !isRotating)
        {
            Rotate(90);
            level.CheckAllRotation();
            AudioSource.PlayClipAtPoint(click, transform.position, 1f);
        }
    }
    private void Rotating()
    {
        //keep rotating to reach target rotation 
        //quaternion slerp used to add smooth rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, Time.deltaTime * turnSpeed);

        //if target rotation becomes really close to current rotation
        //set current rotation to target rotation
        //and stop rotating
        if (Quaternion.Angle(transform.rotation, targetQuaternion) < 0.1f)
        {
            transform.rotation = targetQuaternion;
            isRotating = false;
        }
    }
    public void Rotate(int z)
    {
        //set target rotation
        Quaternion deltaRotation = Quaternion.Euler(0, 0, z);
        targetQuaternion = transform.rotation * deltaRotation;

        //set is rotating true to start rotating function
        isRotating = true;

        //to calculate number of rotation happened (if z = 90 so piece rotates one time)
        currentRotation += z / 90;

        //each piece have rotation step (line have 2 steps 'vertical - horizontal'
        //so if current rotation = rotation step it returns to default position
        if (currentRotation == rotationStep)
            currentRotation = 0;
    }
    public int GetRotationStep()
    {
        return rotationStep;
    }
    public int GetCurrentRotation()
    {
        return currentRotation;
    }

}
