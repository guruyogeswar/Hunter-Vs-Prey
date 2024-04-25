using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
public class AgentController : Agent
{
    [SerializeField] private Transform target;
    public float pelletcount;
    public GameObject food;

    [SerializeField] private List<GameObject> spawnedtarget = new List<GameObject>();

    [SerializeField] private Transform envlocation;

    [SerializeField] private float movespeed;
    private Rigidbody rb;

    public HunterController classobject;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }


    public override void OnEpisodeBegin()
    {
        //Agent
        transform.localPosition = new Vector3(Random.Range(-4f, 4f), 0.5f, Random.Range(-4f, 4f));

        //target.localPosition = new Vector3(Random.Range(-4f, 4f), 0.3f, Random.Range(-4f, 4f));
        removepellet();
        createtarget();
    }

    private void createtarget()
    {
        for (int i = 0; i < pelletcount; i++)
        {
            int counter = 0;
            bool overlap;

            GameObject newtarget = Instantiate(food);
            //make pellet child of env
            newtarget.transform.parent = envlocation;
            //random position
            Vector3 targetlocation = new Vector3(Random.Range(-4f, 4f), 0.3f, Random.Range(-4f, 4f));
            //check overlap
            /*
            if(spawnedtarget.Count != 0) 
            {
                for(int k=0; k<spawnedtarget.Count; k++) 
                {
                    if(counter < 10)
                    {
                        overlap = checkoverlap(targetlocation, spawnedtarget[k].transform.localPosition, 0.5f);
                        if (overlap == false)
                        {
                            targetlocation = new Vector3(Random.Range(-4f, 4f), 0.3f, Random.Range(-4f, 4f));
                            k--;
                            counter++;
                            
                        }
                    }
                    else 
                    {
                        k= spawnedtarget.Count;
                    }
                }
            }
            */
            newtarget.transform.localPosition = targetlocation;
            newtarget.transform.localPosition = targetlocation;
            //add to list 
            spawnedtarget.Add(newtarget);
        }
    }
    public bool checkoverlap(Vector3 positionofnew, Vector3 positionofold, float mindistance)
    {
        float distance = Vector3.Distance(positionofnew, positionofold);
        if (distance < mindistance)
        {
            return true;
        }
        return false;
    }

    public void removepellet()
    {
        for (int i = 0; i < spawnedtarget.Count; i++)
        {
            Destroy(spawnedtarget[i]);
        }
        spawnedtarget.Clear();
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
        if (other.gameObject.tag == "target")
        {
            spawnedtarget.Remove(other.gameObject);
            Destroy(other.gameObject);
            AddReward(10f);
            if (spawnedtarget.Count == 0)
            {
                AddReward(5f);
                classobject.AddReward(-10f);
                EndEpisode();
            }
            /*            SetReward(10f);
                        Destroy(other.gameObject);
                        pelletcount--;

                        if (pelletcount == 0)
                        {
                            EndEpisode();
                        }*/
        }
        if (other.gameObject.tag == "wall")
        {
            AddReward(-15f);
            removepellet();
            classobject.EndEpisode();
            EndEpisode();
        }
    }
}