using UnityEngine;

public class InfiniteGround : MonoBehaviour
{
    public Transform player;
    public Transform[] chunks;
    public float chunkLength = 100f;

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("InfiniteGround: player is not assigned.");
            return;
        }

        if (chunks == null || chunks.Length == 0)
        {
            Debug.LogWarning("InfiniteGround: chunks array is empty.");
            return;
        }

        for (int i = 0; i < chunks.Length; i++)
        {
            if (chunks[i] == null)
            {
                Debug.LogWarning("InfiniteGround: chunks[" + i + "] is not assigned.");
                return;
            }
        }

        Transform firstChunk = chunks[0];

        if (player.position.z > firstChunk.position.z + chunkLength / 2f)
        {
            Transform lastChunk = chunks[chunks.Length - 1];

            firstChunk.position = new Vector3(
                firstChunk.position.x,
                firstChunk.position.y,
                lastChunk.position.z + chunkLength
            );

            for (int i = 0; i < chunks.Length - 1; i++)
            {
                chunks[i] = chunks[i + 1];
            }

            chunks[chunks.Length - 1] = firstChunk;
        }
    }
}