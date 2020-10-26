using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private PlayerCounterController _counterController;
    private PlayerMovement _playerMovement;
    private float _poisonedValue = 0;
    public GameObject Maska;
    public GameObject PoisonBar;
    public Image PoisonImageToChange;
    public bool CanMakeSons = false;
    public bool Infected = false;
    public bool CanInfect = true;
    private AudioSource _audioSource;
    private Coroutine _lastCoroutine = null;
    private DragAndDrop _dnd;
    public PlayerType ThisPlayerType = PlayerType.Normal;

    private void Awake()
    {
        PoisonBar.SetActive(false);

        _dnd = GetComponent<DragAndDrop>();
        _audioSource = GetComponent<AudioSource>();
        _playerMovement = GetComponent<PlayerMovement>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _counterController = FindObjectOfType<PlayerCounterController>();
        StartCoroutine(WaitSomeTime());
        InvokeRepeating(nameof(TryToInfectMyself), 10f, 10f);
    }
    public void Infect()
    {
        PoisonBar.SetActive(true);
        Infected = true;
        ThisPlayerType = PlayerType.Infected;

        _counterController.IncreasePlayers(PlayerType.Infected);
        _counterController.DecreasePlayers(PlayerType.Normal);

        _spriteRenderer.color = Color.red;
        _lastCoroutine = StartCoroutine(Poison());
    }

    private IEnumerator Poison()
    {
        while (_poisonedValue < 1)
        {
            _poisonedValue += 0.001f;
            PoisonImageToChange.fillAmount = _poisonedValue;
            yield return new WaitForSeconds(0.01f);
        }

        _audioSource.Play();
        yield return new WaitForSeconds(0.1f);
        Die();
    }

    public void Heal()
    {
        if (Infected)
        {
            Infected = false;
            StopCoroutine(_lastCoroutine);
            PoisonBar.SetActive(false);
            ThisPlayerType = PlayerType.Normal;
            _spriteRenderer.color = Color.white;
            _poisonedValue = 0;

            _counterController.DecreasePlayers(PlayerType.Infected);
            _counterController.IncreasePlayers(PlayerType.Normal);
        }  
    }

    public void Die()
    {
        Instantiate(ObjectsContainer.instance.DeadPlayerPrefab, transform.position, Quaternion.identity);

        _counterController.IncreasePlayers(PlayerType.Dead);
        _counterController.DecreasePlayers(ThisPlayerType);

        ThisPlayerType = PlayerType.Dead;
        Destroy(gameObject);
    }

    private void TryToInfectMyself()
    {
        var selfInfectChance = Random.Range(0f, 1f);
        if (selfInfectChance > 0.8f && ThisPlayerType == PlayerType.Normal)
        {
            Infect();
        }
    }

    public void Imunitet()
    {
        StartCoroutine(GetImunitet());
    }

    private IEnumerator GetImunitet()
    {
        CanInfect = false;
        Maska.SetActive(true);
        yield return new WaitForSeconds(10);
        CanInfect = true;
        Maska.SetActive(false);
    }

    private IEnumerator WaitSomeTime()
    {
        yield return new WaitForSeconds(5f);
        CanMakeSons = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        _playerMovement.ChangeDirection(true);

        if (collision.gameObject.CompareTag("Player"))
        {
            if (CanMakeSons && collision.gameObject.GetComponent<PlayerInteractions>().CanMakeSons)
            {
                var createSonChance = Random.Range(0f, 1f);
                if (createSonChance > 0.9f && ((_counterController.PlayersNormalCount + _counterController.PlayersInfectedCount) < _counterController.MaxPlayersCount))
                {
                    var newPlayer = Instantiate(ObjectsContainer.instance.PlayerPrefab, transform.position, Quaternion.identity);
                    
                    if (ThisPlayerType == PlayerType.Infected || collision.gameObject.GetComponent<PlayerInteractions>().ThisPlayerType == PlayerType.Infected)
                    {
                        _counterController.IncreasePlayers(PlayerType.Normal);
                        newPlayer.GetComponent<PlayerInteractions>().Infect();
                    }
                    else
                    {
                        _counterController.IncreasePlayers(PlayerType.Normal);
                    }
                    
                    CanMakeSons = false;
                    StartCoroutine(WaitSomeTime());
                }
            }

            if (Infected && CanInfect && !_dnd.IsDragging)
            {
                var infectChance = Random.Range(0f, 1f);
                var collPI = collision.gameObject.GetComponent<PlayerInteractions>();
                if (infectChance > 0.8f && collPI.ThisPlayerType == PlayerType.Normal)
                {
                    collPI.Infect();
                }
            }
        }
    }
}
