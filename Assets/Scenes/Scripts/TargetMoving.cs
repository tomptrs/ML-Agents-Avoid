using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMoving : MonoBehaviour
{

    public PlayerAvoidanceAgent Agent = null;

    public float targetSpeed = 5f;

    public float maxDistance { get; set; }

    private Vector3 originalPosition;

    private void Awake()
    {
        originalPosition = transform.localPosition;
        
    }

    private void Update()
    {
      //  if (transform.localPosition.z <= (originalPosition.z - maxDistance))
        //    transform.localPosition = originalPosition;
     //   else
     //   {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - (Time.deltaTime * targetSpeed));

      //  }
    }

    public void ResetTarget()
    {
        transform.localPosition = originalPosition;
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;


    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.tag == "player")
    //    {
    //        Agent.AddReward(-1f);
    //        Agent.EndEpisode();
    //    }
    //    if (other.transform.tag == "wall")
    //    {
    //        Agent.AddReward(1f);
    //        Agent.EndEpisode();

    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("player"))
        {
            Agent.AddReward(-1f);
            Agent.EndEpisode();
        }
        if (collision.transform.CompareTag("wall"))
        {
            Agent.AddReward(1f);
            Agent.EndEpisode();
        }
    }


}
