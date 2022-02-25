using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationController : MonoBehaviour
{
    private Animator _characterAnimator;

    private void Start()
    {
        _characterAnimator = GetComponent<Animator>();
    }

    public IEnumerator TransformCountdown()
    {
        _characterAnimator.SetTrigger("Transform"); //here goes the animation human -> werewolf
        yield return new WaitForSeconds(10);
        _characterAnimator.SetTrigger("TransformToHuman"); //here goes the animation werewolg -> human
    }

    public void Test()
    {
        Debug.Log("Funciona");
    }
}
