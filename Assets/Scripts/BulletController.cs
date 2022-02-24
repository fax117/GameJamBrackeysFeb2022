using System;
using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;

namespace DefaultNamespace
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }
}