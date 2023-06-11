using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public GameObject followObject;
    [SerializeField] public Vector2 followOffset;
    [SerializeField] public float speed = 7.0f;
    private Vector2 threshold;

    void Start(){
        threshold = CalcluateThreshold();
    }

    // Update is called once per frame
    void FixedUpdate(){
        if(followObject){

            Vector2 follow = followObject.transform.position;

            float xDiff = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
            float yDiff = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);
            
            Vector3 newPosition = transform.position;
            if(Mathf.Abs(xDiff) >= threshold.x){
                newPosition.x = follow.x;
            }
            if(Mathf.Abs(yDiff) >= threshold.y){
                newPosition.y = follow.y;
            }

            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        }
    }

    
    private Vector3 CalcluateThreshold()
    {

        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height,
                                Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Vector2 border = CalcluateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1)); 
    }
}
