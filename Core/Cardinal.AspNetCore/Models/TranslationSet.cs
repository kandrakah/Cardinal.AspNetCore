namespace Cardinal.AspNetCore.Models
{
    public class TranslationSet
    {
        public string Key { get; }

        public object Value { get; }

        public TranslationSet()
        {

        }

        public TranslationSet(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }

        public static TranslationSet Set(string key, object value)
        {
            return new TranslationSet(key, value);
        }

        public override string ToString()
        {
            return $"[KEY:{this.Key}][VALUE:{this.Value}]";
        }
    }
}
