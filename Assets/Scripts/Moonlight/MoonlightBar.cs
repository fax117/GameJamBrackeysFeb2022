using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoonlightBar : MonoBehaviour
{
    [SerializeField] private Image _moonlightBar;

    [SerializeField] private float _lerpDuration = 0.1f;

    public void UpdateMoonlightBar(float moonlightValue)
    {
        StartCoroutine(LerpMoonlightBar(moonlightValue));
    }

    private IEnumerator LerpMoonlightBar(float moonlightValue)
    {
        float current = _moonlightBar.fillAmount; // current amount of moonlight set by the fill amount of the image component
        float target = moonlightValue; // percentage of moonlight
        float timer = 0f; // to keep track of the lerp duration
        float duration = _lerpDuration; // lerp duration set from inspector

        while (timer < duration)
        {
            timer += Time.deltaTime; // increase timer every loop
            float progress = timer / duration; // get the progress of each loop to use for interpolation
            float lerped = Mathf.Lerp(current, target, progress); // value given every leep from current to target by progress
            _moonlightBar.fillAmount = lerped; // set the fillAmount of the image to that lerped target

            yield return null;
        }
    }
}
