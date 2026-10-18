using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class RollerAgent : Agent
{

    public Rigidbody rBody;
    public float forceMultiplier = 10f;
    public Transform Target;

    // Start is called once before the first execution of Update after the Agent is created
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        // If the Agent falls off the platform, reset its position and zero out its velocity
        if (this.transform.localPosition.y < 0)
        {
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.linearVelocity = Vector3.zero;
            this.transform.localPosition = new Vector3(0, 0.5f, 0);
        }

        // Move the Target to a new random position to promote generalization
        Target.localPosition = new Vector3(Random.Range(-4f, 4f), 0.5f, Random.Range(-4f, 4f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // 1. Target Position (x,y,z) - Tells Agent where it needs to go
        sensor.AddObservation(Target.localPosition);

        // 2. Agent Position (x,y,z) - Allows Agent to know where it is now
        sensor.AddObservation(this.transform.localPosition);

        // 3. Agent Velocity (x,z) - Helps Agent control speed and avoid overshooting
        sensor.AddObservation(rBody.linearVelocity.x);
        sensor.AddObservation(rBody.linearVelocity.z);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Receive 2 continuous actions (x, z directions)
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];

        // Apply force to the Rigidbody
        rBody.AddForce(controlSignal * forceMultiplier);

        // Calculate distance to target
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

        // If distance < 1.42 -> reward +1.0 and end episode
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        // If Agent falls (y < 0) -> end episode (no explicit penalty given)
        else if (this.transform.localPosition.y < 0)
        {
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        continuousActionsOut[0] = h;
        continuousActionsOut[1] = v;
    }

}
