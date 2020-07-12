using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParent : MonoBehaviour
{
    private GameObject _target;
    
    // Start is called before the first frame update
    void Start()
    {
        this._target = this.transform.parent.gameObject;
        this.transform.parent = this.transform.parent.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if(this._target)
            this.transform.position = this._target.transform.position;
    }
}
