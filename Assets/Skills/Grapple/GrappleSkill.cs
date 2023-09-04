using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleSkill : BaseSkill
{
    public float _distance = 10f;

    public override void Activate(BaseCharacter character)
    {
        if (!_canActivate) return;
        base.Activate();

        PlayerCharacter player = character as PlayerCharacter;

        if (player)
        {
            Vector3 cameraPosition = player.GetCamera().transform.position;
            Vector3 cameraForward = player.GetCamera().transform.forward;

            Ray ray = new Ray(cameraPosition, cameraForward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _distance))
            {
                player.transform.position = hit.point;
            }
        }
    }
}
