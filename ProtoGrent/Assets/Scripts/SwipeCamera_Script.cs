using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCamera_Script : MonoBehaviour
{
    public GameObject cam;
    [Range(0.0f, 100.0f)]
    public float maxTranslate;
    [Range(0.0f, 100.0f)]
    public float maxRotate;
    [Range(0.0f, 10.0f)]
    public float sensitivity;
    [Header("Snaps Values")]
    public float leftSnap;
    public float centerSnap;
    public float rightSnap;

    public float travelled;
    private Vector3 position;
    private Vector3 lastPosition;
    private Vector3 deltaPosition;
    private Vector3 targetPosition;

    void Start()
    {
    }
    
    void FixedUpdate()
    {
        Drag();
    }

    void Drag()
    {

        if (Input.GetMouseButtonDown(0))
        {
            lastPosition = Input.mousePosition;
            position = Input.mousePosition;
            deltaPosition = Vector3.zero;
        }
        else if (Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0))
            {
                position = Input.mousePosition;
                deltaPosition = lastPosition - position;
                lastPosition = position;
            }
            
            
            Vector3 rotation = cam.transform.rotation.eulerAngles;
            rotation.y += deltaPosition.x * sensitivity;
            Quaternion newRot = Quaternion.Euler(rotation);

            float yRot = cam.transform.rotation.eulerAngles.y;
            if (yRot > 180)
            {
                yRot = -(360 - yRot);
            }

            travelled = yRot;

            targetPosition = cam.transform.position;
            Debug.Log("x : " + deltaPosition.x + " : " + yRot);
            if ((deltaPosition.x > 0 && yRot < maxRotate) || (deltaPosition.x < 0 && yRot > -maxRotate))
            {
                cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, newRot, 5f * Time.deltaTime);
                //cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z + yRot * yRot);
                //cam.transform.Translate(Vector3.right * deltaPosition.x * Time.deltaTime,Space.World);
                

            }
            targetPosition = new Vector3(transform.position.x + (yRot * 0.2f), cam.transform.position.y, cam.transform.position.z);
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, 5f * Time.deltaTime);
            cam.transform.position =new Vector3(Mathf.Clamp(cam.transform.position.x, -maxTranslate,maxTranslate),cam.transform.position.y,cam.transform.position.z);

            if (!Input.GetMouseButton(0))
            {
                   if (leftSnap > yRot)
                   {
                       rotation = cam.transform.rotation.eulerAngles;
                       rotation.y = leftSnap - 10;
                       newRot = Quaternion.Euler(rotation);
                       cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, newRot, 20f * Time.deltaTime);
                   }
                  
                   else if (rightSnap < yRot)
                   {
                       rotation = cam.transform.rotation.eulerAngles;
                       rotation.y = rightSnap + 10;
                       newRot = Quaternion.Euler(rotation);
                       cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, newRot, 20f * Time.deltaTime);
                   }
                  
                   else
                   {
                       rotation = cam.transform.rotation.eulerAngles;
                           rotation.y = 0;
                           newRot = Quaternion.Euler(rotation);
                           cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, newRot, 20f * Time.deltaTime);
                       }
        }
    }


    


}
