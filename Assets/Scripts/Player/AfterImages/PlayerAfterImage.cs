using UnityEngine;

namespace Player.AfterImages
{
    public class PlayerAfterImage : MonoBehaviour
    {
        private float activeTime = 0.1f;
        private float timeActivated;
        private float alpha;
        private float alphaSet = 0.8f;
        private float alphaMultiplier = 0.85f;

        private Transform playerRef;
        private SpriteRenderer sr;
        private SpriteRenderer playerSr;
        private Color color;

        private void OnEnable()
        {
            sr = GetComponent<SpriteRenderer>();
            playerRef = FindObjectOfType<PlayerMovement>().transform;
            playerSr = playerRef.GetComponent<SpriteRenderer>();

            alpha = alphaSet;
            sr.sprite = playerSr.sprite;

            transform.position = playerRef.position;
            transform.rotation = playerRef.rotation;

            timeActivated = Time.time;
        }

        private void Update()
        {
            alpha *= alphaMultiplier;
            color = new Color(1f, 1f, 1f, alpha);
            sr.color = color;

            if (Time.time >= (timeActivated + activeTime))
            {
                PlayerAfterImagePool.Instance.AddToPool(gameObject);
            }
        }
    }
}
