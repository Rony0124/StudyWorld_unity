using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    private void Start() {
        Character.Instance.playerBoost += HealthBoost;

    }
    public void HealthBoost(Character cha) {
        Debug.Log("Hp increased");
        cha.hp += 100;
    }

    public void DefenseBoost(Character cha) {
        Debug.Log("Defense increased");
        cha.defense += 200;
    }

    public void DamageBoost(Character cha) {
        Debug.Log("damage increased");
        cha.damage += 100;
    }

    

}
