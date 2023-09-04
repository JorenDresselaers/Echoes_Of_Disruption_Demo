using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileSkill : BaseSkill
{
    [SerializeField] private GameObject _homingMissileEffect;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private int _damage = 5;
    [SerializeField] private bool _doesArch = false;
    [SerializeField] private bool _randomiseSpawnPosition = false;
    [SerializeField] private float _lifeTime = 10f;

    public override void Activate(BaseCharacter character)
    {
        if (!_canActivate) return;
        base.Activate();

        GameObject homingMissile = Instantiate(_homingMissileEffect, character.transform.position, character.transform.rotation);
        if(_randomiseSpawnPosition)
        {
            homingMissile.transform.position = character.transform.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        }
        HomingMissileEffect effect = homingMissile.GetComponent<HomingMissileEffect>();

        if (effect)
        {
            effect._speed = _speed;
            effect._damage = _damage;
            effect._doesArch = _doesArch;
            if(effect.TryGetComponent(out TimedLife timedLife)) timedLife._timedLife = _lifeTime;

            //check if fired by a player
            PlayerCharacter player = character as PlayerCharacter;
            if (player)
            {
                effect._isPlayer = true;
                GameObject targetObject = player.GetShootCameraLook().GetLookAtObject();
                if (targetObject)
                {
                    effect._isTargetingObject = true;
                    effect._targetObject = targetObject;
                }
                else
                {
                    effect._isTargetingObject = false;
                    effect._targetLocation = player.GetShootCameraLook().GetLookAtLocation();
                    if(effect._targetLocation == Vector3.zero)
                    {
                        effect._targetLocation = player.transform.position + player.GetCamera().transform.forward * _lifeTime * _speed;
                    }
                }
            }
            //if not, check if fired by an enemy
            else
            {
                EnemyCharacter enemy = character as EnemyCharacter;
                if(enemy)
                {
                    effect._isPlayer = false;
                    effect._targetObject = enemy.GetPlayerCharacter().gameObject;
                }
                //if neither, destroy missile
                else
                {
                    Destroy(homingMissile, 0.01f);
                }
            }
        }
    }
}
