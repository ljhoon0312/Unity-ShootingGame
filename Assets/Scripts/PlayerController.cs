using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Image cooldownImage;
    public float specialCooldown = 10f;
    private float currentCooldown = 0f;
    private bool canUseSpecial = true;

    public float moveSpeed = 5f;
    private PlayerAttack attack;

    public AudioClip specialSkillSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        attack = GetComponent<PlayerAttack>();
        if (attack == null)
            Debug.LogError("PlayerAttack 컴포넌트를 찾을 수 없습니다.");
        if (cooldownImage == null)
        {
            // Canvas 안에 있는 "CooldownImage"라는 이름의 UI Image를 찾아 자동 연결
            cooldownImage = GameObject.Find("CooldownImage")?.GetComponent<Image>();
        }
        if (cooldownImage != null)
        {
            cooldownImage.enabled = true;
            cooldownImage.gameObject.SetActive(true);
            cooldownImage.fillAmount = 1f;
        }
    }

    void Update()
    {
        Move();
        if (!canUseSpecial)
        {
            currentCooldown -= Time.deltaTime;
            //float ratio = currentCooldown / specialCooldown;
            //cooldownImage.fillAmount = ratio;
            cooldownImage.fillAmount = 1f - (currentCooldown / specialCooldown);

            if (currentCooldown <= 0f)
            {
                canUseSpecial = true;
                cooldownImage.fillAmount = 1f;
            }
        }
            if (Input.GetKeyDown(KeyCode.Space))
            attack?.Fire();

        if(Input.GetKeyDown(KeyCode.LeftShift) && canUseSpecial)
            UseSpecialSkill();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, v, 0).normalized;

        transform.position += dir * moveSpeed * Time.deltaTime;

        // 화면 경계 제한
        ClampToScreen();
    }

    void ClampToScreen()
    {
        Vector3 pos = transform.position;
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        float x = Mathf.Clamp(pos.x, min.x, max.x);
        float y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = new Vector3(x, y, pos.z);
    }

    void UseSpecialSkill()
    {
        if (!canUseSpecial) return;
        // 사운드 재생
        if (specialSkillSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(specialSkillSound);
        }
        Debug.Log("필살기 발동!");
        canUseSpecial = false;
        currentCooldown = specialCooldown;
        cooldownImage.fillAmount = 1f;
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("EnemyBullet"))
        {
            Destroy(bullet);
        }
    }
}
