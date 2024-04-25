using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class HunterController : Agent
{
    [SerializeField] private Transform target; // The agent's target to catch
    [SerializeField] public float movespeed; // The speed of the hunter
    [SerializeField] private Transform envlocation;


    private Rigidbody rb;
    public AgentController classobject;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        // Reset the hunter's position
        transform.localPosition = new Vector3(Random.Range(-4f, 4f), 0.3f, Random.Range(-4f, 4f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        /*This is for giving coordinates
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];
        */

        float moveRotate = actions.ContinuousActions[0];
        float moveForward = actions.ContinuousActions[1];
        Debug.Log(moveRotate);
        Debug.Log(moveForward);

        rb.MovePosition(transform.position + transform.forward * moveForward * movespeed * Time.deltaTime);
        transform.Rotate(0f, moveRotate * movespeed, 0f, Space.Self);
        /*
        Vector3 velocity = new Vector3(moveX, 0, moveY);
        velocity = velocity.normalized * Time.deltaTime * movespeed;
        transform.localPosition += velocity;
        */
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;
        continousActions[0] = Input.GetAxisRaw("Horizontal");
        continousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("prey")) // If the hunter catches the agent
        {
            AddReward(20f); // Penalize the hunter
            other.gameObject.GetComponent<AgentController>().EndEpisode(); // End the episode for the agent
            EndEpisode(); // End the episode for the hunter
        }
        if (other.gameObject.tag == "wall")
        {
            AddReward(-15f);
            //other.gameObject.GetComponent<AgentController>().EndEpisode();
            EndEpisode();
        }
    }
}
