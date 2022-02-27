using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Moonlight : MonoBehaviour
{
    [Header("Moonlight Params")]
    [SerializeField] private float _energyGranted = 13.0f;

    private GameObject _player;
    private PlayerMoonlight _playerMoonlight;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerMoonlight = _player.GetComponent<PlayerMoonlight>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        bool isOnCooldown = false;
        bool isInWolfMode = false;

        if (collision.gameObject.TryGetComponent(out TransformationController tc))
        {
            isOnCooldown = tc.IsOnCooldown;
            isInWolfMode = tc.IsWolfModeActive;
        }

        if (_playerMoonlight.Current == 0 && isInWolfMode) _playerMoonlight.ResetBar();

        if (!collision.gameObject.CompareTag("Player") || isOnCooldown || isInWolfMode) return;

        _playerMoonlight.ChargeUp(_energyGranted * Time.deltaTime);
    }
}
