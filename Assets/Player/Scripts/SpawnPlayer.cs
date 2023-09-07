using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] GameObject _objectToSpawn;
    [SerializeField] private string _ignoreIfTagInScene;
    [SerializeField] private bool _spawnOnFunctionCall = false;
    [SerializeField] private BaseTalentTree _tree;

    private void Awake()
    {
        if (_spawnOnFunctionCall) return;

        Spawn();
    }

    public void Spawn(BaseTalentTree tree = null)
    {
        if (tree != null) _tree = tree;

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
        if (spawnObject)
        {
            GameObject playerObject = Instantiate(_objectToSpawn, transform.position, transform.rotation);
            PlayerCharacter player = playerObject.GetComponent<PlayerCharacter>();
            player.SetTalentTree(_tree);
        }
        
        Destroy(gameObject, 0.01f);
    }
}
