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

        private static void Calc(SuperClass super) {
            while (super.Read() != 0) {
                Debug.Log("?");
            }
        }
    }
}
