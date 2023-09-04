using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField] protected BaseSkill _offensiveSkill;
    [SerializeField] protected BaseSkill _defensiveSkill;
    [SerializeField] protected BaseSkill _utilitySkill;
    [SerializeField] protected List<BaseItem> _items = new List<BaseItem>();

    protected virtual void Awake()
    {
        InitializeSkills();
    }

    public void InitializeSkills()
    {
        if (_offensiveSkill)
        {
            _offensiveSkill = Instantiate(_offensiveSkill, transform);
        }
        if (_defensiveSkill)
        {
            _defensiveSkill = Instantiate(_defensiveSkill, transform);
        }
        if (_utilitySkill)
        {
            _utilitySkill = Instantiate(_utilitySkill, transform);
        }
    }

    public void ClearSkills()
    {
        if (_offensiveSkill)
        {
            Destroy(_offensiveSkill, 0.01f);
        }
        if (_defensiveSkill)
        {
            Destroy(_defensiveSkill, 0.01f);
        }
        if (_utilitySkill)
        {
            Destroy(_utilitySkill, 0.01f);
        }
    }

    public void SetOffeniveSkill(BaseSkill offensiveSkill)
    {
        if (!offensiveSkill) return;
        _offensiveSkill = Instantiate(offensiveSkill, transform);
    }
    
    public void SetDefensiveSkill(BaseSkill defensiveSkill)
    {
        if (!defensiveSkill) return;
        _defensiveSkill = Instantiate(defensiveSkill, transform);
    }
    
    public void SetUtilitySkill(BaseSkill utilitySkill)
    {
        if (!utilitySkill) return;
        _utilitySkill = Instantiate(utilitySkill, transform);
    }

    public BaseSkill GetUtilitySkill()
    {
        return _utilitySkill;
    }

    public BaseSkill GetOffensiveSkill()
    {
        return _offensiveSkill;
    }
    
    public BaseSkill GetDefensiveSkill()
    {
        return _defensiveSkill;
    }
}
