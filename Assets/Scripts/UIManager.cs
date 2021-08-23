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
        [SerializeField] GameObject weaponUI;
        [SerializeField] GameObject maskImage;
        [SerializeField] GameObject sringeImage;
        [SerializeField] GameObject gelImage;
        [SerializeField] Animator cinematicBarAnimator;
        [SerializeField] Animator phoneAnimator;
        [SerializeField] GameObject cinematicBar;

        private bool isPhoneShowed = false;

        private void Start()
        {
            if (cinematicBarAnimator != null && cinematicBar != null)
                SetCinematicBarsActive(true);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Tab)){
                if (isPhoneShowed)
                    phoneAnimator.SetTrigger("HideObjectives");
                else
                    phoneAnimator.SetTrigger("ShowObjectives");

                isPhoneShowed = !isPhoneShowed;
            }
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

        public void ShowWeaponUI(bool value){
            weaponUI.SetActive(value);
        }
    }
}
