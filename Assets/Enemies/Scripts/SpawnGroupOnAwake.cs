using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroupOnAwake : MonoBehaviour
{
    [SerializeField] List<GameObject> _objectsToSpawn = new List<GameObject>();
    [SerializeField] int _amountToSpawn = 1;
    [SerializeField] float _spawnDistance = 5f;

    private void Awake()
    {
        for(int i = 0; i < _amountToSpawn; ++i)
        {
            GameObject toSpawn = _objectsToSpawn[Random.Range(0, _objectsToSpawn.Count)];

            Vector3 spawnPos = transform.position;
            spawnPos.x = spawnPos.x - _spawnDistance + Random.Range(0f, _spawnDistance * 2f);

            Instantiate(toSpawn, spawnPos, Quaternion.identity);
        }

        Destroy(gameObject, 0.01f);
    }
}
