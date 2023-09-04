using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BaseDirector : MonoBehaviour
{
    public bool _isEnabled;
    [SerializeField] private bool _destroyWhenBroke = false;
    [Header("Credits")]
    [SerializeField] private int _credits = 0;
    [SerializeField] private float _creditMultiplier = 1f;
    [SerializeField] private int _income = 0;
    [SerializeField] private float _timeBetweenIncome = 1f;
    private float _timeSinceLastIncome = 0;

    [Header("Deck")]
    [SerializeField] private SpawnCardDeck _deck;
    [SerializeField] private bool _weighted = true;
    [SerializeField] private float _timeBetweenSpawns = 1.0f;
    [SerializeField] private float _timeSinceLastSpawn = 0f;
    private List<SpawnLocation> _spawnLocations;

    [Header("Location")]
    [SerializeField] private bool _spawnAroundObject = false;
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private float _spawnDistance = 5f;


    protected void Awake()
    {
        _spawnLocations = FindObjectsOfType<SpawnLocation>().ToList();
        if (_deck) _deck = Instantiate(_deck, transform);
    }

    protected void Update()
    {
        if (!_isEnabled) return;

        _timeSinceLastIncome -= Time.deltaTime;
        if (_timeSinceLastIncome <= 0)
        {
            _timeSinceLastIncome = _timeBetweenIncome;
            _credits += (int)(_income * _creditMultiplier);
        }

        _timeSinceLastSpawn -= Time.deltaTime;
        if (_timeSinceLastSpawn <= 0)
        {
            if (_credits > 0)
            {
                SpawnRandomCard();
                _timeSinceLastSpawn = _timeBetweenSpawns;
            }
        }
    }

    private SpawnCard GetRandomCard()
    {
        return _weighted ? _deck.GetRandomCardWeighted() : _deck.GetRandomCard();
    }

    private void SpawnRandomCard()
    {
        if (_deck.GetLowestCost() > _credits)
        {
            if (_destroyWhenBroke) Destroy(gameObject, 0.01f);
            else _isEnabled = false;
            return;
        }

        bool hasGottenValidCard = false;
        int getCardAttempts = 0;
        SpawnCard currentCard = null;
        while (!hasGottenValidCard && getCardAttempts < 10)
        {
            getCardAttempts++;
            currentCard = GetRandomCard();
            if (!currentCard)
            {
                print("Random card was NULL");
                continue;
            }

            if (currentCard._cost > _credits)
            {
                print("Random card was too expensive");
                continue;
            }
            hasGottenValidCard = true;
        }

        if (!currentCard)
        {
            print("Random card was NULL");
            return;
        }

        if (currentCard._cost > _credits)
        {
            print("Random card was too expensive");
            return;
        }

        if (!_spawnAroundObject)
        {
            int randomLocationInt = Random.Range(0, _spawnLocations.Count);

            bool hasSpawned = false;
            int spawnAttempts = 0;
            while (!hasSpawned && spawnAttempts < 10)
            {
                //print("[" + attempts + "] Trying to spawn");
                spawnAttempts++;
                hasSpawned = _spawnLocations[randomLocationInt].Spawn(currentCard._objectToSpawn);
                if (hasSpawned)
                {
                    PayCard(currentCard);
                }
            }
        }
        else
        {
            Vector3 spawnPos = transform.position;
            spawnPos.x = spawnPos.x - _spawnDistance + Random.Range(0f, _spawnDistance * 2f);
            spawnPos.z = spawnPos.z - _spawnDistance + Random.Range(0f, _spawnDistance * 2f);

            Instantiate(currentCard._objectToSpawn, spawnPos, Quaternion.identity);
            PayCard(currentCard);
        }
    }

    private void PayCard(SpawnCard card)
    {
        _credits -= card._cost;
        if (_credits <= 0)
        {
            if (_destroyWhenBroke) Destroy(gameObject, 0.01f);
            else _isEnabled = false;
        }
    }
}