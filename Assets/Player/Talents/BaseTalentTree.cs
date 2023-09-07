using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseTalentTree : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private List<BaseTalent> _talents = new List<BaseTalent>();
    public int _availablePoints;
    public int _totalPoints;

    private void Awake()
    {
        if(_talents.Count <= 0)
        {
            _talents = GetComponentsInChildren<BaseTalent>().ToList();
        }
    }

    public void Initialize(PlayerCharacter player)
    {
        foreach(var talent in _talents) 
        {
            talent.Initialize(player);
        }
    }

    public void DisableInterface()
    {
        _canvas.enabled = false;
    }
    
    public void EnableInterface()
    {
        _canvas.enabled = true;
    }

    public void ToggleInterface()
    {
        _canvas.enabled = !_canvas.enabled;
    }
}
