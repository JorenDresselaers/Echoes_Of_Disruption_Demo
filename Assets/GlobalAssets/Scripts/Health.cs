using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour, IHittable
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private bool _doesRegen = false;
    [SerializeField] private float _regenTime = 5f;
    private float _currentRegenTime;

    [SerializeField] private float _flashDuration = 0.1f;
    [SerializeField] private Material _onHitFlashMaterial;
    [SerializeField] private Material _normalMaterial;
    [SerializeField] private MeshRenderer _meshToFlash;
    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _currentRegenTime = _regenTime;
    }

    private void Update()
    {
        if (_doesRegen)
        {
            _currentRegenTime -= Time.deltaTime;
            if (_currentRegenTime <= 0)
            {
                _currentRegenTime = _regenTime;
                if (_currentHealth < _maxHealth)
                {
                    _currentHealth++;
                }
            }
        }
    }

        public void Damage(int damage)
    {
        _currentHealth -= damage;
        FlashOnDamaged();
        if(_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int heal)
    {
        _currentHealth += heal;
        if(_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    public void Die()
    {
        Destroy(gameObject, 0.01f);
    }

    public void HitObject(int damage)
    {
        Damage(damage);
    }

    private void FlashOnDamaged()
    {
        if (!_onHitFlashMaterial || !_meshToFlash) return;

        if(!_normalMaterial) _normalMaterial = _meshToFlash.material;

        _meshToFlash.material = _onHitFlashMaterial;
        Invoke(RESET_MATERIAL_FUNCTION, _flashDuration);
    }

    const string RESET_MATERIAL_FUNCTION = "ResetMaterial";
    private void ResetMaterial()
    {
        if (!_normalMaterial) return;

        _meshToFlash.material = _normalMaterial;
    }

    public void SetMaxHealth(int max)
    {
        _maxHealth = max;
    }

    public void IncreaseMaxHealth(int increase)
    {
        _maxHealth += increase;
        _currentHealth += increase;
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }
    
    public int GetMaxHealth()
    {
        return _maxHealth;
    }
}
