using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnd : MonoBehaviour
{
    public event EventHandler OnPlayerEnterTriggerEnd;
    [SerializeField] RoomController roomController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        roomController.EndGame();   
    }
}
