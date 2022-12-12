using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Kettle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tempText;
    private int temperature = 22;
    private Coroutine coroutineHeat = null;

    private void Start()
    {
        tempText.text = temperature.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && coroutineHeat == null)
            coroutineHeat = StartCoroutine(HeatWater());
    }

    IEnumerator HeatWater()
    {
        while (temperature != 100)
        {
            yield return new WaitForSeconds(1f);
            temperature++;
            tempText.text = temperature.ToString();
        }
        tempText.color = new Color(255f, 0f, 0f);
    }
}
