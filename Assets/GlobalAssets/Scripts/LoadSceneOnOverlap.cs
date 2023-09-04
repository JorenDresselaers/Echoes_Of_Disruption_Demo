using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class LoadSceneOnOverlap : MonoBehaviour
{
    [SerializeField] private string _interactTag;
    [SerializeField] private string _sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (_interactTag != string.Empty)
        {
            if (other.tag != _interactTag) return;
        }

        foreach(DropOnDestroy obj in FindObjectsOfType<DropOnDestroy>())
        {
            obj._quitting = true;
        }

        SceneManager.LoadScene(_sceneToLoad);
    }
}
