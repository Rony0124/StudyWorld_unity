using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lexer {
    private readonly string code;
    private readonly Tokenizer tokenizer;
    private int position;
    private int readPosition;
    private byte currentCharAsByte;

    public Lexer(string code, Tokenizer tokenizer) {
        this.code = code;
        this.tokenizer = tokenizer;
    }
    
    public Tokenizer.Token TestNextToken(string input) {
        Tokenizer.Token token;
        
        var x = PeekChar(input);

        switch (x) {
            case '+':
                token = new Tokenizer.Token(Tokenizer.TokenType.PLUS, x +"");
                Debug.Log("aa");
                break;
            default:
                token = new Tokenizer.Token(Tokenizer.TokenType.ILLEGAL, x +"");
                break;
        }
        
        readPosition += 1;
        
        return token;
    }
    
    
    private char PeekChar(string code) {
        if (readPosition >= code.Length)
            return (char)0;

        return code[readPosition];
    }
    
}
