using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
  
    bool stopped = false;

    private void OnEnable()
    {
        Actions.OnPlayerDeath += StopMoveForward;

    }
    private void OnDisable()
    {
        Actions.OnPlayerDeath -= StopMoveForward;

    }




    void Update()
    {
        if(!stopped)
         transform.Translate(Vector3.right * PipeManager.Instance.GetPipeSpeed() * Time.deltaTime);   
    }

    void StopMoveForward()
    {
        stopped = true;
    }
}
