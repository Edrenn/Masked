using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMaster : ProgressCheckpoint
{
    [SerializeField] private RotationNode finalNode;

    // Start is called before the first frame update
    void Start()
    {
        finalNode.SetOnGoalReached(SetCheckpointReady);
    }
}
