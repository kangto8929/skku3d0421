using UnityEngine;
using UnityEngine.SceneManagement;


public enum EGameState
{
    Ready,
    Run,
    Pause,
    Over
}


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private EGameState _gameState = EGameState.Run;
    public EGameState GameState => _gameState;




    private void Awake()
    {
        // ���� ������Ʈ�� ������ ��� ���� ������Ʈ�� ������ ������
        // ����ƽ ������ ���� �־ ������ ����� ��찡 �ִ�.
        // �̷� ��쿡�� ���� ������Ʈ�� �������� �ʵ���
        if (_instance != null)
        {
            //Destroy(this.gameObject);
        }
        //DontDestroyOnLoad(this.gameObject); // -> ���� �ٲ� '�� ���� ������Ʈ�� �������� �ʰڴ�'��� �ǹ�




        _instance = this;
    }

    public void Pause()
    {
        _gameState = EGameState.Pause;
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;

        PopupManager.Instance.Open(EPopupType.UI_OptionPopup, closeCallback: Continue);
    }

    public void Continue()
    {
        _gameState = EGameState.Run;
        Time.timeScale = 1;

       // Cursor.lockState = CursorLockMode.Locked;
    }

    public void TimePause()
    {
        _gameState = EGameState.Pause;
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
    }


    public void Restart()
    {
        _gameState = EGameState.Run;
        Time.timeScale = 1;

       // Cursor.lockState = CursorLockMode.Locked;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);


        // �ٽý����� �ߴ��� ������ �������� ��찡 �ִ�...
        // �̱��� ó���� �߸�������� ��������.
    }

}