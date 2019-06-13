using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CompletePlayerController : MonoBehaviour
{

    public float speed;
    public GameObject playerBulletGO;
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;
    public GameObject deathEffect;
    public GameObject impactEffect;
    public int damage = 3;
    public int currentHealth = 9;
    private bool isHit = false;

    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 1f, 0f, 0.1f);
    private float flashTimer = .05f; // number of enemies you want to spawn



    void Start()
    {

    }

    void Update()
    {


        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet01 = (GameObject)Instantiate(playerBulletGO);
            bullet01.transform.position = bulletPosition01.transform.position;

            GameObject bullet02 = (GameObject)Instantiate(playerBulletGO);
            bullet02.transform.position = bulletPosition02.transform.position;
        }
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
        if (currentHealth <= 0)
        {
            DestoryPlayer();
        }
    }

    public void DestoryPlayer()
    {
        ScoreManager.Instance.ResetSceneUI();
        Explosion();
        Destroy(gameObject);
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f; //Half player width
        max.x = max.x + 0.225f;

        max.y = max.y - 0.225f; //Half player width
        max.y = max.y + 0.225f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }

    private void Explosion()
    {
        GameObject ImpactEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        ImpactEffect.GetComponent<ParticleSystem>().Play();
    }

    IEnumerator StartFlash()
    {
        {
            Debug.Log("itsinn");
            damageImage.color = flashColour;
            yield return new WaitForSeconds(flashTimer);
            Debug.Log("ItsnotIN");
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        yield break;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (((col.gameObject.tag == "EnemyShipTag") || (col.gameObject.tag == "EnemyBulletTag") || (col.gameObject.tag == "BossBulletTag")) && (!isHit))
        {
            StartCoroutine(StartFlash());
            isHit = true;
            currentHealth -= damage;
            healthSlider.value = currentHealth;
            isHit = false;
        }
    }
}