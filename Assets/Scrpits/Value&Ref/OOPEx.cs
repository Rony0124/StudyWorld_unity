using System.Collections;
using System.Collections.Generic;
using UnityEngine;
struct Mage {
    public int hp;
    public int attack;
    
    public Mage(int hp, int atk) {
        this.hp = hp;
        this.attack = atk;
    }
}

class Knight {
    public static int counter = 0;
    public int id;
    public int hp;
    public int attack;

    public Knight() {
        id = counter;
        counter++;
    }
    public Knight(int a, int b):this() {
        
    }

    public static Knight CreateKnight() {
        Knight knight = new Knight();
        knight.hp = 0;

        return knight;
    }
}
public class OOPEx : MonoBehaviour
{
    void Start() {
        Knight knight = new Knight();
        knight.hp = 10;
        Knight knight2 = new Knight();
        
    }
}
