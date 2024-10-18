using UnityEngine;

public class TrailPainter3D : MonoBehaviour
{
    public GameObject player;
    public Renderer floorRenderer;
    public int textureWidth = 512;
    public int textureHeight = 512;
    public int brushSize = 5; 
    public Color brushColor = Color.red;

    private Texture2D drawTexture;
    private Vector2 uvPos;

    void Start()
    {
        drawTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);

        for (int x = 0; x < drawTexture.width; x++)
        {
            for (int y = 0; y < drawTexture.height; y++)
            {
                drawTexture.SetPixel(x, y, Color.white);
            }
        }
        drawTexture.Apply();

        floorRenderer.material.mainTexture = drawTexture;
    }

    void Update()
    {
        PaintTrail();
    }

    void PaintTrail()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, Vector3.down, out hit))
        {
            if (hit.collider.gameObject == floorRenderer.gameObject)
            {
                uvPos = hit.textureCoord;

                int pixelX = (int)(uvPos.x * drawTexture.width);
                int pixelY = (int)(uvPos.y * drawTexture.height);

                DrawOnTexture(pixelX, pixelY);
            }
        }
    }

    void DrawOnTexture(int x, int y)
    {
        for (int i = -brushSize; i <= brushSize; i++)
        {
            for (int j = -brushSize; j <= brushSize; j++)
            {
                int pixelX = x + i;
                int pixelY = y + j;

                if (pixelX >= 0 && pixelX < drawTexture.width && pixelY >= 0 && pixelY < drawTexture.height)
                {
                    if (i * i + j * j <= brushSize * brushSize)
                    {
                        drawTexture.SetPixel(pixelX, pixelY, brushColor);
                    }
                }
            }
        }
        drawTexture.Apply();
    }
}
