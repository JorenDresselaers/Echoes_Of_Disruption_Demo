using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostItem : BaseItem
{
    public float _speedIncrease = 1f;

    public override void OnPickup(BaseCharacter character)
    {
        base.OnPickup(character);
        if (character.TryGetComponent(out Movement movement))
        {
            movement._movementSpeed += _speedIncrease;
        }
    }

    public override void UpdateValues(BaseCharacter character)
    {
        if (character.TryGetComponent(out Movement movement))
        {
            movement._movementSpeed += _speedIncrease * _previousCount;
            movement._movementSpeed += _speedIncrease * _count;
        }
    }
}
