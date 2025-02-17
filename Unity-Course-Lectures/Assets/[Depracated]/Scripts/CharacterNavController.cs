﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class CharacterNavController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _targetFeedback;

    private NavMeshAgent _navMeshAgent;

    
    void Start ()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_targetFeedback != null)
            _targetFeedback.SetActive(false);
	}
	
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _navMeshAgent.SetDestination(hit.point);

                _targetFeedback.transform.position = new Vector3(hit.point.x, 
                                                                _targetFeedback.transform.position.y, 
                                                                hit.point.z);
                
            }
        }

        if(_targetFeedback != null)
            _targetFeedback.SetActive(!TargetReached());
    }

    private bool TargetReached()
    {
        if (!_navMeshAgent.pathPending)
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                   return true;

        return false;    
        

    }

}
