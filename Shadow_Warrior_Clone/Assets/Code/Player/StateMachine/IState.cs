using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateCode
{
    public interface IState
    {
        void OnEnter();
        void OnExit();
        Type Tick();
        void FixedTick();
    }
}