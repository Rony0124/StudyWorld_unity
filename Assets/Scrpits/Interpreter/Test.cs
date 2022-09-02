using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    private Lexer x;
    // Start is called before the first frame update
    void Start() {
        x = new Lexer("asd", new Tokenizer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
