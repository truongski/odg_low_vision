using UnityEngine;

namespace Core
{
    public class MouseMoveable : MonoBehaviour
    {
        private Vector3 originalPosition;
        private float lastClick;
        private Vector3 previousPosition;

        private bool pressed;
        public float sensitivity = 0.5f;

        public float doubleClickDelay = 0.5f;

        private void Start()
        {
            originalPosition = this.transform.localPosition;
            previousPosition = Input.mousePosition;
        }
        private void Update()
        {
            var currentPosition = Input.mousePosition;
            Vector3 deltaPosition = currentPosition - previousPosition;

            if (Input.GetMouseButton(0))
            {
                Vector3 forward = this.transform.parent.transform.forward * deltaPosition.y * sensitivity;
                this.transform.position += forward;

                pressed = true;
            }
            else
            {
                if (pressed)
                {
                    if (Time.time < lastClick + doubleClickDelay)
                    {
                        this.transform.localPosition = this.originalPosition;
                    }

                    lastClick = Time.time;
                    pressed = false;
                }
            }

            previousPosition = currentPosition;
        }
    }
}