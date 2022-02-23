using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMoonlight : MonoBehaviour
{
    [SerializeField] private float _maxMoonlight = 100f;
    [SerializeField] private float _current = 0f;

    public float Percentage => _current / _maxMoonlight;

    public UnityEvent<float> ChargeMoonlight;

    public void ChargeUp(float amount)
    {
        _current = Mathf.Clamp(_current + amount, 0f, _maxMoonlight);
        ChargeMoonlight.Invoke(Percentage);
    }
}