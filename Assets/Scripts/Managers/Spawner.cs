using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Piece[] pieces;

    public Piece Spawn()
    {
        return SpawnAtPosition(transform.position);
    }

    public Piece SpawnAtPosition(Vector3 position)
    {
        int index = (int) Random.Range(0, pieces.Length);
        Piece piece = Instantiate(pieces[index], position, Quaternion.identity) as Piece;
        return piece;
    }
}
