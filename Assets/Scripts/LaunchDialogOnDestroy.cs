using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class LaunchDialogOnDestroy : MonoBehaviour
    {
        private void OnDestroy()
        {
            DialogBox dialogBox = FindObjectOfType<DialogBox>();
            dialogBox.SetName("*Voix au mégaphone*");
            dialogBox.AddNewLines(new List<Dialog>()
            {
                new Dialog("Ne bougez plus !"),
                new Dialog("Déjà, merci d'avoir vaincu cette créature !"),
                new Dialog("Mais ... vous avez été en contact avec elle."),
                new Dialog("Vous devez donc venir pour qu'on vous analyse."),
                new Dialog("Ne bougez pas on vient vous chercher.")
            });
            dialogBox.SetEndOfDialogAction(new EndOfDialogAction(LaunchNewLevel));
        }
        public void LaunchNewLevel()
        {
            FindObjectOfType<Player>().SetCanDoThings(false);
            SceneController sceneController = FindObjectOfType<SceneController>();
            sceneController.LoadLevel(3);
        }

    }
}
