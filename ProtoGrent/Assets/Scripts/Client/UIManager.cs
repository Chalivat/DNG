using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject connectionMenu;
    public GameObject registerMenu;
    public GameObject loginMenu;
    public GameObject mainMenu;

    public GameObject deckSelectionMenu;

    public InputField Register_usernameField;
    public InputField Register_passwordField;
    public InputField Register_emailField;

    public InputField Login_usernameField;
    public InputField Login_passwordField;

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

    public void GoToConnection()
    {
        connectionMenu.SetActive(true);
    }

    public void GoToRegister()
    {
        connectionMenu.SetActive(false);

        registerMenu.SetActive(true);
    }

    public void GoToLogin()
    {
        connectionMenu.SetActive(false);

        loginMenu.SetActive(true);
    }

    public void GoToDeckSelection()
    {
        mainMenu.SetActive(false);

        deckSelectionMenu.SetActive(true);
    }

    public void GoToMain()
    {
        loginMenu.SetActive(false);

        mainMenu.SetActive(true);
    }

    public void BackToConnectionMenu()
    {
        loginMenu.SetActive(false);
        registerMenu.SetActive(false);

        connectionMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        deckSelectionMenu.SetActive(false);

        mainMenu.SetActive(true);
    }
}
