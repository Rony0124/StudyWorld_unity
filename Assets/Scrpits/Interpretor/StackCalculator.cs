using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class StackCalculator {
    private Queue<string> tokenQueue;
    private string currentToken;
    //private List<>
    private Stack<string> stack = new Stack<string>();
    private List<string> postFixList = new List<string>();
    private bool isRightBraceDequeued;
    private bool isLeftBraceDequeued;

    public string InfixToPostFix(string infixInput) {
        var list = Regex.Split(infixInput, @"\s*([-+/*()])\s*")
            .Where(n => !string.IsNullOrEmpty(n))
            .ToList();

        tokenQueue = new Queue<string>(list);

        NextToken();
        while (currentToken != "end") {
         
            if (IsOperator(currentToken)) {
                var lastOpPriority = GetLastPriority();
                var currentOpPriority = GetPriority(currentToken);
                if(lastOpPriority > currentOpPriority) {
                    var poppedOperator = PopStack();

                    if(poppedOperator != "(" && poppedOperator != ")") {
                        postFixList.Add(poppedOperator);
                    }
                    continue;
                }
                PushStack(currentToken);
            }
            else {
                postFixList.Add(currentToken);
            }
            NextToken();
        }
        while(stack.Count != 0) {
            var poppedOperator = PopStack();
            if (poppedOperator != "(" && poppedOperator != ")") {
                postFixList.Add(poppedOperator);
            }
        }

        var exp = postFixList.Aggregate("", (current, postFix) => current + postFix + " ");

        var result = CalculateStack();

        return exp + "=" + result;
    }

    private void NextToken() {
        if (tokenQueue.Count <= 0) {
            currentToken = "end";
            return;
        }

        currentToken = tokenQueue.Dequeue();

        if(currentToken == "(") {
            isLeftBraceDequeued = true;
        }
        if(currentToken == ")") {
            isRightBraceDequeued = true;
        }

    }

    private bool IsOperator(string data) {
        return Regex.IsMatch(data, @"\s*([-+/*()])\s*");
    }
    private int GetLastPriority() {
        if(stack.Count == 0) {
            return 0;
        }
        var op = stack.Peek();
        return GetPriority(op);
    }
    private int GetPriority(string op) {
        switch (op) {
            case "+":
            case "_":
                return 1;
            case "*":
            case "/":
                return 2;
            case "(":
                return isLeftBraceDequeued ? 4 : 0;
            case ")":
                return isRightBraceDequeued ? 0 : 5;
            default:
                return 3;
        }
    }

    private string PopStack() {
        var op = stack.Pop();
        return op;
    }

    private void PushStack(string op) {
        if(op == "(") {
            isLeftBraceDequeued = false;
        }
        if( op == ")") {
            isRightBraceDequeued = false;
        }
        stack.Push(op);
    }
    private string CalculateStack() {
        foreach(var postFix in postFixList) {
            if (!IsOperator(postFix)) {
                stack.Push(postFix);
                continue;
            }

            var b = stack.Pop();
            var a = stack.Pop();
            var result = Calculate(postFix, a, b);
            stack.Push(result);
        }
        return stack.Pop();
    }
    private string Calculate(string op, string a, string b) {
        var aF = float.Parse(a);
        var bF = float.Parse(b);

        return op switch {
            "+" => (aF + bF).ToString(),
            "-" => (aF - bF).ToString(),
            "*" => (aF * bF).ToString(),
            "/" => (aF / bF).ToString(),
            _ => throw new Exception()
        };
    }

}
