using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LSP {
    public class Controller : MonoBehaviour {
        void Start() {
            MoveModel(new DerivedClass());
        }

        private void MoveModel(SuperClass super) {
            super.Move();
        }
    }
}