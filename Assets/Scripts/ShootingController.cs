using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [Header("Behaviour")] 
    [SerializeField] protected bool _linear = true;
    [SerializeField] protected bool _orbiting = false;

    [Header("Bullet options")] 
    [SerializeField] protected float _rateOfFire = 1f;
    [SerializeField] protected int _numberOfShoots = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
