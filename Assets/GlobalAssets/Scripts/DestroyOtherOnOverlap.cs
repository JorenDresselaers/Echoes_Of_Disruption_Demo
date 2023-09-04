using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DestroyOtherOnOverlap : MonoBehaviour
{
    [SerializeField] List<GameObject> _objectsToDestroy = new List<GameObject>();
    [SerializeField] private bool _activateOnOverlap = true;
    [SerializeField] private bool _destroyAfterInteraction = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!_activateOnOverlap) return;
        Interact();
    }

    public void Interact()
    {
        foreach (GameObject obj in _objectsToDestroy)
        {
            if (!obj) continue;
            Destroy(obj, 0.01f);
        }
        if (_destroyAfterInteraction) Destroy(gameObject, 0.01f);
    }
}
