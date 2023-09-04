using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastwaveSkill : BaseSkill
{
    [SerializeField] private GameObject _blastWaveEffect;
    [SerializeField] private float _speed = 1f;

    public override void Activate(BaseCharacter character)
    {
        if (!_canActivate) return;
        base.Activate();

        GameObject blastWave = Instantiate(_blastWaveEffect, character.transform.position, character.transform.rotation);
        BlastWaveEffect effect = blastWave.GetComponent<BlastWaveEffect>();

        if (effect)
        {
            PlayerCharacter player = character as PlayerCharacter;
            if (player)
            {
                Vector3 targetLocation = player.GetShootCameraLook().GetLookAtLocation();
                if (targetLocation != Vector3.zero)
                {

                    effect._movementDirection = targetLocation - transform.position;
                    effect._movementDirection = effect._movementDirection.normalized * _speed;
                }
                else
                {
                    effect._movementDirection = player.GetCamera().transform.forward * _speed;
                }
            }
            else
            {
                effect._movementDirection = character.transform.forward * _speed;
            }
        }
    }
}
