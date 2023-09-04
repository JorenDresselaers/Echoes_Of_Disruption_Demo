using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    //settings
    protected int _count = 1;
    protected int _previousCount = 0;
    [SerializeField] private string _name = "Unnamed Item";

    public enum ItemType
    {
        Passive,
        OnAttack,
        OnHit,
        Periodic
    }

    [SerializeField] protected ItemType _type;

    //chance on event
    [SerializeField] protected float _procChance = 0f;

    //effects over time
    [SerializeField] protected bool _hasPeriodicEffect = false;
    [SerializeField] private float _totalCooldown = 1f;
    private float _currentCooldown;

    protected void Update()
    {
        if (_type == ItemType.Periodic)
        {
            _currentCooldown -= Time.deltaTime;
            if (_currentCooldown <= 0)
            {
                _currentCooldown = _totalCooldown;
                OnPeriodic();
            }
        }
    }

    protected virtual void OnPeriodic()
    {
    }

    public virtual void OnPickup(BaseCharacter character)
    {
    }

    public virtual void OnAttack(BaseCharacter character)
    {
    }

    public virtual void OnHit(BaseCharacter character)
    {
    }

    public void AddCount(int toAdd)
    {
        _previousCount = _count;
        _count += toAdd;
    }

    public void RemoveCount(int toRemove)
    {
        _previousCount = _count;
        _count -= toRemove;
    }

    public virtual void UpdateValues(BaseCharacter character)
    {

    }

    public int GetCount()
    {
        return _count;
    }

    public string GetName()
    {
        return _name;
    }
}
