using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace NHT.ASM.Models.Enums
{
    public abstract class SmartEnum<TEnum> : IEquatable<SmartEnum<TEnum>> where TEnum : SmartEnum<TEnum>
    {
        private static readonly Lazy<List<TEnum>> LazyList = new Lazy<List<TEnum>>(ListAllOptions);

        private static List<TEnum> ListAllOptions()
        {
            Type t = typeof(TEnum);
            return t.GetFields(BindingFlags.Static | BindingFlags.Public).Where(p => t.IsAssignableFrom(p.FieldType)).Select(pi => (TEnum)pi.GetValue(null)).OrderBy(p => p.Value).ToList();
        }

        public static List<TEnum> List => LazyList.Value;

        public string Value { get; }

        public int Id { get; protected set; }

        protected SmartEnum(string value, int id)
        {
            Value = value;
            Id = id;
        }

        protected SmartEnum()
        {
        }

        //We cannot use this without the nuget package because of Guard
        //public static TEnum FromName(string name)
        //{
        //    Guard.Against.NullOrEmpty(name, nameof(name));
        //    TEnum @enum = List.FirstOrDefault(item => string.Equals(item.Value, name, StringComparison.OrdinalIgnoreCase));
        //    if (@enum == null)
        //        throw new SmartEnumNotFoundException($"No {typeof(TEnum).Name} with Name {name} found.");
        //    return @enum;
        //}

        public static TEnum FromValue(int value)
        {
            TEnum @enum = List.FirstOrDefault(item => EqualityComparer<int>.Default.Equals(item.Id, value));
            if (!(@enum == null))
                return @enum;
            throw new SmartEnumNotFoundException($"No {typeof(TEnum).Name} with Value {value} found.");
        }

        public static TEnum FromValue(int value, TEnum defaultValue)
        {
            TEnum @enum = List.FirstOrDefault(item => EqualityComparer<int>.Default.Equals(item.Id, value));
            if (@enum == null)
                @enum = defaultValue;
            return @enum;
        }

        public override string ToString()
        {
            return $"{Value} ({Id})";
        }

        public override int GetHashCode()
        {
            return new { Value, Id }.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SmartEnum<TEnum>);
        }

        public bool Equals(SmartEnum<TEnum> other)
        {
            if (other == null)
                return false;
            if (this == other)
                return true;
            if (GetType() != other.GetType() || Value != other.Value)
                return false;
            return EqualityComparer<int>.Default.Equals(Id, other.Id);
        }

        public static bool operator ==(SmartEnum<TEnum> left, SmartEnum<TEnum> right)
        {
            if ((object)left != null)
                return left.Equals(right);
            return (object)right == null;
        }

        public static bool operator !=(SmartEnum<TEnum> left, SmartEnum<TEnum> right)
        {
            return !(left == right);
        }

        public static implicit operator int(SmartEnum<TEnum> smartEnum)
        {
            return smartEnum.Id;
        }

        public static explicit operator SmartEnum<TEnum>(int value)
        {
            return FromValue(value);
        }
    }

    [Serializable]
    internal class SmartEnumNotFoundException : Exception
    {
        public SmartEnumNotFoundException()
        {
        }

        public SmartEnumNotFoundException(string message) : base(message)
        {
        }

        public SmartEnumNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SmartEnumNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}