using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
using Unity.VisualScripting;


public class MyPlayerInputManager : MonoBehaviour
{

    [SerializeField] GameObject playerPrefab;

    [SerializeField] Transform SpawnPoint;

    public event Action<GameObject> joined;
    bool wasdJoined = false;
    bool J1HasJoined = false;
    private HashSet<Gamepad> joinedGamepads = new HashSet<Gamepad>();

    public void AttachScript(GameObject player)
    {
        if (!J1HasJoined)
        {
            player.AddComponent<J1>();
            J1HasJoined = true;
        }
        else
        {
            player.AddComponent<J2>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current == null) return;

        if (!wasdJoined && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            var player = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard", pairWithDevice: Keyboard.current);
            
            player.transform.position = SpawnPoint.position;

            wasdJoined = true;
            AttachScript(player.gameObject);
            
            
            joined?.Invoke(player.gameObject);
            Debug.Log("j'invoque Player dans playerinput "+player.gameObject);
            GameObject text = GameObject.Find("join_message");
            text.SetActive(false);

        }

        foreach (var gamepad in Gamepad.all)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame && !joinedGamepads.Contains(gamepad))
            {
                var player = PlayerInput.Instantiate(playerPrefab, controlScheme: "Gamepad", pairWithDevice: gamepad);
                joinedGamepads.Add(gamepad);

                player.transform.position = SpawnPoint.position;

                AttachScript(player.gameObject);
                joined?.Invoke(player.gameObject);
                Debug.Log("j'invoque Player dans playerinput " + player.gameObject);
                GameObject text = GameObject.Find("join_message");
                text.SetActive(false);
            }
        }


    }
}
