using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleUI : MonoBehaviour
{
    [SerializeField] private InputField inputfield;
    [SerializeField] private Button button;
    [SerializeField] private Text text;
    // Start is called before the first frame update

    private void Awake() {
        button.onClick.AddListener(StackCal);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StackCal() {
        StackCalculator stc = new StackCalculator();
        text.text = stc.InfixToPostFix(inputfield.text);
    }
}
