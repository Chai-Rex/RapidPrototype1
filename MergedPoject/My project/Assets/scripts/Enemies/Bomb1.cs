using System;
using UnityEngine;

public class Bomb1 : MonoBehaviour
{

    private Vector3 endPosition = Vector3.zero;
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private float g = 1f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    void Start()
    {
        rigidbody2d.AddForce(new Vector2(
            Dome.Instance.transform.position.x - this.transform.position.x,
            Dome.Instance.transform.position.y - this.transform.position.y
            ).normalized * g);

        //Bomb rotation
        Vector3 dir = endPosition - this.transform.position;
        dir = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg+90f;
        angle = angle < 0 ? angle + 360 : angle;
        this.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    void Update()
    {
        //Bomb rotation
        Vector3 dir = endPosition - this.transform.position;
        dir = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
        angle = angle < 0 ? angle + 360 : angle;
        this.transform.eulerAngles = new Vector3(0, 0, angle);
        this.spriteRenderer.sprite = spriteRenderer.sprite;
        //Bomb movement
        transform.position = Vector3.MoveTowards(this.transform.position, endPosition, speed * Time.deltaTime);

        //Out of camera
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
            Destroy(this.gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Planet"))//hit planet->destroy
        {
            Dome.Instance.LowerHeathBy(1);
            Destroy(this.gameObject);
        }

    }

}
