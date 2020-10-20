using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class PlayerAvoidanceAgent : Agent
{
    public float speed = 10.0f;

    public Vector3 idlePosition = new Vector3(0, 0.5f, -7);

    public Vector3 leftPostiion = Vector3.zero;

    public Vector3 rightPosition = Vector3.zero;

    private Vector3 moveTo = Vector3.zero;

    private Vector3 prevPosition = Vector3.zero; // dit moet je meegeven, anders blijft de agent op zelfde plaats staan

    public TargetMoving TargetMoving;

    int punishCounter = 0;



    public override void OnEpisodeBegin()
    {

        transform.localPosition = idlePosition;
        moveTo = prevPosition = idlePosition;
        TargetMoving.ResetTarget();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //3 observaties van mezelf: x y en z
        sensor.AddObservation(transform.localPosition);

        sensor.AddObservation(TargetMoving.transform.localPosition);
    }


    public override void OnActionReceived(float[] vectorAction)
    {
        Vector3 translation = Vector3.zero;

        float sense = 3f / 2f * Mathf.Pow(vectorAction[0], 2) - 5f / 2f * vectorAction[0];
        translation = transform.right * speed * sense * Time.deltaTime;

        transform.Translate(translation, Space.World);

        if (Mathf.Abs(transform.localPosition.x) > 10)
        {
            AddReward(-1f);
        }

        AddReward(0.001f);

        //        //prevPosition = moveTo;
        //        int direction = Mathf.FloorToInt(vectorAction[0]);
        //        moveTo = idlePosition;

        //        switch (direction)
        //        {
        //            case 0:
        //                moveTo = idlePosition;
        //                break;
        //            case 1:
        //                moveTo = leftPostiion;
        //                break;
        //            case 2:
        //                moveTo = rightPosition;
        //                break;
        //        }

        ////        transform.localPosition = idlePosition; => de agent zal te snel bewegen om collisions te detecteren

        //        transform.localPosition = Vector3.MoveTowards(transform.localPosition, moveTo, Time.deltaTime * speed);

        //        if (Mathf.Abs(transform.localPosition.x) > 10)
        //        {
        //            AddReward(-1f);
        //        }

        //        AddReward(0.001f);

        //        //if (prevPosition == moveTo)
        //        //    punishCounter++;

        //        //if (punishCounter > 2)
        //        //{
        //        //    AddReward(-0.01f);
        //        //    punishCounter = 0;
        //}

    }

    //public void TakeAwayPoints()
    //{
    //    AddReward(-0.5f);
    //    TargetMoving.ResetTarget();
    //    EndEpisode();
    //}

    //public void GivePoints()
    //{
    //    AddReward(1f);
    //    TargetMoving.ResetTarget();
    //    EndEpisode();
    //}

    public override void Heuristic(float[] actionsOut)
    {

        //idle
        if (Input.GetKey(KeyCode.DownArrow))
        {
            actionsOut[0] = 0;
        }

        //move left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            actionsOut[0] = 1;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            actionsOut[0] = 2;
        }


    }

}
