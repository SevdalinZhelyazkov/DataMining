namespace kNN
{
    public class Instance
    {
        public Instance(double sepalLength, double sepalWidth, double petalLength, double petalWidth, string actualClass) 
        {
            this.SepalLength = sepalLength;
            this.SepalWidth = sepalWidth;
            this.PetalLength = petalLength;
            this.PetalWidth = petalWidth;
            this.ActualClass = actualClass;
        }

        public double SepalLength { get; private set; }

        public double SepalWidth { get; private set; }

        public double PetalLength { get; private set; }

        public double PetalWidth { get; private set; }

        public string ActualClass { get; private set; }

        public string PredictedClass { get; set; }
    }
}
