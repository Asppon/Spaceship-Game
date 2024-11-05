using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Have to import so that script knows what an image is

public class HealthUIController : MonoBehaviour
{
    public GameObject healthContainer;
    private float amountFilled; //how much of the health bar is filled
    
    void Update()
    {
        amountFilled = (float)GameController.Health; //sets the amount filled to current health
        amountFilled = amountFilled / GameController.PlayerHealth;
        healthContainer.GetComponent<Image>().fillAmount = amountFilled;
    } //updates health ui based on current health
}
