using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hunter2D
{
    public class Gun : MonoBehaviour
    {
        [Header("Settings")]
        public Bullet bullet;
        public bool isRightHand;
        public Vector3 right;
        public Vector3 tright;

        private void OnEnable()
        {
            bullet.transform.position = transform.position;
            var obj = Instantiate(bullet);
            obj.localDirection = Vector2.right * (transform.right.y < 0 ? 1 : -1);
            right = Vector2.right * (transform.right.y > 0 ? 1 : -1);
            tright = transform.right;
        }
    }
}
