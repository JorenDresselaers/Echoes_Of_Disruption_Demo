using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyCharacter : BaseCharacter
{
    public static string ENEMY_TAG = "Enemy";

    private NavMeshAgent _agent;
    private PlayerCharacter _playerCharacter;
    private MeleeAttack _meleeAttack;

    protected override void Awake()
    {
        base.Awake();
        _meleeAttack = GetComponentInChildren<MeleeAttack>();
        if(_meleeAttack) _meleeAttack.SetIsAttacking(true);
        _agent = GetComponent<NavMeshAgent>();
        _playerCharacter = FindFirstObjectByType<PlayerCharacter>();
        if (_agent.isActiveAndEnabled && _agent.isOnNavMesh)
        {
            _agent.SetDestination(_playerCharacter.transform.position);
        }
    }

    private void Update()
    {
        if (_agent.isActiveAndEnabled && _agent.isOnNavMesh)
        {
            _agent.SetDestination(_playerCharacter.transform.position);
        }
        if(_offensiveSkill != null) _offensiveSkill.Activate(this);
        if(_defensiveSkill != null) _defensiveSkill.Activate(this);
        if(_utilitySkill != null) _utilitySkill.Activate(this);
    }

    public PlayerCharacter GetPlayerCharacter()
    {
        return _playerCharacter;
    }
}
