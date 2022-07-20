using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class TagParser : MonoBehaviour {
    private string textInput;
    private string currentToken;
    private Queue<string> tokenQueue;
    private List<string> stringList = new List<string>();
    public List<string> resultList = new List<string>();
    private Queue<string> tagStack = new Queue<string>();
    private List<string> tagTargetList = new List<string>();

    private string tag = "pop";
    // Start is called before the first frame update
    void Start() {
        var text = transform.GetComponent<Text>();
        textInput = text.text;

        var list = Regex.Split(textInput, @"\s*([<>/])\s*")
            .Where(n => !string.IsNullOrEmpty(n))
            .ToList();

        tokenQueue = new Queue<string>(list);

        NextToken();

        while (currentToken != "end") {
            if (currentToken == "<") {
                var stirng = BuildTag(currentToken);
                tagStack.Enqueue(stirng);
                stringList.Add(stirng);
            } else {
                stringList.Add(currentToken);
            }

            NextToken();
        }

        var result = ApplyTagFunc();
        transform.GetComponent<Text>().text = result;
    }

    private void AddFunc(List<string> arg0) { }

    private string ApplyTagFunc() {
        for (int i = 0; i < stringList.Count; i++) {
            if (stringList[i].Contains(tag + ">")) {
                var j = i;

                while (!stringList[j].Contains("</" + tag+ ">")) {
                    if (!stringList[++j].Contains(">")) {
                        tagTargetList.Add(stringList[j]);
                    }
                }
                continue;
            }
            resultList.Add(stringList[i]);
        }
        var exp = resultList.Aggregate("", (partialPhrase, word) => $"{partialPhrase} {word}");
        return exp;
    }

    private string BuildTag(string data) {
        string tag = data;

        while (true) {
            data = tokenQueue.Dequeue();
            tag += data;

            if (data == ">") {
                return tag;
            }
        }
    }

    private void NextToken() {
        if (tokenQueue.Count <= 0) {
            currentToken = "end";
            return;
        }

        currentToken = tokenQueue.Dequeue();
    }
}
