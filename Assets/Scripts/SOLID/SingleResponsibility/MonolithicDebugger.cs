using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DebuggerAudio), typeof(DebuggerInput),
    typeof(DebuggerPrinter))]
public class MonolithicDebugger : MonoBehaviour {
    [SerializeField] private DebuggerAudio audio;
    [SerializeField] private DebuggerInput input;
    [SerializeField] private DebuggerPrinter printer;
    
    private void Start()
    {
        audio = GetComponent<DebuggerAudio>();
        input = GetComponent<DebuggerInput>();
        printer = GetComponent<DebuggerPrinter>();
    }
}


public class DebuggerAudio : MonoBehaviour {
    
}

public class DebuggerInput : MonoBehaviour {
    
}

public class DebuggerPrinter : MonoBehaviour {
    private void PrintDebug(string text) {
        Debug.Log(text);
    }
}
