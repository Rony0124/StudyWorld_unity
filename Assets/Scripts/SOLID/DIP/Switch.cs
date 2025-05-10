using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private ISwitchable switcher;

    public bool isActivated;

    private void Toggle() {
        if (switcher.isActive) {
            switcher.Activate();
        } else {
            switcher.DeActivate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
