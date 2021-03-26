using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleManager : MonoBehaviour
{
    [SerializeField] private List<Animator> rightTentacles;
    [SerializeField] private List<Animator> leftTentacles;

    public void Attack()
    {
        // choose tentacles
        int[] rightTentacleSelection = new int[2] {-1,-1 };
        int[] leftTentacleSelection = new int[2] {-1,-1 };

        // Select right tentacles
        rightTentacleSelection[0] = Random.Range(0, 3);
        do
        {
            int otherTentacle = Random.Range(0, 3);
            if (otherTentacle != rightTentacleSelection[0])
                rightTentacleSelection[1] = otherTentacle;
        } while (rightTentacleSelection[1] == -1);

        // Select right tentacles
        leftTentacleSelection[0] = Random.Range(0, 3);
        do
        {
            int otherTentacle = Random.Range(0, 3);
            if (otherTentacle != leftTentacleSelection[0])
                leftTentacleSelection[1] = otherTentacle;
        } while (leftTentacleSelection[1] == -1);

        Debug.Log("Right : " + rightTentacleSelection[0] + " , " + rightTentacleSelection[1]);
        Debug.Log("Left : " + leftTentacleSelection[0] + " , " + leftTentacleSelection[1]);
        // Make them attack
        rightTentacles[rightTentacleSelection[0]].SetTrigger("Attack");
        rightTentacles[rightTentacleSelection[1]].SetTrigger("Attack");
        leftTentacles[leftTentacleSelection[0]].SetTrigger("Attack");
        leftTentacles[leftTentacleSelection[1]].SetTrigger("Attack");
    }
}
