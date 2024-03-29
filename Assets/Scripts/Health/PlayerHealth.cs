using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _curHealth = 10f;
    [SerializeField] private ParticleSystem _bloodEffect;
    public float _dmgDealtAccumulator = 0f;

    [SerializeField] private float _timeBetweenDamage = 0.25f;
    [SerializeField] private SpriteRenderer _characterRenderer;

    private Animator _characterAnimations;
    private PlayerAudioEffects _audioEffects;
    private bool _isDead = false;

    public float Percentage => _curHealth / _maxHealth;
    public float Current => _curHealth;
    public bool IsDead => _isDead;

    public UnityEvent<float> OnDamaged;
    public UnityEvent OnDeath;

    private AudioSource _audioSource;

    private void Start()
    {
        _characterAnimations = GetComponent<Animator>();
        _characterRenderer = GetComponent<SpriteRenderer>();
        _audioEffects = GetComponent<PlayerAudioEffects>();
        _audioSource = GetComponent<AudioSource>();
        _isDead = false;
    }

    public void DealDamage(float damageValue)
    {
        Instantiate(_bloodEffect, gameObject.transform.position, gameObject.transform.rotation);
        _audioEffects.HurtEffect();
        _curHealth = Mathf.Clamp(_curHealth - damageValue, 0f, _maxHealth);
        OnDamaged.Invoke(Percentage);

    }

    public void AccumulateHealth(float dmgDealt)
    {
        _dmgDealtAccumulator = Mathf.Clamp(_dmgDealtAccumulator + dmgDealt, 0f, _maxHealth);
    }

    public void GetHealth(float healingValue)
    {
        _curHealth = Mathf.Clamp(_curHealth + healingValue, 0f, _maxHealth);
        OnDamaged.Invoke(Percentage);
    }

    public void PlayerDeath()
    {
        if (Current <= 0) 
        {
            _isDead = true;
            _characterAnimations.SetTrigger("IsDead");
            _audioEffects.DyingEffect();
            StartCoroutine(DeathTimer());
            OnDeath.Invoke();
        } 
    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(5.0f);
    }

    //debugging
    public void OnDebug()
    {
        //DealDamage(Random.Range(5, 15));
        DealDamage(5);
    }


}
