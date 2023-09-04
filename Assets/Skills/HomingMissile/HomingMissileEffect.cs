using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Collider))]
public class HomingMissileEffect : MonoBehaviour
{
    public bool _isTargetingObject = true;
    public GameObject _targetObject;
    public Vector3 _targetLocation;
    public float _speed = 1;
    public int _damage = 5;
    public bool _isPlayer = true;

    public bool _doesArch = false;
    private float _upwardsDuration = 10f;
    private Vector3 _upwardsForce;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
        if(_doesArch)
        {
            _upwardsForce = new Vector3(0, _upwardsDuration, 0);
        }
        else
        {
            _upwardsForce = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTargetingObject && _targetObject)
        {
            transform.position += (_targetObject.transform.position - transform.position + _upwardsForce).normalized * _speed * Time.deltaTime;
        }
        else
        {
            transform.position += (_targetLocation - transform.position + _upwardsForce).normalized * _speed * Time.deltaTime;
        }

        if (_doesArch)
        {
            _upwardsDuration -= Time.deltaTime;
            if (_upwardsDuration >= 0)
            {
                _upwardsForce.y = _upwardsDuration;
            }
            else
            {
                _upwardsForce = Vector3.zero;
            }
        }

        transform.rotation = Quaternion.LookRotation((transform.position - _targetLocation).normalized);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isPlayer && other.tag == PlayerCharacter.PLAYER_TAG) return;
        else if(!_isPlayer && other.tag == EnemyCharacter.ENEMY_TAG) return;

        if (other.TryGetComponent(out IHittable hittableObject))
        {
            hittableObject.HitObject(_damage);
        }
        Destroy(gameObject, 0.01f);
    }
}
