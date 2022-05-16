using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private static Character instance;
    public static Character Instance {
        get {
            return instance;
        }
    }
    public delegate void Boost(Character target);
    public Boost playerBoost;

    public string playerName = "rony";
    public float hp = 10;
    public float defense = 10;
    public float damage = 10;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerBoost += GetStronger;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            playerBoost(this);
        }
    }

    private void GetStronger(Character cha) {
        Debug.Log("1"+cha.hp);
        cha.hp += 1000;
        Debug.Log("2"+cha.hp);
    }

    public Character sendInfo() {
        return this;
    }
}
