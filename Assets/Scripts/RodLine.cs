using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodLine : MonoBehaviour
{
    // will draw a line from the player to the fishing area
    public Transform rodPosition;
    public Transform fishingArea;
    [SerializeField] LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // configure line renderer
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.positionCount = 2;

        StartCoroutine(DrawLine());

    }

    IEnumerator DrawLine() {
        // draw a line from the player to the fishing area
        while (true) {
            // Debug.Log("Drawing line");
            for (float i = 0; i < 1; i += 0.01f) {
                Vector3 start = rodPosition.position;
                Vector3 end = fishingArea.position;
                // move the line up and down
                end.y += Mathf.Sin(i * Mathf.PI) * 0.07f;

                lineRenderer.SetPosition(0, start);
                lineRenderer.SetPosition(1, end);

                yield return new WaitForSeconds(0.01f);
            }
        }
    }
    
    void PullLineAnimation() {
        

    }

}
