using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;
    public InputField emailField;

    public Text label;

    public Button submitButton;

    public void CallRegister()
    {
        //StartCoroutine(Register());
        ClientSend.Register();
    }

    public void VerifyInput()
    {
        submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8 && emailField.text.Length > 0);
    }
}
