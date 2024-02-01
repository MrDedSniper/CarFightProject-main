using Photon.Pun;

public class DeactivateCameraIfNotMine : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        if (!photonView.IsMine)
        {
            gameObject.SetActive(false);
        }
    }
}
