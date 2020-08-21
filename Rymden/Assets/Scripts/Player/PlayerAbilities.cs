using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Genneral Properties")]
    public Image abilityIcon;
    public Sprite defultImage;

    [Header("Shield Relevant Properties")]
    public float[] activeTime;
    public float[] shieldSize;
    public GameObject shieldObject;
    GameObject _tempSheildClone = null;

    [Header("Debug")]
    [Tooltip("Activate abilit? True: Yes || False = No")]
    public bool activateAbility = false;
    [Tooltip("0: Shield\n1: Multishot\n")]
    [SerializeField] int _activeAbilityIndex = -1;
    [Tooltip("0: Common\n1: Rare\n2: Legendary")]
    [SerializeField] int _activeAbilityRarity = -1;
    [SerializeField] private float activationTime;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && _activeAbilityIndex > -1)
        {
            activateAbility = true;
            ActivateAbility();
        }

        if (activateAbility)
        {
            abilityIcon.color = new Color(abilityIcon.color.r, abilityIcon.color.g, abilityIcon.color.b, 0.5f);     //Fades UI icon
            abilityIcon.fillAmount -= (1f / activationTime) * Time.deltaTime; //Drain the hud image to display the timer

            if (_tempSheildClone != null)
            {
                TrackPlayer(_tempSheildClone);
            }
        }
    }

    public void ActivateAbility()
    {
        if (activateAbility)
        {
            switch (_activeAbilityIndex)
            {
                case 0:     //Shield
                    #region SHIELD
                    {
                        GameObject _shieldClone = Instantiate(shieldObject); //Spawn a sheild for x seconds
                        switch (_activeAbilityRarity)
                        {
                            case 0:
                                {
                                    _shieldClone.GetComponent<MeshRenderer>().sharedMaterial.SetColor("Color_B8352066", Color.white * 10);
                                    break;
                                }

                            case 1:
                                {
                                    _shieldClone.GetComponent<MeshRenderer>().sharedMaterial.SetColor("Color_B8352066", Color.green * 10);
                                    break;
                                }

                            case 2:
                                {
                                    _shieldClone.GetComponent<MeshRenderer>().sharedMaterial.SetColor("Color_B8352066", Color.yellow * 10);
                                    _shieldClone.AddComponent<LegendarySheild>();
                                    break;
                                }
                        }

                        _shieldClone.transform.localScale = new Vector3(shieldSize[_activeAbilityRarity], shieldSize[_activeAbilityRarity], shieldSize[_activeAbilityRarity]);
                        activationTime = activeTime[_activeAbilityRarity];
                        _tempSheildClone = _shieldClone;
                        Destroy(_shieldClone, activationTime);
                        Invoke("RemoveAbilty", activationTime);
                        break;
                    }
                #endregion

                case 1:     //Multishot
                    #region MULTISHOT
                    {
                        if (_activeAbilityRarity == 2)      //if the item is legendary
                        {
                            //Multiply Shots (timed/amount???)
                            //Activate special ability
                            //Active special abilty
                        }

                        //Multiply Shots
                        //Drain the hud image to display the timer
                        activationTime = activeTime[_activeAbilityRarity];
                        Invoke("RemoveAbilty", activationTime);

                        break;
                    }
                    #endregion
            }
        }

    }

    void TrackPlayer(GameObject _trackObject)
    {
        _trackObject.transform.position = transform.position;
    }

    public void RemoveAbilty()
    {
        switch (_activeAbilityIndex)
        {
            case 0:
                {
                    _tempSheildClone = null;
                    break;
                }
        }

        #region HUD Reset

        activateAbility = false;                                //Deactivates button
        abilityIcon.sprite = defultImage;                       //Resets image
        abilityIcon.color = new Color(0.2f, 0.2f, 0.2f, 1f);    //Resets item color
        abilityIcon.fillAmount = 1;                             //Resets image value

        #endregion

        #region Ability Reset

        _activeAbilityIndex = -1;           //Reset _rarity
        _activeAbilityRarity = -1;          //Reset _ability

        #endregion
    }

    public void GetAbilityInfo(int _abilityIndex, int _rarity)   //Collect info about the item
    {
        _activeAbilityIndex = _abilityIndex;
        _activeAbilityRarity = _rarity;
    }
}
