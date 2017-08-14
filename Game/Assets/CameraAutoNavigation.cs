using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAutoNavigation : MonoBehaviour {

    [SerializeField] private GameObject _target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(_target != null){
            Vector3 v3 = _target.transform.position - transform.position;
            if (v3.sqrMagnitude < 1000){
                transform.position = new Vector3(_target.transform.position.x, _target.transform.position.y, transform.position.z);
                _target = null; 
                return;  
            } 
            v3.Normalize();
            v3 = v3 * Time.deltaTime * 1000;
            transform.position += v3;
        }
	}

    public void SetTarget(GameObject target)
    {
        _target = target;
    }
}
