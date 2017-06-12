using UnityEngine;

public class BGCollideController : MonoBehaviour
{
    private int numbersOfBg;
    private float distanceBetweenBg;

    private int numbersOfGr;
    private float distanceBetweenGr;

    private int numbersOfPipes;
    private float distanceBetweenPipe;

    private bool upperPipe;

    public void Start()
    {
        var backgrounds = GameObject.FindGameObjectsWithTag("Background");
        this.numbersOfBg = backgrounds.Length;

        var grounds = GameObject.FindGameObjectsWithTag("Ground");
        this.numbersOfGr = grounds.Length;

        var pipe = GameObject.FindGameObjectsWithTag("Pipe");
        this.numbersOfPipes = pipe.Length;

        randomizePipe(pipe);

        if (numbersOfBg < 2 || numbersOfGr < 2 || numbersOfPipes < 2)
        {
            throw new System.InvalidOperationException("the Pipes, Grounds and Backgrounds must be atleast two!");
        }
       this.distanceBetweenBg = 3.5f;

      this.distanceBetweenGr = 3.5f;

        Mathf.Abs(this.distanceBetweenPipe = pipe[1].transform.position.x - pipe[0].transform.position.x);

    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Background"))
        {
            var background = collider.transform.position;
            background.x += (distanceBetweenBg * numbersOfBg);
            collider.transform.position = background;
        }
        if (collider.CompareTag("Ground"))
        {
            var ground = collider.transform.position;
            ground.x += (distanceBetweenGr * numbersOfGr);
            collider.transform.position = ground;
        }
        if (collider.CompareTag("Pipe"))
        {
            var pipe = collider.transform.position;
            pipe.x += distanceBetweenPipe * numbersOfPipes;

            float randomY;
            if (this.transform)
            {
                randomY = Random.Range(-1, 0.5f);
            }
            else
            {
                randomY = Random.Range(1.5f, 3);
            }
            pipe.y = randomY;
            this.upperPipe = !this.upperPipe;
            collider.transform.position = pipe;

        }
    }
    private void randomizePipe(GameObject[] pipe)
    {
        var count = 0;
        for (int i = 0; i < pipe.Length; i++)
        {
            count++;
            var currentPipe = pipe[i];
            float randomY;
            if (count % 2 == 0)
            {
                randomY = Random.Range(1.5f, 3);

            }
            else
            { 
                randomY = Random.Range(-1, 1f);

            }
            var pipePosition = currentPipe.transform.position;
            pipePosition.y = randomY;
            currentPipe.transform.position = pipePosition;
        }
    }

}
