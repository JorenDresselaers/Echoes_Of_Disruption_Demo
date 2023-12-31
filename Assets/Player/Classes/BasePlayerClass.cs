using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerClass : MonoBehaviour
{
    [SerializeField] protected BaseSkill _offensiveSkill;
    [SerializeField] protected BaseSkill _defensiveSkill;
    [SerializeField] protected BaseSkill _utilitySkill;
    [SerializeField] protected bool _isMelee;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _attackSpeed;

    public void Initialize(PlayerCharacter player)
    {
        player.ClearSkills();
        player._isMelee = _isMelee;
        player._baseAttackSpeed = _attackSpeed;
        player.SetAttackSpeed(_attackSpeed);

        player.SetOffeniveSkill(_offensiveSkill);
        player.SetDefensiveSkill(_defensiveSkill);
        player.SetUtilitySkill(_utilitySkill);
    }
}
