using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestroy : MonoBehaviour
{
    [SerializeField] public float destroyTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, (destroyTime + GameController.explosionMod));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
