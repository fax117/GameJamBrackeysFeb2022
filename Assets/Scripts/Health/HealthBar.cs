using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // gets the healthbar image to be changed with the health percentage
    [SerializeField] private Image _healthBar;
    // how much time will it take to lerp the damage taken and show it in the health bar
    [SerializeField] private float _lerpDuration = 0.1f;

    // receives the percentage of the health
    public void UpdateBar(float percentage)
    {
        // starts the lerping of the health bar and sends the health values
        StartCoroutine(LerpHealthBar(percentage));
    }

    // coroutine to lerp the health bar to the correct amount after damage
    private IEnumerator LerpHealthBar(float healthValues)
    {
        float current = _healthBar.fillAmount; // current amount of health set by the fill amount of the image component
        float target = healthValues; // healthValues.y contains the percentage of the health
        float timer = 0f; // to keep track of the lerp duration
        float duration = _lerpDuration; // lerp duration set from inspector

        while (timer < duration)
        {
            timer += Time.deltaTime; // increase timer every loop
            float progress = timer / duration; // get the progress of each loop to use for interpolation
            float lerped = Mathf.Lerp(current, target, progress); // value given every leep from current to target by progress
            _healthBar.fillAmount = lerped; // set the fillAmount of the image to that lerped target

            yield return null;
        }
    }
}