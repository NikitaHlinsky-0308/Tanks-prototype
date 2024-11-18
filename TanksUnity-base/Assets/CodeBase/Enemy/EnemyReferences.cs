using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace CodeBase.Player
{
    [DisallowMultipleComponent]
    public class EnemyReferences : MonoBehaviour
    {
        [HideInInspector] public NavMeshAgent navMeshAgent;

        [Header("Stats")]
        public float pathUpdateDelay = 0.2f;
        
        private void Awake()
        {   
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
    }
}