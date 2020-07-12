using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasaController : MonoBehaviour
{
    public GameObject casaViva;
    public GameObject casaMorta;
    
    // Start is called before the first frame update
    void Start()
    {
        this.SetVivo();
    }

    // Update is called once per frame
    public void SetVivo()
    {
        this.casaViva.SetActive(true);
        this.casaMorta.SetActive(false);
    }
    
    public void SetMorto()
    {
        this.casaViva.SetActive(false);
        this.casaMorta.SetActive(true);
    }
}
