using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _player;

    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Health _playerHealth;
    
    [SerializeField] private TextMeshProUGUI _itemText;

    [SerializeField] private TextMeshProUGUI _offense;
    [SerializeField] private TextMeshProUGUI _defense;
    [SerializeField] private TextMeshProUGUI _utility;

    private void Awake()
    {
        _healthText.text = _playerHealth.GetCurrentHealth().ToString() + "/" + _playerHealth.GetMaxHealth().ToString();
        UpdateItems();
    }

    // Update is called once per frame
    private void Update()
    {
        _healthText.text = _playerHealth.GetCurrentHealth().ToString();

        if (_player.GetOffensiveSkill())
        {
            if (_player.GetOffensiveSkill().CanActivate())
            {
                _offense.color = Color.green;
            }
            else
            {
                _offense.color = Color.red;
            }
        }

        if (_player.GetDefensiveSkill())
        {
            if (_player.GetDefensiveSkill().CanActivate())
            {
                _defense.color = Color.green;
            }
            else
            {
                _defense.color = Color.red;
            }
        }

        if (_player.GetUtilitySkill())
        {
            if (_player.GetUtilitySkill().CanActivate())
            {
                _utility.color = Color.green;
            }
            else
            {
                _utility.color = Color.red;
            }
        }
    }

    public void UpdateItems()
    {
        string newText = "";

        foreach (BaseItem item in _player.GetItems())
        {
            newText += item.GetCount() + "x " + item.GetName() + "\n";
        }

        _itemText.text = newText;
    }
}
