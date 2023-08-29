using UnityEngine;

public class Projectile : MonoBehaviour {
    public Vector3 direction;
    public float speed;

    public System.Action destroyed;


    private void Update() {
        this.transform.position += direction * speed * Time.deltaTime;

        if (this.transform.position.y > Camera.main.orthographicSize * 2) {
            //this.destroyed.Invoke();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //if (this.destroyed != null) {
        //    this.destroyed.Invoke();
        //}
        Destroy(this.gameObject);
    }

}
