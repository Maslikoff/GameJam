using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private PlayerControler _player;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            door.SetActive(true);
            _player = other.GetComponent<PlayerControler>();
            _player.IsRunning = true;
        }
    }
}
