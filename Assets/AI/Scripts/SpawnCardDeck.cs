using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCardDeck : MonoBehaviour
{
    [SerializeField] private List<SpawnCard> _deck = new List<SpawnCard>();
    private int _lowestCardCost = -1;
    public int _totalWeight = 0;

    private void Awake()
    {
        for (int currentCard = 0; currentCard < _deck.Count; currentCard++)
        {
            if (_deck[currentCard]._objectToSpawn == null)
            {
                _deck.RemoveAt(currentCard);
            }
        }

            foreach (var card in _deck)
        {
            _totalWeight += card._weight;

            if(_lowestCardCost < 0 || card._cost < _lowestCardCost)
                _lowestCardCost = card._cost;
        }
    }

    public SpawnCard GetRandomCard(bool removeWhenDrawn = false)
    {
        int randomInt = Random.Range(0, _deck.Count);
        SpawnCard cardToReturn = _deck[randomInt];
        if (removeWhenDrawn)
        {
            _deck.RemoveAt(randomInt);
        }
        return cardToReturn;
    }
    
    public SpawnCard GetRandomCardWeighted(bool removeWhenDrawn = false)
    {
        if (_totalWeight <= 0) return GetRandomCard();

        int result = 0, total = 0;
        int randVal = Random.Range(0, _totalWeight + 1);
        for (result = 0; result < _deck.Count; result++)
        {
            total += _deck[result]._weight;
            if (total >= randVal) break;
        }

        if (removeWhenDrawn)
        {
            _deck.RemoveAt(result);
        }
        return _deck[result];
    }

    public int GetCardCount()
    {
        return _deck.Count;
    }

    public int GetLowestCost()
    {
        return _lowestCardCost;
    }
}
