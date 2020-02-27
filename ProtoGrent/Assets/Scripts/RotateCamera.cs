using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateCamera : MonoBehaviour
{

    public GameObject cam;
    public GameObject Anchor;
    [Range(0.0f, 10.0f)] public float minZoom;
    [Range(0.0f, 10.0f)] public float maxZoom;
    [Range(0.0f, 10.0f)] public float sensitivityX;
    [Range(0.0f, 10.0f)] public float sensitivityY;

    public Main_Script mainScript;
    public Holding_Script holdingScript;
    public DefausseDisplay_Script defausseScript;

    public float offset;
    public float travelled;
    private Vector3 position;
    private Vector3 lastPosition;
    private Vector3 deltaPosition;
    private Vector3 targetPosition;
    private float zoom;

    private bool isPlayer1;

    void Start()
    {
        Anchor = cam.transform.parent.gameObject;
    }

    void OnEnable()
    {
        GameManager.newTurn += SwapPlayer;
    }

    void OnDisable()
    {
        GameManager.newTurn -= SwapPlayer;
    }
    
    void Update()
    {
        if (canSwipe())
        {
            Drag();
            Zoom();
        }
        
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
        else
        {
            deltaPosition = Vector3.zero;
        }


        Vector3 rotation = cam.transform.rotation.eulerAngles;
        rotation.y += -deltaPosition.x * sensitivityX * Time.deltaTime;
        rotation.x += deltaPosition.y * sensitivityY * Time.deltaTime;
        Quaternion newRot = Quaternion.Euler(rotation);

        float yRot = cam.transform.rotation.eulerAngles.y;
        if (yRot > 180)
        {
            yRot = -(360 - yRot);
        }

        travelled = yRot;

        Anchor.transform.rotation = newRot;

    }

    void Zoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(-difference * 0.001f);
        }

        Zoom(Input.GetAxis("Mouse ScrollWheel"));

    }

    void Zoom(float value)
    {
        zoom += value;
        //Debug.Log(zoom);
        Vector3 direction = cam.transform.localPosition - Anchor.transform.position;
        direction = direction.normalized;

        //cam.transform.position = direction * zoom;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.transform.Translate(direction * value);
        //Anchor.transform.localScale = new Vector3(zoom,zoom,zoom);
    }

    bool canSwipe()
    {
        if (mainScript.mainIsOpen || holdingScript.isHolding || defausseScript.isOnClick)
        {
            return false;
        }
        else return true;
    }

    void SwapPlayer()
    {
        if (!isPlayer1)
        {
            Anchor.transform.rotation = Quaternion.identity;
            isPlayer1 = !isPlayer1;
        }
        else
        {
            Anchor.transform.rotation = Quaternion.identity * Quaternion.Euler(0,180,0);
            isPlayer1 = !isPlayer1;
        }
    }

    
}
