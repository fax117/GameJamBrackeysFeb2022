using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private float _dampTime = 0.1f;

    private Animator _characterAnimator;
    private PlayerMoonlight _playerMoonlight;
    private CharacterMovement _characterMovement;


    private void Start()
    {
        _characterAnimator = GetComponent<Animator>();
        _playerMoonlight = GetComponent<PlayerMoonlight>();
        _characterMovement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        PlayerMoving();
    }

    public void PlayerMoving()
    {
        Vector3 velocity = _characterMovement.Velocity;
        Vector3 flattenedVelocity = new Vector3(velocity.x, velocity.y, 0f);
        float speed = Mathf.Min(_characterMovement.MoveInput.magnitude, flattenedVelocity.magnitude / _characterMovement.Speed);
        _characterAnimator.SetFloat("Speed", speed);
    }

    public void TransformAnimEnded()
    {
        _characterAnimator.SetBool("IsWerewolf", true);
    }

    public void TransformHumanAnimEnded()
    {
        _characterAnimator.SetBool("IsWerewolf", false);
    }

}
