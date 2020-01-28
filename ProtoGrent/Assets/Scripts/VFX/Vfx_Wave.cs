using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vfx_Wave : MonoBehaviour
{
    public AnimationCurve XposCurve, YposCurve;
    public AnimationCurve ZrotCurve;

    public float animDuration = 2f;
    public float animSpeed = .1f;

    public float animPos = 0f;

    private void FixedUpdate()
    {
        transform.position += Vector3.up * (YposCurve.Evaluate(animPos) / animDuration);
        transform.position += Vector3.right * (XposCurve.Evaluate(animPos) / animDuration);

        transform.eulerAngles = new Vector3((ZrotCurve.Evaluate(animPos) / animDuration * 360f), -90f,0);

        animPos += (Time.deltaTime * animSpeed);
    }
}
