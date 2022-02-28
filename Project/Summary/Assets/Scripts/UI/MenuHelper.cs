using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class MenuHelper : MonoBehaviourPun
{
    [SerializeField] private GameObject MenuInterface;
    [SerializeField] private GameObject InGameInterface;
    [SerializeField] private Button StartRaceButton;
    private Race _race; 

    void Start()
    {
        _race = FindObjectOfType<Race>();
    }

    private void OnEnable()
    {
        _race = FindObjectOfType<Race>();
        StartRaceButton.onClick.AddListener(_race.StartRace);
    }

    private void OnDisable()
    {
        StartRaceButton.onClick.RemoveListener(_race.StartRace);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !MenuInterface.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            MenuInterface.SetActive(true);
            InGameInterface.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && MenuInterface.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            MenuInterface.SetActive(false);
            InGameInterface.SetActive(true);
        }
    }

    public void ReturnToMainMenu()
    {
        PhotonNetwork.Disconnect();
    }
}