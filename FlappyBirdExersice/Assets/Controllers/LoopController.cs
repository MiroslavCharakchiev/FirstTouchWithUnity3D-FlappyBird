using UnityEngine;

public class LoopController : MonoBehaviour
{
    private float offsetX;
    public GameObject Player;

    public void Start()
    {
        this.offsetX = this.transform.position.x - this.Player.transform.position.x;
    }

    public void Update()
    {
        var position = this.transform.position;
        position.x = this.Player.transform.position.x + offsetX;
        this.transform.position = position;
    }
}
