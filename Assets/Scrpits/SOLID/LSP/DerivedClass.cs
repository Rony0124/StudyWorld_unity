using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSP {
    public class DerivedClass : SuperClass {
        public override void Move() {
            throw new Exception();
        }
        public void Accelerate() {
            
        }

        public override int Read() {
            //blah blah
            return 1;
        }
    }
}

