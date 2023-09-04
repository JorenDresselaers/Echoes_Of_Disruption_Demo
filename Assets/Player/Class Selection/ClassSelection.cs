using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ClassSelection : MonoBehaviour
{
    [SerializeField] BasePlayerClass _baseClass;

    private void Awake()
    {
        if (!_baseClass) Destroy(gameObject, 0.01f);
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerCharacter player))
        {
            player.SetClass(_baseClass);

            if (TryGetComponent(out EnableDisableOtherOnOverlap overlap))
            {
                overlap.Interact();
            }
            
            if (TryGetComponent(out DestroyOtherOnOverlap destroyOverlap))
            {
                destroyOverlap.Interact();
            }


            Destroy(gameObject, 0.01f);
        }
    }
}
