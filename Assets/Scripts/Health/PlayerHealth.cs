using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _curHealth = 10f;

    public float Percentage => _curHealth / _maxHealth;
    public float Current => _curHealth;

    public UnityEvent<float> OnDamaged;
    
    public void DealDamage(float damageValue)
    {
        _curHealth = Mathf.Clamp(_curHealth - damageValue, 0f, _maxHealth);
        OnDamaged.Invoke(Percentage);
    }

    public void GetHealth(float healingValue)
    {
        _curHealth = Mathf.Clamp(_curHealth + healingValue, 0f, _maxHealth);
        OnDamaged.Invoke(Percentage);
    }

    //debugging
    public void OnDebug()
    {
        //DealDamage(Random.Range(5, 15));
        GetHealth(5);
    }


}
