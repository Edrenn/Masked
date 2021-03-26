using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject maskImage;
        [SerializeField] GameObject sringeImage;
        [SerializeField] GameObject gelImage;
        [SerializeField] Animator cinematicBarAnimator;
        [SerializeField] GameObject cinematicBar;

        private void Start()
        {
            if (cinematicBarAnimator != null && cinematicBar != null)
                SetCinematicBarsActive(true);
        }

        public void SetMaskImage()
        {
            maskImage.SetActive(true);
            sringeImage.SetActive(false);
            gelImage.SetActive(false);
        }

        public void SetSringeImage()
        {
            sringeImage.SetActive(true);
            maskImage.SetActive(false);
            gelImage.SetActive(false);
        }

        public void SetGelImage()
        {
            gelImage.SetActive(true);
            sringeImage.SetActive(false);
            maskImage.SetActive(false);
        }

        public void SetCinematicBarsActive(bool value)
        {
            if (value)
                cinematicBarAnimator.gameObject.SetActive(true);
            else
            {
                if (cinematicBar.activeInHierarchy)
                {
                    cinematicBarAnimator.SetTrigger("HideBars");
                }
            }
        }

        public void ShowUI(bool value)
        {
            if (value)
                GetComponent<Animator>().SetTrigger("ShowUI");
            else
                GetComponent<Animator>().SetTrigger("UIHide");
        }
    }
}
