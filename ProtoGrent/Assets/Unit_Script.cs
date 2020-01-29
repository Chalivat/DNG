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

    public int u;
    public int t;
    public Transform p;

    public float rndX;
    public float rndY;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            WhichUnity(u,t,p);
        }
    }

    void WhichUnity(int unit,int type, Transform position)
    {
        if(unit <= 4)
        {
            SpawnerWeak(unit, type, position);
        }
        else if(unit > 4 && unit <= 7)
        {
            SpawnerMid(unit, type, position);
        }
        else
        {
            SpawnerStrong(unit, type, position);
        }
    }

    Vector3 FinalPosition(Transform position)
    {
        float rnd = Random.Range(-rndX, rndX);
        float rnd1 = Random.Range(-rndY, rndY);
        Vector3 finalPosition = new Vector3(position.position.x + rnd, position.position.y, position.position.z + rnd1);
        return finalPosition;
    }

    void SpawnerWeak(int unitnumber, int type, Transform position)
    {
        switch (type)
        {
            case 0:
                for (int i = 0; i < unitnumber; i++)
                {
                    GameObject cloneFront = Instantiate(front1[Random.Range(0, front1.Count)], FinalPosition(position), Quaternion.identity);
                }
                break;
            case 1:
                for (int i = 0; i < unitnumber; i++)
                {
                    GameObject cloneDistance = Instantiate(distance1[Random.Range(0, distance1.Count)], FinalPosition(position), Quaternion.identity);
                }
                break;
            case 2:
                for (int i = 0; i < unitnumber; i++)
                {
                    GameObject cloneArtillery = Instantiate(artillery1[Random.Range(0, artillery1.Count)], FinalPosition(position), Quaternion.identity);
                }
                break;
            default:
                print("Nope");
                break;
        }
    }

    void SpawnerMid(int unitnumber, int type, Transform position)
    {
        switch (type)
        {
            case 0:
                for (int i = 0; i < unitnumber; i++)
                {
                    GameObject cloneFront = Instantiate(front2[Random.Range(0, front1.Count)], FinalPosition(position), Quaternion.identity);
                }
                break;
            case 1:
                for (int i = 0; i < unitnumber; i++)
                {
                    GameObject cloneDistance = Instantiate(distance2[Random.Range(0, distance1.Count)], FinalPosition(position), Quaternion.identity);
                }
                break;
            case 2:
                for (int i = 0; i < unitnumber; i++)
                {
                    GameObject cloneArtillery = Instantiate(artillery2[Random.Range(0, artillery1.Count)], FinalPosition(position), Quaternion.identity);
                }
                break;
            default:
                print("Nope");
                break;
        }
    }

    void SpawnerStrong(int unitnumber, int type, Transform position)
    {
        switch (type)
        {
            case 0:
                for (int i = 0; i < unitnumber; i++)
                {
                    GameObject cloneFront = Instantiate(front3[Random.Range(0, front1.Count)], FinalPosition(position), Quaternion.identity);
                }
                break;
            case 1:
                for (int i = 0; i < unitnumber; i++)
                {
                    GameObject cloneDistance = Instantiate(distance3[Random.Range(0, distance1.Count)], FinalPosition(position), Quaternion.identity);
                }
                break;
            case 2:
                for (int i = 0; i < unitnumber; i++)
                {
                    GameObject cloneArtillery = Instantiate(artillery3[Random.Range(0, artillery1.Count)], FinalPosition(position), Quaternion.identity);
                }
                break;
            default:
                print("Nope");
                break;
        }
    }
}
