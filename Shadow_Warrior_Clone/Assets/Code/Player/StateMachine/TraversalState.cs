using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateCode
{
    public class TraversalState : IState
    {

        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExit()
        {
            
        }
        public virtual void FixedTick()
        {
            
        }

        public virtual Type Tick()
        {
            return null;
        }
    }
}