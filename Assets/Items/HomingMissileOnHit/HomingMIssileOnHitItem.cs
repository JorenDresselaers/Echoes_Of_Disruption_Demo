using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMIssileOnHitItem : BaseItem
{
    [SerializeField] GameObject _homingMissile;

    public override void OnAttack(BaseCharacter character)
    {
        float random = Random.Range(0f, 100f);
        if(random < _procChance * _count)
        {
            EnemyCharacter enemyCharacter = FindFirstObjectByType<EnemyCharacter>();
            if (enemyCharacter)
            {
                GameObject enemy = enemyCharacter.gameObject;
                HomingMissileEffect missile = Instantiate(_homingMissile, transform.position, Quaternion.identity).GetComponent<HomingMissileEffect>();
                missile._targetObject = enemy;
            }
        }
    }
}
