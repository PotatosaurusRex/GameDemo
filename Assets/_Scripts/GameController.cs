using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LP.TurnBasedStrategyTutorial
{
    public class GameController : MonoBehaviour
    {
        // reference objects
        [SerializeField] private GameObject player = null;
        [SerializeField] private GameObject enemy = null;
        [SerializeField] private Slider playerHealth = null;
        [SerializeField] private Slider enemyHealth = null;
        [SerializeField] private Button attackBtn = null;
        [SerializeField] private Button healBtn = null;

        private bool isPlayerTurn = true;

        // attack function
        private void Attack(GameObject target, float damage)
        {
            if (target == enemy)
            {
                enemyHealth.value -= damage;
            }
            else
            {
                playerHealth.value -= damage;
            }

            ChangeTurn();
        }

        private void Heal(GameObject target, float amount)
        {
            if (target == enemy)
            {
                enemyHealth.value += amount;
            }
            else
            {
                playerHealth.value += amount;
            }

            ChangeTurn();
        }

        public void BtnAttack()
        {
            Attack(enemy, 10);
        }

        public void BtnHeal()
        {
            Heal(player, 5);
        }

        private void ChangeTurn()
        {
            isPlayerTurn = !isPlayerTurn;

            if (!isPlayerTurn)
            {
                attackBtn.interactable = false;
                healBtn.interactable = false;

                StartCoroutine(EnemyTurn());
            }
            else 
            {
                attackBtn.interactable = true;
                healBtn.interactable = true;
            }
        }

        private IEnumerator EnemyTurn()
        {
            yield return new WaitForSeconds(1);

            int random = 0;
            random = Random.Range(1, 3); // inclusive, exclusive

            if (random == 1)
            {
                Attack(player, 12);
            } else
            {
                Heal(enemy, 3);
            }
        }
    }
}
