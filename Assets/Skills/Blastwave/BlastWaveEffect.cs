using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastWaveEffect : MonoBehaviour
{
    public Vector3 _movementDirection;
    public int _damage = 1;

    private List<GameObject> _hitObjects = new List<GameObject>();

    private void FixedUpdate()
    {
        transform.position += _movementDirection * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PlayerCharacter.PLAYER_TAG || _hitObjects.Contains(other.gameObject)) return;

        if (other.TryGetComponent(out IHittable hittableObject))
        {
            hittableObject.HitObject(_damage);
            _hitObjects.Add(other.gameObject);
            //Destroy(gameObject, 0.01f);
        }
    }
}
