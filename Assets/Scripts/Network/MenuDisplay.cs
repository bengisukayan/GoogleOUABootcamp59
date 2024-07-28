using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System;

public class MenuDisplay : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject connectingPanel;
    [SerializeField] private GameObject menuPaneL;
    [SerializeField] private TMP_InputField joinCodeInputField;

    private async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log($"Player ID: {AuthenticationService.Instance.PlayerId}");
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return;
        }

        connectingPanel.SetActive(false);
        menuPaneL.SetActive(true);
    }

    public void StartHost()
    {
        ServerManager.Instance.StartHost();
    }

    public void StartClient()
    {
        ClientManager.Instance.StartClient(joinCodeInputField.text);
    }
}
