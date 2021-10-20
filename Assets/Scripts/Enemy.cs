using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if __DEBUD_AVAILABLE__

using UnityEditor;

#endif

public class Enemy : MonoBehaviour
{
    public Transform player;

    public float speed = 2;

    public float followSpeed = 2f;
    public float followDistance = 6f;

    float distance;
    //Debug Mode

    Vector3 playerOffset;
    Vector3 playerOffsetProjected;
    Vector3 playerOffsetNormalized;
    void Start()
    {
        
    }

    #if __DEBUD_AVAILABLE__

    private void OnDrawGizmos()
    {
        if(Switches.debugMode && Switches.debugShowIds)
        {
           Handles.Label(transform.position + new Vector3(0, 0.2f, 0), gameObject.name);
        }
        
        if(Switches.debugMode && Switches.debugShowEnemyFollowInfo)
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawWireSphere(transform.position, followDistance);

            if(distance < followDistance)
            {
                Gizmos.DrawLine(transform.position, playerOffset + transform.position);//Tienes que saber dónde esta en la posición mundo el enemigo
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, playerOffsetProjected + transform.position);
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, playerOffsetNormalized + transform.position);
                Handles.Label(transform.position + new Vector3(0, 1.2f, 0), "distance: " + distance);
            }



        }
    }

    #endif

    // Update is called once per frame
    void Update()
    {
        transform.position += -Vector3.right * speed * Time.deltaTime;
        if (gameObject.name == "Enemy07")
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        playerOffset = player.position - transform.position;
        playerOffset = new Vector3(playerOffset.x, playerOffset.y, 0);

        float distance = playerOffset.magnitude;

        if(distance < followDistance)
        {
            playerOffsetProjected = new Vector3(0, playerOffset.y, 0);
            playerOffsetNormalized = playerOffset.normalized;

            transform.position += playerOffsetNormalized * followSpeed * Time.deltaTime;
        }




    }
}
