using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationController : MonoBehaviour
{
    [SerializeField] private float _wolfModeDuration = 10f;
    [SerializeField] private float _cooldownTimer = 7f;

    private Animator _characterAnimator;

    public bool IsWolfModeActive { get; set; } = false;
    public bool IsOnCooldown { get; set; } = false;

    private void Start()
    {
        _characterAnimator = GetComponent<Animator>();
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
        _characterAnimator.SetTrigger("Transform"); //here goes the animation human -> werewolf
        yield return new WaitForSeconds(_wolfModeDuration);
        _characterAnimator.SetTrigger("TransformToHuman"); //here goes the animation werewolg -> human
        IsWolfModeActive = false;
    }

    public void CallTransformCountdownCoroutine()
    {
        StartCoroutine(TransformCountdown());
    }

    public void CallOnCooldownCoroutine()
    {
        StartCoroutine(OnCooldown());
    }

    public void Test()
    {
        Debug.Log("Funciona");
    }
}
