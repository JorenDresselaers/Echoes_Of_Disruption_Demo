using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnAwake : MonoBehaviour
{
    [SerializeField] GameObject _objectToSpawn;
    [SerializeField] private string _ignoreIfTagInScene;
    [SerializeField] private bool _spawnOnFunctionCall = false;

    private void Awake()
    {
        if (_spawnOnFunctionCall) return;

        Spawn();
    }

    public void Spawn()
    {
        bool spawnObject = true;
        foreach (var obj in FindObjectsOfType<GameObject>())
        {
            if (obj.tag == _ignoreIfTagInScene)
            {
                spawnObject = false;
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;
            }
        }
        if (spawnObject) Instantiate(_objectToSpawn, transform.position, transform.rotation);
        Destroy(gameObject, 0.01f);
    }
}
