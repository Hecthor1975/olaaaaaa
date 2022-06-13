using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcos.Taller
{
    [System.Serializable]
    public class MoveKeys
    {
        public KeyCode leftKeyCode = KeyCode.A;
        public KeyCode rightKeyCode = KeyCode.D;
    }
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class MvRelative : MonoBehaviour
    {
        public MoveKeys normal = new MoveKeys();
        public MoveKeys alt = new MoveKeys();

        private Rigidbody2D m_body2d;
        private SpriteRenderer m_SR;

        public float m_maxSpeed = 4.5f;
        private bool isMoving = false;

        public bool flipWithScale;
        public bool flipWithSprite;

        // Start is called before the first frame update
        void Start()
        {
            m_body2d = GetComponent<Rigidbody2D>();
            m_SR = GetComponentInChildren<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            int moveDirection = GetMoveDirection();
            // Swap direction of sprite depending on move direction
            if (moveDirection > 0)
            {
                if (flipWithSprite && m_SR != null) m_SR.flipX = false;
                if (flipWithScale) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            else if (moveDirection < 0)
            {
                if (flipWithSprite && m_SR != null) m_SR.flipX = true;
                if (flipWithScale) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            m_body2d.velocity = new Vector2(moveDirection * m_maxSpeed, m_body2d.velocity.y);
        }

        public int GetMoveDirection()
        {
            int moveDirection = 0;
            bool isKeyPressed = false;

            if (Input.GetKey(normal.leftKeyCode) || Input.GetKey(alt.leftKeyCode))
            {
                moveDirection += -1;
                isKeyPressed = true;
            }
            if (Input.GetKey(normal.rightKeyCode) || Input.GetKey(alt.rightKeyCode))
            {
                moveDirection += 1;
                isKeyPressed = true;
            }

            isMoving = isKeyPressed;
            return moveDirection;
        }

        public bool IsMoving()
        {
            return isMoving;
        }
    }
}
