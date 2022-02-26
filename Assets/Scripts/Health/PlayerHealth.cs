using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _curHealth = 10f;

    [SerializeField] private float _timeBetweenDamage = 0.25f;
    [SerializeField] private Sprite _playerDeath;
    
    private SpriteRenderer _characterRenderer;

    public float Percentage => _curHealth / _maxHealth;
    public float Current => _curHealth;

    public UnityEvent<float> OnDamaged;

    private void Start()
    {
        _characterRenderer = GetComponent<SpriteRenderer>();
    }

    public void DealDamage(float damageValue)
    {
        _curHealth = Mathf.Clamp(_curHealth - damageValue, 0f, _maxHealth);
        OnDamaged.Invoke(Percentage);

        if (_curHealth <= 0) _characterRenderer.sprite = _playerDeath;
    }

    public void GetHealth(float healingValue)
    {
        _curHealth = Mathf.Clamp(_curHealth + healingValue, 0f, _maxHealth);
        OnDamaged.Invoke(Percentage);
    }

    private IEnumerator FlashOnDamageCoroutine()
    {
        Color originalColor = _characterRenderer.color;
        _characterRenderer.color = Color.red;
        yield return new WaitForSeconds(_timeBetweenDamage);
        _characterRenderer.color = originalColor;
    }

    public void FlashOnDamage()
    {
        StartCoroutine(FlashOnDamageCoroutine());
    }

    //debugging
    public void OnDebug()
    {
        //DealDamage(Random.Range(5, 15));
        GetHealth(5);
    }


}
