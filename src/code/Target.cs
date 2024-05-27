using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{
    private int vlue;
    public GameObject text;
    private control control;

    public void Init(control ñ, int v)
    {
        control = ñ;
        vlue = v;
    }

    // Start is called before the first frame update
    void Start()
    {
        text.GetComponent<TextMeshPro>().text = vlue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            control.AddInScore(vlue);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
