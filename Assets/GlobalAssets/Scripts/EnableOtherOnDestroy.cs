using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ToggleOtherOnDestroy : MonoBehaviour
{
    [SerializeField] Mode _mode;
    [SerializeField] List<GameObject> _objectsToToggle = new List<GameObject>();

    private enum Mode
    {
        Enable,
        Disable,
        Toggle
    }

    private void OnDestroy()
    {
        switch (_mode)
        {
            case Mode.Enable:
                foreach (GameObject obj in _objectsToToggle)
                {
                    if (!obj) continue;
                    obj.SetActive(true);
                }
                break;
            case Mode.Disable:
                foreach (GameObject obj in _objectsToToggle)
                {
                    if (!obj) continue;
                    obj.SetActive(false);
                }
                break;
            case Mode.Toggle:
                foreach (GameObject obj in _objectsToToggle)
                {
                    if (!obj) continue;
                    obj.SetActive(!obj.activeSelf);
                }
                break;
        }
    }

}

