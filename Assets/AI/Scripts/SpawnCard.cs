using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnCard", menuName = "ScriptableObjects/SpawnCard", order = 1)]
public class SpawnCard : ScriptableObject
{
    public GameObject _objectToSpawn;
    public int _cost;
    public int _weight;
}
