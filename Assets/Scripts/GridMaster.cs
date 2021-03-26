using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridMaster : ProgressCheckpoint
{
    [SerializeField] private SpriteRenderer finalDestination;
    private List<RotationBlock> allRotationBlocks;

    private void Start() {
        allRotationBlocks = FindObjectsOfType<RotationBlock>().ToList();

        foreach (var block in allRotationBlocks)
        {
            block.OnRotateEvent += CheckAllRotationBlock;
        }
    }

    private void CheckAllRotationBlock(){
        bool areAllBlocksReady = true;

        foreach (var block in allRotationBlocks)
        {
            if (block.isTargetReached == false)
                areAllBlocksReady = false;
        }
        
        if (areAllBlocksReady == true){
            foreach (var block in allRotationBlocks)
            {
                block.SetGreenColor();
            }

            finalDestination.color = Color.green;

            foreach (var block in FindObjectsOfType<RotationOnClick>())
            {
                block.SetRotationEnabled(false);
            }

            SetCheckpointReady();
        }
    }
}
