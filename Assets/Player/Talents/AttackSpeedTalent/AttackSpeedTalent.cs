using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedTalent : BaseTalent
{
    //BROKEN
    [SerializeField] protected float _attackSpeedIncrease = 0.9f;

    public override void Initialize(PlayerCharacter player)
    {
        if (!_isEnabled || _rank <= 0) return;
        player.IncreaseAttackSpeed(Mathf.Pow(1 - _attackSpeedIncrease, _rank));
        print("Attack speed increased by: " + Mathf.Pow(_attackSpeedIncrease, _rank) + "\nTotal is now: " + player.GetAttackSpeed());
    }
}
