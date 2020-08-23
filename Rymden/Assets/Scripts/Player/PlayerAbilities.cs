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

    [Header("Cluster Bomb Properties")]
    public int[] amountOfBombs;
    public float[] survivalTime;
    public float[] detectionRadius;
    public GameObject projectileObject;
    public Transform shipCannonL;

    [HideInInspector] public bool activateAbility = false;
    [Header("Debug")]
    [Tooltip("0: Shield\n1: Multishot\n")]
    [SerializeField] int _activeAbilityIndex = -1;
    [Tooltip("0: Common\n1: Rare\n2: Legendary")]
    [SerializeField] int _activeAbilityRarity = -1;
    [SerializeField] private float _activationTime;

    void Update()
    {

        if (Input.GetKeyUp(KeyCode.E) && _activeAbilityIndex > -1)
        {
            activateAbility = true;
            ActivateAbility();
        }

        if (activateAbility)
        {
            if (_tempSheildClone != null)
            {
                abilityIcon.color = new Color(abilityIcon.color.r, abilityIcon.color.g, abilityIcon.color.b, 0.5f);     //Fades UI icon
                abilityIcon.fillAmount -= (1f / _activationTime) * Time.deltaTime; //Drain the hud image to display the timer
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
                        _activationTime = activeTime[_activeAbilityRarity];
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
                        _tempSheildClone = _shieldClone;
                        Destroy(_shieldClone, _activationTime);
                        Invoke("RemoveAbilty", _activationTime);
                        break;
                    }
                #endregion

                case 1:     //Tracking Bombs
                    #region TRACKING BOMBS
                    {
                        GameObject _projectileClone = Instantiate(projectileObject, shipCannonL);
                        _projectileClone.transform.position = shipCannonL.position;
                        _projectileClone.transform.parent = null;

                        switch (_activeAbilityRarity)
                        {
                            case 0:
                                {
                                    _projectileClone.GetComponent<CircleCollider2D>().radius = detectionRadius[0];
                                    _projectileClone.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_EmissionColor", Color.white * 5);
                                    break;
                                }

                            case 1:
                                {
                                    _projectileClone.GetComponent<CircleCollider2D>().radius = detectionRadius[1];
                                    _projectileClone.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_EmissionColor", Color.green * 5);
                                    break;
                                }

                            case 2:
                                {
                                    _projectileClone.GetComponent<CircleCollider2D>().radius = detectionRadius[2];
                                    _projectileClone.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_EmissionColor", Color.yellow * 5);
                                    break;
                                }
                        }
                        RemoveAbilty();
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
            case 0:     //Shield
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
