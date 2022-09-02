using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tokenizer {
    public static Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType> {
        {"ILLEGAL", TokenType.ILLEGAL},
        {"EOF", TokenType.EOF},
        {"IDENT", TokenType.IDENT},
        {"int", TokenType.INT},
        {"ASSIGN", TokenType.ASSIGN},
        {"PLUS", TokenType.PLUS},
        {"COMMA", TokenType.COMMA},
        {"SEMICOLON", TokenType.SEMICOLON},
        {"LPAREN", TokenType.LPAREN},
        {"RPAREN", TokenType.RPAREN},
        {"LBRACE", TokenType.LBRACE},
        {"RBRACE", TokenType.RBRACE},
        {"FUNCTION", TokenType.FUNCTION},
        {"LET", TokenType.LET}
    };

    public struct Token {
        public TokenType type;
        public string literal;

        public Token(TokenType type, string literal) {
            this.type = type;
            this.literal = literal;
        }
    }

    public enum TokenType {
        ILLEGAL,
        EOF,
        
        //식별자 + 리터럴
        IDENT,
        INT,
        
        //연산자
        ASSIGN,
        PLUS,
        
        //구분자
        COMMA,
        SEMICOLON,
        
        LPAREN,
        RPAREN,
        LBRACE,
        RBRACE,
        
        //예약어
        FUNCTION,
        LET
    }

    
}
