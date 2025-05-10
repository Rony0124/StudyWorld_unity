using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LamdaExpression : MonoBehaviour {
    //람다 형식 
    public delegate int Calculate(int a, int b);
    //func 대리자 => 결과를 반환하는 메소드를 참조
    public delegate TResult Funk<out TResult>();
    public delegate TResult Funk<in T, out TResult>(T args);
    public delegate TResult Funk<in T1, in T2, out TResult>(T1 args1, T2 args2);
    //action 대리자 => 결과를 반화하지 않는 메소드를 참조
    public delegate void Fund();
    public delegate void Fund<in T>(T args);
    public delegate void Fund<in T1, in T2>(T1 args1, T2 args2);
    
    // Start is called before the first frame update
    void Start() {
        Calculate cal = (a, b) => a + b;
        //func 대리자 사용
        Funk<int> funk = () => 123;
        Funk<int, int> funk1 = args => args * 5;
        Funk<int, int, int> funk2 = (args1, args2) => args1 * args2;
        //action대리자 사용
        Fund fund = () => Debug.Log("asdsd");
        fund();
        Fund<int> fund1 = (int args) => {
            var i = args * 5;
            Debug.Log("123");
        };

        Func<int> func = () => 123;

        Action<int> act = i => {
            var x = i * 5;
        };
    }


}

