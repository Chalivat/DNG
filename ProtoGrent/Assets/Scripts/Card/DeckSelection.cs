using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSelection : MonoBehaviour
{
    public static DeckSelection instance;

    public GameObject deckPrefab;
    public Transform deckParent;
    private Vector3[] spawnPos = { new Vector3(-300, -40, 0), new Vector3(-150, -40, 0), new Vector3(-0, -40, 0) };

    public Button launchButton;
    public int selectedDeckPower = 0;

    private void Awake()
    {
        launchButton.onClick.AddListener(LaunchButton);

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

    public void LaunchButton()
    {
        if(selectedDeckPower > 0)
        ClientSend.SearchMatch(selectedDeckPower);
        launchButton.interactable = false;
    }

    public void InitializeDeck()
    {
        Deck[] _decks = AllDecks.instance.allDeck.ToArray(); 

        for (int i = 0; i < _decks.Length; i++)
        {
            SpawnDeck(_decks[i], spawnPos[i]);
        }    
    }

    public void SpawnDeck(Deck _deck, Vector3 _pos)
    {
        GameObject deck = Instantiate(deckPrefab, deckParent);
        deck.transform.localPosition = _pos;
        deck.GetComponent<DeckPrefab>().DrawDeck(_deck, null);
    }
}
