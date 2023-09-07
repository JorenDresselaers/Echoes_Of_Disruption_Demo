using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoostTalent : BaseTalent
{
    [SerializeField] protected int _healthBoost = 10;

    public override void Initialize(PlayerCharacter player)
    {
        if (!_isEnabled || _rank <= 0) return;

        Health health = player.GetHealth();
        health.SetMaxHealth(health.GetMaxHealth() + _healthBoost * _rank);
        health.Heal(health.GetMaxHealth());
    }
}
