using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statemachine : MonoBehaviour
{
    private bool changeState_idle=false;
    private bool changeState_Walk = false;
    private enum state
    {
        Idle,
        Walk
    }

    private state currentstate;

    private void Start()
    {
        currentstate=state.Idle;
    }

    private void Update()
    {
        switch (currentstate) { 
        
        case state.Idle:
                break;

        case state.Walk:
                break;
        }
        
    }
    void idleState()
    {
        if (changeState_idle) { 
          currentstate = state.Idle;
        }
    }
    void walkState() {
        if (changeState_Walk) { 
         currentstate=state.Walk;
        }
    }
}
