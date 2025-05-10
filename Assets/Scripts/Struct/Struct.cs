using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Struct : MonoBehaviour
{

    public struct Numbers
    {
        public int a, b;
        public Numbers(int a, int b)
        {
            this.a = a;
            this.b = b;
        }
        public void Log()
        {
            Debug.Log(a);
            Debug.Log(b);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Numbers num;
        num.a = 10;
        num.b = 20;
        num.Log();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
