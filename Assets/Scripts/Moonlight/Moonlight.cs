using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Moonlight : MonoBehaviour
{
    [Header("Moonlight Params")]
    [SerializeField] private float _energyGranted = 5.0f;
    [SerializeField] private float _timeBetweenEachChargeUp = 0.5f;

    private GameObject _player;
    private PlayerMoonlight _playerMoonlight;

    private bool _isStandingOnMoonlight;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerMoonlight = _player.GetComponent<PlayerMoonlight>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        _isStandingOnMoonlight = true;
        StartCoroutine(ChargeBar());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        _isStandingOnMoonlight = false;
        StopCoroutine(ChargeBar());
    }

    private IEnumerator ChargeBar()
    {
        while (true)
        {
            if (_isStandingOnMoonlight)
            {
                _playerMoonlight.ChargeUp(_energyGranted);
                yield return new WaitForSeconds(_timeBetweenEachChargeUp);
            }
            yield return null;
        }
    }
}
