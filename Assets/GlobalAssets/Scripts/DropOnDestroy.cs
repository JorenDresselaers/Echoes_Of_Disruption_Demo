using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] private SpawnCardDeck _deck;
    [SerializeField] private string _itemDeckTag;
    public bool _quitting;

    private void Awake()
    {
        if(!_deck)
        {
            foreach(SpawnCardDeck deck in FindObjectsOfType<SpawnCardDeck>())
            {
                if(deck.tag == _itemDeckTag)
                {
                    _deck = deck;
                    break;
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (!_quitting && _deck)
        {
             Instantiate(_deck.GetRandomCardWeighted()._objectToSpawn, transform.position, transform.rotation);
        }
    }

    private void OnApplicationQuit()
    {
        _quitting = true; //make sure no items drop as the game is ending
        Destroy(gameObject);
    }
}
