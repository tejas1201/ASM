namespace NHT.ASM.Models.Enums
{
    public class ParameterInputTypeEnum : SmartEnum<ParameterInputTypeEnum>
    {
        public static ParameterInputTypeEnum Input = new ParameterInputTypeEnum("input", 1);
        public static ParameterInputTypeEnum Textarea = new ParameterInputTypeEnum("textarea", 2);
        public static ParameterInputTypeEnum Select = new ParameterInputTypeEnum("select", 3);
        public static ParameterInputTypeEnum Checkbox = new ParameterInputTypeEnum("checkbox", 4);
        
        protected ParameterInputTypeEnum(string value, int id):base(value, id)
        {
        }
    }
}
