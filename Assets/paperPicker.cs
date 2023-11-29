using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaperPicker : MonoBehaviour
{
    //variables 
    public GameObject collectTextObj, intText;
    public TextMeshProUGUI collectText;
    public static int pagesCollected;

    private bool interactable;

    //linking these to an isTrigger box collider
    void OnTriggerEnter(Collider other)
    {
        UpdateInteractability(other, true);
    }
    //linking these to an isTrigger box collider
    void OnTriggerExit(Collider other)
    {
        UpdateInteractability(other, false);
    }

    void UpdateInteractability(Collider other, bool value)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(value);
            interactable = value;
        }
    }

    void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.E))
        {
            pagesCollected++;
            UpdateUI();
            DisableObjects();
        }
    }

    void UpdateUI()
    {
        collectText.text = pagesCollected + "/8 pages";
        collectTextObj.SetActive(true);
    }

    void DisableObjects()
    {
        intText.SetActive(false);
        gameObject.SetActive(false);
        interactable = false;
    }
}
