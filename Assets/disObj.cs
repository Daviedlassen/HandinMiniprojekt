using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class disObj : MonoBehaviour

{
    //variables for the active time and which object to set to inactive

    public GameObject Obj;
    public float activeTime;

    void Update()
    {
        //runs the code whenever an attached object is active.
        if (Obj.active == true)
        {
            StartCoroutine(Disableobj());
        }
    }

    IEnumerator Disableobj()
    {
        yield return new WaitForSeconds(activeTime);
        Obj.SetActive(false);
    }
}
