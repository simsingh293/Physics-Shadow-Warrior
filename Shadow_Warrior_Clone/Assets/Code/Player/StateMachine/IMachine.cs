using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateCode
{
    public interface IMachine
    {
        
        void Init();
        void SetStates();
        void ChangeState();

        
    }
}