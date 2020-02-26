using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpManager : MonoBehaviour
{
    GameObject lerpInstance;

    public Vector3 startPos, endPos;
    public Transform lerpObject;

    public float lerpTime;

    public bool isLocalSpace;
    public bool normalizeDistance;

    LerpCurve.Curve curve;

    public LerpManager(Vector3 startPos,Vector3 endPos,Transform lerpObject,float lerpTime,bool isLocalSpace,bool normalizeDistance,LerpCurve.Curve curve)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        this.lerpObject = lerpObject;
        this.lerpTime = lerpTime;

        this.isLocalSpace = isLocalSpace;
        this.normalizeDistance = normalizeDistance;

        this.curve = curve;
    }

    public void StartLerp()
    {
        lerpInstance = new GameObject("LerpInstance");
        lerpInstance.AddComponent<LerpClass>().SetLerpValue(startPos, endPos, lerpObject, lerpTime, isLocalSpace, normalizeDistance, curve, this);
    }

    public void CancelLerp()
    {
        Destroy(lerpInstance);
    }

    ~LerpManager()
    {
        Debug.Log("LERP END");
        Destroy(lerpInstance);
    }
}

public class LerpClass : MonoBehaviour
{
    float lerpPos;

    Vector3 startPos;
    Vector3 endPos;
    public Transform lerpObject;

    float lerpTime;
    float lerpSpeed;

    bool isLocalSpace;
    bool normalizeDistance;

    AnimationCurve animCurve;

    LerpManager lerpManager;

    public void SetLerpValue(Vector3 startPos,Vector3 endPos, Transform lerpObject, float lerpTime,bool isLocalSpace, bool normalizeDistance ,LerpCurve.Curve curve,LerpManager lerpManager)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        this.lerpObject = lerpObject;
        this.lerpTime = lerpTime;
        this.lerpManager = lerpManager;

        this.isLocalSpace = isLocalSpace;
        this.normalizeDistance = normalizeDistance;

        animCurve = GameObject.FindObjectOfType<LerpCurve>().GetCurve(curve);

        lerpPos = 0;

        TestAllLerpInstance();
    }

    void TestAllLerpInstance()
    {
        LerpClass[] allLerpInstance = GameObject.FindObjectsOfType<LerpClass>();
        for (int i = 0; i < allLerpInstance.Length; i++)
        {
            if (allLerpInstance[i].lerpObject == lerpObject && allLerpInstance[i] != this)
            {
                endPos = allLerpInstance[i].GetObjectStartPos();
                Destroy(allLerpInstance[i].gameObject);
            }
        }
        CalculeSpeed();
    }

    void CalculeSpeed()
    {
        if (normalizeDistance)
            lerpSpeed = 1 / lerpTime;
        else
            lerpSpeed = (Vector3.Distance(startPos, endPos) / lerpTime);
    }

    private void Update()
    {
        if (lerpObject == null)
            Destroy(gameObject);

        float percent = Mathf.Clamp01((lerpPos / lerpTime));

        if (isLocalSpace)
        {
            lerpObject.localPosition = Vector3.Lerp(startPos, endPos, animCurve.Evaluate(percent));
            if (lerpObject.localPosition == endPos)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            lerpObject.position = Vector3.Lerp(startPos, endPos, animCurve.Evaluate(percent));
            if (lerpObject.position == endPos)
            {
                Destroy(gameObject);
            }
        }

        lerpPos += lerpSpeed * Time.deltaTime;
    }

    public Vector3 GetObjectStartPos()
    {
        return startPos;
    }

    private void OnDestroy()
    {
        Destroy(lerpManager);
    }
}
