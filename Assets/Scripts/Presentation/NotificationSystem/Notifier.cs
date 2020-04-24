using System.Collections.Generic;
using UnityEngine;

namespace Dungeons.Presentation.NotificationSystem
{
    public class Notifier : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;
        [SerializeField] private Message _messageTemplate;
        [SerializeField] private int _minMessagesInPool;

        private readonly Queue<Message> _messagePool = new Queue<Message>();

        #region Unity
        private void Awake()
        {
            AddMessagesToPool(_minMessagesInPool);
        }
        #endregion

        public void ShowMessage(string text, Sprite icon = null, float displayTime = 0)
        {
            if (_messagePool.Count == 0)
                AddMessagesToPool(_minMessagesInPool);

            Message message = _messagePool.Dequeue();
            message.transform.SetAsFirstSibling();
            message.Display(text, icon, displayTime);
        }

        public void ReturnToPool(Message message)
        {
            _messagePool.Enqueue(message);
        }

        private void AddMessagesToPool(int number)
        {
            for (int i = 0; i < number; i++)
            {
                Message message = Instantiate(_messageTemplate, _content);
                message.Init(this);
                _messagePool.Enqueue(message);
            }
        }
    }
}
