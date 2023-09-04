using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthBoostItem : BaseItem
{
    public int _maxHealthIncrease = 25;

    public override void OnPickup(BaseCharacter character)
    {
        base.OnPickup(character);
        if(character.TryGetComponent(out Health health))
        {
            health.IncreaseMaxHealth(_maxHealthIncrease);
        }
    }

    public override void UpdateValues(BaseCharacter character)
    {
        if (character.TryGetComponent(out Health health))
        {
            health.IncreaseMaxHealth(-_maxHealthIncrease * _previousCount);
            health.IncreaseMaxHealth(_maxHealthIncrease * _count);
        }
    }
}
