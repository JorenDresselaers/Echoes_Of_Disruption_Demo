using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour, IHittable
{
    public void HitObject(int damage)
    {
        Destroy(gameObject);
    }
}
