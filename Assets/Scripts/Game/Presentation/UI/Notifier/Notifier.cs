using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class Notifier : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;
        [SerializeField] private Message _messageTemplate;
        [SerializeField] private int _minNumberMessagesInPool;

        private Queue<Message> _messagePool = new Queue<Message>();

        private void Awake()
        {
            AddMessagesInPool(_minNumberMessagesInPool);
        }

        public void ShowMessage(string messageText, Sprite messageImage = null, float displayTime = 0)
        {
            if (_messagePool.Count == 0)
                AddMessagesInPool(_minNumberMessagesInPool);

            Message message = _messagePool.Dequeue();
            message.transform.SetAsFirstSibling();
            message.Display(messageText, messageImage, displayTime);
        }

        public void ReturnMessageInPool(Message message)
        {
            _messagePool.Enqueue(message);
        }

        private void AddMessagesInPool(int number)
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
