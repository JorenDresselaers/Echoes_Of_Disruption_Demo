using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootAtCameraLook : MonoBehaviour
{
    public bool _canShoot = true;

    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _totalShotCooldown = 0.1f;
    private float _currentShotCooldown;
    [SerializeField] private int _damagePerHit = 1;
    [SerializeField] private bool _hasKnockback = true;
    [SerializeField] private float _knockback = 5f;
    private bool _isShooting = false;
    private PlayerCharacter _player;

    private void Update()
    {
        if(_isShooting)
        {
            _currentShotCooldown -= Time.deltaTime;
            if(_currentShotCooldown <= 0)
            {
                _currentShotCooldown = _totalShotCooldown;
                ShootCast();
            }
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isShooting = true;
        }
        else if(context.canceled)
        {
            _isShooting = false;
        }
    }

    private void ShootCast()
    {
        if (_player) _player.TriggerOnAttack();

        Vector3 cameraPosition = _playerCamera.transform.position;
        Vector3 cameraForward = _playerCamera.transform.forward;

        Ray ray = new Ray(cameraPosition, cameraForward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.TryGetComponent(out IHittable hittableObject))
            {
                hittableObject.HitObject(_damagePerHit);
                if(_hasKnockback && hitObject.TryGetComponent(out Rigidbody body))
                {
                    body.AddForce(cameraForward * _knockback);
                }
            }
        }
    }

    public Vector3 GetLookAtLocation()
    {
        Vector3 cameraPosition = _playerCamera.transform.position;
        Vector3 cameraForward = _playerCamera.transform.forward;

        Ray ray = new Ray(cameraPosition, cameraForward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
    
    public GameObject GetLookAtObject()
    {
        Vector3 cameraPosition = _playerCamera.transform.position;
        Vector3 cameraForward = _playerCamera.transform.forward;

        Ray ray = new Ray(cameraPosition, cameraForward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.rigidbody != null) return hit.rigidbody.gameObject;
        }
        return null;
    }

    public void SetDamage(int damage)
    {
        _damagePerHit = damage;
    }

    public void SetAttackSpeed(float speed)
    {
        _totalShotCooldown = speed;
    }

    public void SetPlayer(PlayerCharacter player)
    {
        _player = player;
    }

    public Camera GetCamera()
    {
        return _playerCamera;
    }
}
