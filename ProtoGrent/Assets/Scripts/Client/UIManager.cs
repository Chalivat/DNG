using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject mainMenu;
    public GameObject registerMenu;
    public GameObject loginMenu;

    public GameObject deckSelectionMenu;

    public InputField Register_usernameField;
    public InputField Register_passwordField;
    public InputField Register_emailField;

    public InputField Login_usernameField;
    public InputField Login_passwordField;

    public InputField DeckSelectionField;

    public Text label;

    private void Awake()
    {
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

    public void LaunchMatch()
    {
        Client.instance.LaunchMatchmaking();
    }

    public void GoToMain()
    {
        mainMenu.SetActive(true);
    }

    public void GoToRegister()
    {
        mainMenu.SetActive(false);

        registerMenu.SetActive(true);
    }

    public void GoToLogin()
    {
        mainMenu.SetActive(false);

        loginMenu.SetActive(true);
    }

    public void GoToDeckSelection()
    {
        loginMenu.SetActive(false);

        deckSelectionMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        loginMenu.SetActive(false);
        registerMenu.SetActive(false);
        deckSelectionMenu.SetActive(false);

        mainMenu.SetActive(true);
    }
}
