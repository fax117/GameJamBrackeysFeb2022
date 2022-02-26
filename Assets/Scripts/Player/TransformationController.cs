using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationController : MonoBehaviour
{
    [SerializeField] private float _wolfModeDuration = 10f;
    [SerializeField] private float _cooldownTimer = 7f;

    private Animator _characterAnimator;
    private ShootingController _shootingController;
    private PlayerHealth _playerHealth;

    private float lifeDrained;

    public bool IsWolfModeActive { get; set; } = false;
    public bool IsOnCooldown { get; set; } = false;

    private void Start()
    {
        _characterAnimator = GetComponent<Animator>();
        _shootingController = GetComponent<ShootingController>();
        _playerHealth = GetComponent<PlayerHealth>();

    }

    private IEnumerator OnCooldown()
    {
        IsOnCooldown = true;
        yield return new WaitForSeconds(_cooldownTimer);
        IsOnCooldown = false;
    }

    private IEnumerator TransformCountdown()
    {
        IsWolfModeActive = true;
        _characterAnimator.SetTrigger("Transform");
        _shootingController.rpm = 360; //might go on damage controller for tweaking
        yield return new WaitForSeconds(_wolfModeDuration);
        _characterAnimator.SetTrigger("TransformToHuman"); 
        IsWolfModeActive = false;
        _shootingController.rpm = 240;
        _playerHealth.GetHealth(_playerHealth._dmgDealtAccumulator);
    }

    public void CallTransformCountdownCoroutine()
    {
        StartCoroutine(TransformCountdown());
    }

    public void CallOnCooldownCoroutine()
    {
        StartCoroutine(OnCooldown());
    }

}
