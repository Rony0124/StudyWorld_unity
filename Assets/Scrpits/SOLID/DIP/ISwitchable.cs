using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwitchable {
    public bool isActive { get; }
    public void Activate();
    public void DeActivate();
}
