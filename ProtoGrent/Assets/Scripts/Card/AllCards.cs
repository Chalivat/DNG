using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCards : MonoBehaviour
{
    public static AllCards instance;

    public List<cardsList> cardsList = new List<cardsList>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void InitializeCards(string _msg)
    {
        for (int i = 0; i < cardsList.Count; i++)
        {
            cardsList[i].unlocked = _msg[i] == '0'? false : true;
        }
    }
}

[System.Serializable]
public class cardsList
{
    public bool unlocked;
    public Card card;
}
