using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Damage : MonoBehaviour {
    [SerializeField] private int damagePoints;

    public int DamagePoints => damagePoints;

    public void Hit() {
        Destroy(gameObject);
    }
}