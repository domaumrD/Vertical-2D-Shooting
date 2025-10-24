using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float moveSpeed;

    void Update()
    {
        this.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        if (this.transform.position.y < -11)
        {
            this.transform.position = new Vector3(this.transform.position.x, 11);
        }
    }
}
