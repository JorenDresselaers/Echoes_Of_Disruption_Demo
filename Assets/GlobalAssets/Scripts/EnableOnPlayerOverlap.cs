using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnableOnPlayerOverlap : MonoBehaviour
{
    [SerializeField] GameObject _objectToEnable;

    private void OnTriggerEnter(Collider other)
    {
        if (!_objectToEnable) return;
        if(other.tag == PlayerCharacter.PLAYER_TAG)
        {
            _objectToEnable.SetActive(true);
            Destroy(gameObject, 0.01f);
        }
    }
}
