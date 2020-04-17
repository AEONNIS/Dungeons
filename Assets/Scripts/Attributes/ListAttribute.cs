using System;
using UnityEngine;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ListAttribute : PropertyAttribute
    {
        private int _fieldWidth;
        private int _collumnsNumber;

        public int FieldWidth => _fieldWidth;
        public int CollumnsNumber => _collumnsNumber;

        public ListAttribute(int fieldWidth, int collumnsNumbers)
        {
            _fieldWidth = fieldWidth;
            _collumnsNumber = collumnsNumbers;
        }
    }
}
