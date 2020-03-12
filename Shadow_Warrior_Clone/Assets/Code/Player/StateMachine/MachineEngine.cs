using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateCode
{
    public class MachineEngine : MonoBehaviour
    {
        [Header("Initialization Flags")]
        [SerializeField] private bool useTraversalMachine = false;
        [SerializeField] private bool useCombatMachine = false;

        private TraversalStateMachine traversalMachine = null;

        private void Awake()
        {
            if (useTraversalMachine && traversalMachine == null)
            {
                traversalMachine = new TraversalStateMachine();
            }
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}