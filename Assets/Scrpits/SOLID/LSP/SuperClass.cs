using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSP {
    public class SuperClass : MonoBehaviour {
        public int power;
        public int speed;
        public Vector3 direction;

        public virtual void Move() {
            
        }

        public void Turn(SuperClass super) {
            if (super.GetType() == typeof(DerivedClass)) {
                return;
            }
        }

        public virtual int Read() {
            // blah blah
            return 0;
        }
    }
}
