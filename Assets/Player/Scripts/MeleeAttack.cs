using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeAttack : MonoBehaviour
{
    private Collider _meleeCollider;

    public bool _isPlayer = true;
    public int _damage = 1;
    [SerializeField] private float _totalAttackCooldown = 0.1f;
    private float _currentAttackCooldown;
    private bool _isAttacking = false;

    private List<GameObject> _inRangeObjects = new List<GameObject>();
    private PlayerCharacter _player;

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!_meleeCollider) return;

        if (context.performed)
        {
            //_meleeCollider.enabled = true;
            _isAttacking = true;
            _currentAttackCooldown = 0;
        }
        else if (context.canceled)
        {
            //_meleeCollider.enabled = false;
            _isAttacking = false;
        }
    }

    public void SetIsAttacking(bool isAttacking)
    {
        _isAttacking = isAttacking;
    }

    private void Awake()
    {
        _meleeCollider = GetComponent<Collider>();
        //if (_meleeCollider) _meleeCollider.enabled = false;
    }

    private void Update()
    {
        if (!_isAttacking) return;

        _currentAttackCooldown -= Time.deltaTime;

        if (_currentAttackCooldown <= 0)
        {
            _currentAttackCooldown = _totalAttackCooldown;
            Attack();
        }
    }

    private void Attack()
    {
       if(_player) _player.TriggerOnAttack();
        for(int currentObject = 0; currentObject < _inRangeObjects.Count; ++currentObject)
        {
            if (_inRangeObjects[currentObject] == null)
            {
                _inRangeObjects.RemoveAt(currentObject);
                continue;
            }

            if (_inRangeObjects[currentObject].TryGetComponent(out IHittable hittableObject))
            {
                hittableObject.HitObject(_damage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isPlayer && other.tag == PlayerCharacter.PLAYER_TAG) return;
        if (!_isPlayer && other.tag == EnemyCharacter.ENEMY_TAG) return;

        _inRangeObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isPlayer && other.tag == PlayerCharacter.PLAYER_TAG) return;
        if (!_isPlayer && other.tag == EnemyCharacter.ENEMY_TAG) return;


        _inRangeObjects.Remove(other.gameObject);
    }

    public void SetAttackSpeed(float speed)
    {
        _totalAttackCooldown = speed;
    }

    public void SetPlayer(PlayerCharacter player)
    {
        _player = player;
    }
}
