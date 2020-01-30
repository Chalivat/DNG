using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Script : MonoBehaviour
{
    public List<GameObject> front1 = new List<GameObject>();
    public List<GameObject> front2 = new List<GameObject>();
    public List<GameObject> front3 = new List<GameObject>();
    public List<GameObject> distance1 = new List<GameObject>();
    public List<GameObject> distance2 = new List<GameObject>();
    public List<GameObject> distance3 = new List<GameObject>();
    public List<GameObject> artillery1 = new List<GameObject>();
    public List<GameObject> artillery2 = new List<GameObject>();
    public List<GameObject> artillery3 = new List<GameObject>();

    public float rndX;
    public float rndY;

    public Transform WhichUnity(int unit,int type, Transform transform)
    {
        GameObject unitsParent = new GameObject("UnitParent" + transform.ToString());
        unitsParent.transform.SetParent(transform);
        unitsParent.transform.localPosition = Vector3.zero;

        if (unit <= 4)
        {
            SpawnerWeak(unit, type, unitsParent.transform);
        }
        else if(unit > 4 && unit <= 7)
        {
            SpawnerMid(unit, type, unitsParent.transform);
        }
        else
        {
            SpawnerStrong(unit, type, unitsParent.transform);
        }
        return unitsParent.transform;
    }

    Vector3 FinalPosition(Vector3 position)
    {
        float rnd = Random.Range(-rndX, rndX);
        float rnd1 = Random.Range(-rndY, rndY);
        Vector3 finalPosition = position + new Vector3(rnd,0,rnd1);
        return finalPosition;
    }

    void SpawnerWeak(int unitnumber, int type, Transform transform)
    {
        switch (type)
        {
            case 0:
                for (int i = 0; i < unitnumber; i++)
                {
                    GameObject cloneFront = Instantiate(front1[Random.Range(0, front1.Count)], FinalPosition(transform.position), Quaternion.identity,transform);
                    cloneFront.transform.localEulerAngles = new Vector3(-90f, 0, 0);
                }
                break;
            case 1:
                for (int i = 0; i < unitnumber; i++)
                {
                    GameObject cloneDistance = Instantiate(distance1[Random.Range(0, distance1.Count)], FinalPosition(transform.position), Quaternion.identity, transform);
                    cloneDistance.transform.localEulerAngles = new Vector3(-90f, 0, 0);
                }
                break;
            case 2:
                for (int i = 0; i < unitnumber; i++)
                {
                    GameObject cloneArtillery = Instantiate(artillery1[Random.Range(0, artillery1.Count)], FinalPosition(transform.position), Quaternion.identity, transform);
                    cloneArtillery.transform.localEulerAngles = new Vector3(-90f, 0, 0);
                }
                break;
            default:
                print("Nope");
                break;
        }
    }

    void SpawnerMid(int unitnumber, int type, Transform transform)
    {
        switch (type)
        {
            case 0:
                for (int i = 0; i < unitnumber; i++)
                {
                    GameObject cloneFront = Instantiate(front2[Random.Range(0, front1.Count)], FinalPosition(transform.position), Quaternion.identity, transform);
                    cloneFront.transform.localEulerAngles = new Vector3(-90f, 0, 0);
                }
                break;
            case 1:
                for (int i = 0; i < unitnumber; i++)
                {
                    GameObject cloneDistance = Instantiate(distance2[Random.Range(0, distance1.Count)], FinalPosition(transform.position), Quaternion.identity, transform);
                    cloneDistance.transform.localEulerAngles = new Vector3(-90f, 0, 0);
                }
                break;
            case 2:
                for (int i = 0; i < unitnumber; i++)
                {
                    GameObject cloneArtillery = Instantiate(artillery2[Random.Range(0, artillery1.Count)], FinalPosition(transform.position), Quaternion.identity, transform);
                    cloneArtillery.transform.localEulerAngles = new Vector3(-90f, 0, 0);
                }
                break;
            default:
                print("Nope");
                break;
        }
    }

    void SpawnerStrong(int unitnumber, int type, Transform transform)
    {
        switch (type)
        {
            case 0:
                for (int i = 0; i < unitnumber; i++)
                {
                    GameObject cloneFront = Instantiate(front3[Random.Range(0, front1.Count)], FinalPosition(transform.position), Quaternion.identity, transform);
                    cloneFront.transform.localEulerAngles = new Vector3(-90f, 0, 0);
                }
                break;
            case 1:
                for (int i = 0; i < unitnumber; i++)
                {
                    GameObject cloneDistance = Instantiate(distance3[Random.Range(0, distance1.Count)], FinalPosition(transform.position), Quaternion.identity, transform);
                    cloneDistance.transform.localEulerAngles = new Vector3(-90f, 0, 0);
                }
                break;
            case 2:
                for (int i = 0; i < unitnumber; i++)
                {
                    GameObject cloneArtillery = Instantiate(artillery3[Random.Range(0, artillery1.Count)], FinalPosition(transform.position), Quaternion.identity, transform);
                    cloneArtillery.transform.localEulerAngles = new Vector3(-90f, 0, 0);
                }
                break;
            default:
                print("Nope");
                break;
        }
    }
}
