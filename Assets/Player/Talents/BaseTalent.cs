using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseTalent : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] List<BaseTalent> _talentsRequired = new List<BaseTalent>();
    private List<BaseTalent> _talentsRequiringThis = new List<BaseTalent>();
    public bool _isEnabled = false;
    public int _rank = 0;
    public int _maxRank = 1;

    private void Awake()
    {
        foreach (BaseTalent talent in _talentsRequired)
        {
            talent.AddDependingTalent(this);
        }

        if (_isEnabled)
        {
            Enable();
            foreach (BaseTalent talent in _talentsRequired)
            {
                if (talent._rank < talent._maxRank)
                {
                    Disable();
                    break;
                }
            }
        }
        else
        {
            Disable();
        }
    }

    public virtual void Initialize(PlayerCharacter player)
    {
    }

    public void IncreaseRank()
    {
        _rank++;

        if (_rank >= _maxRank)
        {
            _rank = _maxRank;
            foreach(BaseTalent talent in _talentsRequiringThis)
            {
                talent.CheckIfRequiredAreMax();
            }
        }
        else
        {
            print("Rank is: " + _rank);
        }
    }

    public void SetRank(int rank)
    {
        _rank = rank;
        if (_rank >= _maxRank) _rank = _maxRank;
    }

    public void Enable()
    {
        _isEnabled = true;
        _button.interactable = true;
    }

    public void Disable()
    {
        _isEnabled = false;
        _button.interactable = false;
    }

    private void AddDependingTalent(BaseTalent talent)
    {
        _talentsRequiringThis.Add(talent);
    }

    public bool CheckIfRequiredAreMax()
    {
        foreach(BaseTalent talent in _talentsRequired)
        {
            if(talent._rank < talent._maxRank)
            {
                Disable();
                return false;
            }
        }
        Enable();
        return true;
    }
}
