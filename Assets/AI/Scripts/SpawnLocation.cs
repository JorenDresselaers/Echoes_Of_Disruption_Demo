using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    [SerializeField] private bool _canSpawn = true;
    [SerializeField] private bool _isReusable = false;

    public bool Spawn(GameObject toSpawn, bool disableAfterSpawn = false)
    {
        if (!toSpawn || !_canSpawn) return false;

        Instantiate(toSpawn, transform.position, transform.rotation);
        if (!_isReusable || disableAfterSpawn)
        {
            _canSpawn = false;
        }

        return true;
    }
}
