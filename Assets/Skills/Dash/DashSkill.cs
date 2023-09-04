using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : BaseSkill
{
    public float _distance = 1f;

    public override void Activate(BaseCharacter character)
    {
        if (!_canActivate) return;
        base.Activate();

        PlayerCharacter player = character as PlayerCharacter;
        Movement.Direction direction = Movement.Direction.Null;
        if(player)
        {
            direction = player.GetMovement().GetDirection();
        }

        //character.transform.position += character.transform.forward * _distance;
        Vector3 dashVector = character.transform.forward;

        switch(direction)
        {
            case Movement.Direction.Forward:
                dashVector = character.transform.forward;
                break;

            case Movement.Direction.Backward:
                dashVector = -character.transform.forward;
                break;

            case Movement.Direction.Left:
                dashVector = -character.transform.right;
                break;

            case Movement.Direction.Right:
                dashVector = character.transform.right;
                break;

            case Movement.Direction.ForwardRight:
                dashVector = character.transform.forward + character.transform.right;
                dashVector = dashVector.normalized;
                break;

            case Movement.Direction.ForwardLeft:
                dashVector = character.transform.forward - character.transform.right;
                dashVector = dashVector.normalized;
                break;

            case Movement.Direction.BackwardRight:
                dashVector = -character.transform.forward + character.transform.right;
                dashVector = dashVector.normalized;
                break;

            case Movement.Direction.BackwardLeft:
                dashVector = -character.transform.forward - character.transform.right;
                dashVector = dashVector.normalized;
                break;

            default:
                dashVector = character.transform.forward;
                break;
        }

        Ray ray = new Ray(character.transform.position, dashVector);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _distance))
        {
            character.transform.position = hit.point - dashVector * _distance / 10;
        }
        else
        {
            character.transform.position += dashVector * _distance;
        }
    }

}
