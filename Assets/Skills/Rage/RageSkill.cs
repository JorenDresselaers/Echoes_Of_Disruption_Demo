using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageSkill : BaseSkill
{
    public float _duration = 5f;
    public float _attackSpeedMultiplier = 2f;
    public float _healthCostPercent = 25f;
    PlayerCharacter _character = null;

    public override void Activate(BaseCharacter character)
    {
        if (!_canActivate) return;

        _character = character as PlayerCharacter;
        if (!_character) return;

        base.Activate(character);

        _character.GetHealth().Damage((int)(_character.GetHealth().GetMaxHealth() / (100 / _healthCostPercent)));
        _character.SetAttackSpeed(_character.GetBaseAttackSpeed() / _attackSpeedMultiplier);

        Invoke(RESETATTACKSPEED_FUNCTION, _duration);
    }

    const string RESETATTACKSPEED_FUNCTION = "ResetAttackSpeed";
    private void ResetAttackSpeed()
    {
        _character.ResetAttackSpeed();
    }
}
