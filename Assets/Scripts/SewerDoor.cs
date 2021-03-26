using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewerDoor : MonoBehaviour
{
    [SerializeField] int healthPoint;

    private int shotCounter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            if (projectile.ProjectileType == ProjectileTypeEnum.Sringe)
            {
                healthPoint--;
                if (healthPoint <= 0)
                {
                    Destroy(this.gameObject);
                }
                else
                    GetComponent<Animator>().SetTrigger("TakeDamage");
                Destroy(projectile.gameObject);
            }
            else
            {
                LaunchDialog();
                Destroy(projectile.gameObject);
            }

        }
    }

    private void LaunchDialog()
    {
        FindObjectOfType<Player>().SetCanDoThings(false);
        DialogBox dialogBox = FindObjectOfType<DialogBox>();
        dialogBox.endOfDialogAction += LaunchNextLevel;
        dialogBox.AddNewLines(new List<Dialog>()
        {
            new Dialog("","*Téléphone sonne ...*"),
            new Dialog("Moi","Euh ..."),
            new Dialog("Moi","C'est bloqué ..."),
            new Dialog("Scientifique","Bon bah rentre ..."),
        });
    }

    private void LaunchNextLevel()
    {
        FindObjectOfType<SceneController>().LoadLevel(5);
    }
}
