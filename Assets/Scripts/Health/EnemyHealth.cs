using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _curHealth;

    [SerializeField] private float _maxArmor;
    [SerializeField] private float _curArmor;

    [SerializeField] private float _timeBetweenDamage = 0.25f;
    [SerializeField] private SpriteRenderer _characterRenderer;

    private DamageController _damageController;
    private PlayerAudioEffects _audioEffects;

    private Animator _characterAnimations;
    private bool _isDead = false;

    public float PercentageHP => _curHealth / _maxHealth;
    public float PercentageAP => _curArmor / _maxArmor;
    public float CurrentHP => _curHealth;
    public float CurrentAP => _curArmor;

    public bool IsDead => _isDead;

    public UnityEvent<float> OnDamaged;
    public UnityEvent OnDeath;

    private bool armored;

    // Start is called before the first frame update
    void Start()
    {
        _characterAnimations = GetComponent<Animator>();
        _characterRenderer = GetComponent<SpriteRenderer>();
        _damageController = GetComponent<DamageController>();
        _audioEffects = GetComponent<PlayerAudioEffects>();

        _isDead = false;

        _curHealth = _maxHealth;

        if (this.gameObject.CompareTag("Armor"))
        {
            armored = true;
            _curArmor = _maxArmor;
        }
            
        else
            armored = false;

    }

    public void DealDamage(float damageValue, float armorDamage)
    {
        //_audioEffects.HurtEffect();
        if(CurrentAP <= 0)
        {
            FlashOnDamage();
            _curHealth = Mathf.Clamp(_curHealth - damageValue, 0f, _maxHealth);
            OnDamaged.Invoke(PercentageHP);
        }
        else
        {
            DealDamageArmor(armorDamage);
        }
    }

    public void DealDamageArmor(float damageValue)
    {
        FlashOnDamage();
        _curArmor = Mathf.Clamp(_curArmor - damageValue, 0f, _maxArmor);
        OnDamaged.Invoke(PercentageAP);
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

    public void Death()
    {
        if (CurrentHP <= 0)
        {
            _isDead = true;
            _characterAnimations.SetTrigger("IsDead");
            StartCoroutine(DeathTimer());
            OnDeath.Invoke();
        }
    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(5.0f);
    }
}
