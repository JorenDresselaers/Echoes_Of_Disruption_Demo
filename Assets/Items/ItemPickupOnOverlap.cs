using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupOnOverlap : MonoBehaviour
{
    [SerializeField] private BaseItem _itemToPickUp;

    private void Awake()
    {
        _itemToPickUp = Instantiate(_itemToPickUp, transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != PlayerCharacter.PLAYER_TAG) return;

        if(other.TryGetComponent(out PlayerCharacter player))
        {
            player.AddItem(_itemToPickUp);
            Destroy(gameObject.transform.root.gameObject, 0.01f);
        }
    }
}
