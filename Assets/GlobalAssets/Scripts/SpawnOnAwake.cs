using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnAwake : MonoBehaviour
{
    [SerializeField] GameObject _objectToSpawn;
    [SerializeField] private string _ignoreIfTagInScene;
    private void Awake()
    {
        bool spawnObject = true;
        foreach(var obj in FindObjectsOfType<GameObject>())
        {
            if(obj.tag == _ignoreIfTagInScene)
            {
                spawnObject = false;
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;
            }
        }
        if(spawnObject) Instantiate(_objectToSpawn, transform.position, transform.rotation);
        Destroy(gameObject, 0.01f);
    }
}
