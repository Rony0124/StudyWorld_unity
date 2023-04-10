using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, ISwitchable
{
    private void Open() {
        Debug.Log("open..");
    }

    private void Close() {
        Debug.Log("close..");
    }

    public bool isActive { get; set; }
    public void Activate() {
        Open();
    }

    public void DeActivate() {
        Close();
    }
}
