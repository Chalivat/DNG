using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCurve : MonoBehaviour
{
    public enum Curve { linear, ease, easeIn, easeOut, easeInOut , sin, cardExpendCustomCurve};
    public CurvePreset[] allCurvePreset;

    public Dictionary<Curve, AnimationCurve> curves;

    private void Start()
    {
        curves = new Dictionary<Curve, AnimationCurve>();
        for (int i = 0; i < allCurvePreset.Length; i++)
        {
            curves.Add(allCurvePreset[i].curveName, allCurvePreset[i].curve);
        }
    }

    public AnimationCurve GetCurve(LerpCurve.Curve curve)
    {
        AnimationCurve returnedCurve;
        curves.TryGetValue(curve, out returnedCurve);
        return returnedCurve;
    }
}

[System.Serializable]
public class CurvePreset
{
    public LerpCurve.Curve curveName; 
    public AnimationCurve curve;
}
