using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodLine : MonoBehaviour
{
    // will draw a line from the player to the fishing area
    public Transform rodPosition;
    public Transform fishingArea;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float lineSpeed = 0.003f;
    [SerializeField] float curveAmount = -0.05f;

    // Start is called before the first frame update
    void Start()
    {
        // configure line renderer
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.positionCount = 3;
    }

    public void StartDrawLine()
    {
        lineRenderer.enabled = true;
        StartCoroutine(DrawLine());
    }
   
    IEnumerator DrawLine() {
        // draw a line from the player to the fishing area
        while (true) {
           for (float i = 0; i < 1; i += lineSpeed) {
                Vector3 start = rodPosition.position;
                Vector3 end = fishingArea.position;
                
                // make the line curved in the middle
                Vector3 middle = Vector3.Lerp(start, end, i);
                middle.y += Mathf.Sin(i * Mathf.PI) * curveAmount;
                

                lineRenderer.SetPosition(0, start);
                lineRenderer.SetPosition(1, middle);
                lineRenderer.SetPosition(2, end);

                 yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        } 
    }

    public void StopDrawLine() {
        // remove line
        StopCoroutine(DrawLine());
        lineRenderer.enabled = false;
    }
    
    void PullLineAnimation() {
        

    }

}
