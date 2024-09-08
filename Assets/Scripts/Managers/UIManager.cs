using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject damageTextPrefab;

    [SerializeField]
    private GameObject healingTextPrefab;

    Canvas canvas;

    private void Awake()
    {
        // Search scene for active canvas
        CharacterEvents.characterHit += CreateDamageText;
        CharacterEvents.characterHealed += CreateHealthText;
    }

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    private void OnDisable()
    {
        CharacterEvents.characterHit -= CreateDamageText;
        CharacterEvents.characterHealed += CreateHealthText;
    }

    private void CreateDamageText(Damageable characterHit, int damageTaken)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(characterHit.transform.position);

        TMP_Text damageTextInstance = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();
        damageTextInstance.text = damageTaken.ToString();
    }

    private void CreateHealthText(Damageable characterHealed, int healthReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(characterHealed.transform.position);

        TMP_Text healingTextInstance = Instantiate(healingTextPrefab, spawnPosition, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();
        healingTextInstance.text = healthReceived.ToString();
    }

    // Close the game
    public void OnExitGame(InputAction.CallbackContext context)
    {
        Debug.Log("OnExitGame called");
        Application.Quit();
    }
}