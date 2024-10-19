using UnityEngine;
using UnityEngine.UI; 

public class SpriteAnimationController : MonoBehaviour
{
    public Sprite[] animationSprites; 
    public float animationSpeed = 0.1f; 
    public bool loop = true;           
    public Image targetImage;  
    public SpriteRenderer spriteRenderer;

    private int currentFrame = 0;
    private float timer = 0f;

    private void Start()
    {
        if (animationSprites.Length > 0)
        {
            UpdateSprite();
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= animationSpeed)
        {
            timer = 0f;
            currentFrame++;

            if (currentFrame >= animationSprites.Length)
            {
                if (loop)
                {
                    currentFrame = 0;
                }
                else
                {
                    currentFrame = animationSprites.Length - 1;
                }
            }

            UpdateSprite();
        }
    }

    private void UpdateSprite()
    {
        if (targetImage != null)
        {
            targetImage.sprite = animationSprites[currentFrame];
        }
        else if (spriteRenderer != null)
        {
            spriteRenderer.sprite = animationSprites[currentFrame];
        }
    }

    public void PlayAnimation()
    {
        currentFrame = 0;  
        timer = 0f;        
        UpdateSprite();    
    }

    public void StopAnimation()
    {
        loop = false;
    }
}
