using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnableDisableOtherOnOverlap : MonoBehaviour
{
    private enum Mode
    {
        Enable,
        Disable,
        Toggle
    }

    [SerializeField] Mode _mode;
    [SerializeField] bool _activateOnOverlap = true;
    [SerializeField] string _interactTag;
    [SerializeField] bool _destroyAfterToggle = true;
    [SerializeField] List<GameObject> _objectsToEnableDisable = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if(_interactTag != string.Empty)
        {
            if (other.tag != _interactTag) return;
        }

        if(_activateOnOverlap)
            Interact();
    }

    public void Interact()
    {
        switch (_mode)
        {
            case Mode.Enable:
                foreach (GameObject obj in _objectsToEnableDisable)
                {
                    if (!obj) continue;
                    obj.SetActive(true);
                }
                break;
            case Mode.Disable:
                foreach (GameObject obj in _objectsToEnableDisable)
                {
                    if (!obj) continue;
                    obj.SetActive(false);
                }
                break;
            case Mode.Toggle:
                foreach (GameObject obj in _objectsToEnableDisable)
                {
                    if (!obj) continue;
                    obj.SetActive(!obj.activeSelf);
                }
                break;
        }

        if(_destroyAfterToggle) Destroy(gameObject, 0.01f);
    }
}
