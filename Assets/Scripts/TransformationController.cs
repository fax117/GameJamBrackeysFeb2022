using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationController : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;
    public Sprite werewolf;
    public Sprite human;

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public IEnumerator TransformCountdown()
    {
        _spriteRenderer.sprite = werewolf; //here goes the animation human -> werewolf
        yield return new WaitForSeconds(10);
        _spriteRenderer.sprite = human; //here goes the animation werewolg -> human
    }

    public void Test()
    {
        Debug.Log("Funciona");
    }
}
