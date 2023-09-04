using System;
using UnityEngine;

public class Bomb2 : MonoBehaviour
{

    private Vector3 endPosition = Vector3.zero;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private float g = 1f;
    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject planetHitEffect = null;
    void Start()
    {
        rigidbody2d.AddForce(new Vector2(
            Dome.Instance.transform.position.x - this.transform.position.x,
            Dome.Instance.transform.position.y - this.transform.position.y
            ).normalized * g);

    }

    
    void Update()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, endPosition, speed * Time.deltaTime);
        if (this.transform.position.y > CameraManager.Instance.topLeftCorner.y + 1 ||
           this.transform.position.y < CameraManager.Instance.bottomLeftCorner.y - 1 ||
           this.transform.position.x > CameraManager.Instance.bottomRightCorner.x + 1 ||
           this.transform.position.x < CameraManager.Instance.topLeftCorner.x - 1)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))//hit player->bounce
        {
            this.gameObject.GetComponentInChildren<SpriteRenderer>().sprite= sprite;
            rigidbody2d.velocity = new Vector2(0, 0);
            rigidbody2d.AddForce(new Vector2(
                this.transform.position.x - Player.Instance.transform.position.x,
                this.transform.position.y - Player.Instance.transform.position.y
                ).normalized * g);
            this.gameObject.layer = LayerMask.NameToLayer("PlayerProjectile");
            return;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Invader"))//hit invader->destroy
        {
            if (planetHitEffect != null)
            {
                Instantiate(planetHitEffect, transform.position, transform.rotation, null);
            }
            Destroy(this.gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Planet"))//hit planet->destroy
        {
            if (planetHitEffect != null)
            {
                Instantiate(planetHitEffect, transform.position, transform.rotation, null);
            }
            Dome.Instance.LowerHeathBy(1);
            Destroy(this.gameObject);
        }
    }

}
