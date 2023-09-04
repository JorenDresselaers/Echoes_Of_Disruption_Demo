using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    [SerializeField] protected float _cooldown = 5.0f;
    protected float _timer;
    protected bool _canActivate = true;
    [SerializeField] protected SkillType _skillType = SkillType.Null;
    [SerializeField] protected bool _isChargedAbility = false;
    protected bool _activated = false;

    public enum SkillType
    {
        Null,
        Offensive,
        Defensive,
        Utility
    }
    protected virtual void Start()
    {
    }

    protected virtual void Awake()
    {
        //a small reminder in case you make new skills without assigning types
        if(_skillType == SkillType.Null)
        Debug.Log("Basic skill loaded. Forgot to specify?");
    }

    protected virtual void Update()
    {
        //handling the cooldown timer here so derived skills don't have to
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _timer = 0;
            _canActivate = true;
        }
    }

    public virtual void StartCharging()
    {
    }

    public virtual void StopCharging()
    {
    }

    //empty since activation is specific for every skill
    public virtual void Activate(BaseCharacter character = null)
    {
        _activated = true;
        _timer = _cooldown;
        _canActivate = false;
    }

    public virtual void Deactivate(BaseCharacter character = null)
    {
        if (!_activated) return;
    }

    public bool CanActivate()
    {
        return _canActivate;
    }

    public virtual void SetSkill()
    {
        _skillType = SkillType.Null;
    }

    public SkillType GetSkillType()
    {
        return _skillType;
    }
}
