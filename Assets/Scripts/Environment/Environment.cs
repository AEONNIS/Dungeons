using Game.Model;
using UnityEngine;

namespace Game
{
    public class Environment : MonoBehaviour
    {
        //[SerializeField] private EnvironmentBase _environment;
        //[SerializeField] private float _strength;
        //[SerializeField] private SpriteRenderer _spriteRenderer;
        //[SerializeField] private Rigidbody2D _rigidbody2d;

        //private ColorHighlightingEnvironment _selfHighlighting; //...
        //private ItemDisplay _inHandsItemDisplay; //...
        //private Inventory _inventory; //...
        //private float _damageSize = 0; //...

        //private void Awake()
        //{
        //    _strength = _environment.MaxStrength;

        //    _selfHighlighting = GetComponent<ColorHighlightingEnvironment>(); //...
        //    _inventory = GameObject.Find("Player").GetComponent<Inventory>(); //...
        //}

        //public bool TryToDamage(float damageSize)
        //{

        //}

        //private void ToDamage(float damageSize)
        //{

        //}

        //public void EnvironmentDamage()
        //{
        //    if (_strength <= 0)
        //    {
        //        MessageManager.ShowMessageWithImage(_environment.nameMaterial + " уже разрушен", _environment.spriteDestroyed);
        //        return;
        //    }

        //    if (_environment.id == 0) // Воздействие на неразрушаемый тайл "Черный камень"
        //    {
        //        MessageManager.ShowMessageWithImage(_environment.nameMaterial + " не возможно разрушить", _environment.spriteDefault);
        //        return;
        //    }
        //    else
        //    {
        //        _inHandsItemDisplay = _inventory.GetItemDInSlot(-1);

        //        if (_inHandsItemDisplay.item.id == 0) // Если в руках игрока ничего нет...
        //        {
        //            MessageManager.ShowMessageOnlyText("Для разрушения тайла необходим соответствующий предмет в руке"); // !! Добавить подсказку о том, какой Item эффективнее всего разрушает именно этот тайл.
        //            return;
        //        }

        //        _damageSize = _environment.DamageSize(_inHandsItemDisplay.item.id);
        //        _inHandsItemDisplay.ItemDamage(this);
        //        if (_damageSize > 0) // Если тайл получил реальный урон...
        //        {
        //            _strength -= _damageSize;
        //            _selfHighlighting.HighlightingDamage();

        //            SetStateEnvironment();
        //        }
        //    }
        //}

        //private void SetStateEnvironment()
        //{
        //    if (100 <= _strength)
        //    {
        //        _thisGamObj.SetActive(true);
        //        _thisGamObj.layer = 8; // Ground
        //        _spriteRend.sprite = _environment.spriteDefault;
        //        _rigidbody.bodyType = RigidbodyType2D.Static;
        //    }
        //    else if (30 < _strength && _strength < 100)
        //    {
        //        _thisGamObj.SetActive(true);
        //        _thisGamObj.layer = 8; // Ground
        //        _spriteRend.sprite = _environment.spriteDamaged;
        //        _rigidbody.bodyType = RigidbodyType2D.Static;
        //    }
        //    else if (0 < _strength && _strength <= 30)
        //    {
        //        _thisGamObj.SetActive(true);
        //        _thisGamObj.layer = 8; // Ground
        //        _spriteRend.sprite = _environment.spriteDamaged;
        //        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        //    }
        //    else if (_strength <= 0)
        //    {
        //        if (_damageSize > 0)
        //        {
        //            if (_environment.id == 5) // Если дерево, то поварачиваем, когда разрушено !! КОСТЫЛЬ!
        //                _rigidbody.AddTorque(-2, ForceMode2D.Impulse); // Крутящий момент сделать в зависимости от стороны, с которой стоит игрок (отлетает в сторону от игрока).

        //            _thisGamObj.SetActive(true);
        //            Invoke("Shutdown", shutdownTime);
        //        }
        //        else
        //            _thisGamObj.SetActive(false);

        //        _thisGamObj.layer = 11; // Ground destroyed
        //        _spriteRend.sprite = _environment.spriteDestroyed;
        //        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        //    }
        //}

        //private void Shutdown()
        //{
        //    _damageSize = 0;
        //    _thisGamObj.SetActive(false);
        //}
    }
}
