namespace Models
{
    public enum Weight
    {
        克,
        千克
    }

    public class WeightConverter
    {
        public static Weight Parse(string weightStr)
        {
            weightStr = weightStr.Trim().ToLower();
            if (weightStr == "克" || weightStr == "g" || weightStr == "gram")
                return Weight.克;
            if (weightStr == "千克" || weightStr == "公斤" || weightStr == "kg" || weightStr == "kilogram")
                return Weight.千克;

            throw new System.Exception($"错误的重量单位字符串:{weightStr}");
        }

        public static double Convert(Weight measure, double amount, Weight toMeasure)
        {
            switch (measure)
            {
                case Weight.克:
                    switch (toMeasure)
                    {
                        case Weight.克:
                            return amount;
                        case Weight.千克:
                            return amount / 1000;
                        default:
                            throw new System.Exception("错误的weight类型");
                    }
                case Weight.千克:
                    switch (toMeasure)
                    {
                        case Weight.克:
                            return amount * 1000;
                        case Weight.千克:
                            return amount;
                        default:
                            throw new System.Exception("错误的weight类型");
                    }
                default:
                    throw new System.Exception("错误的weight类型");
            }
        }

        public static double Convert(string measure, double amount, string toMeasure)
        {
            Weight m1 = Parse(measure);
            Weight m2 = Parse(toMeasure);
            return Convert(m1, amount, m2);
        }
    }
}
