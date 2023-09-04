using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedLife : MonoBehaviour
{
    public float _timedLife = -1;

    private void Start()
    {
        if (_timedLife < 0) return;
        Invoke(KILL, _timedLife);
    }

    const string KILL = "Kill";
    private void Kill()
    {
        Destroy(gameObject);
    }
}
