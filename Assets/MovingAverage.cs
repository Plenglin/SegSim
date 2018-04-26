public class MovingAverage
{
    private float sum = 0;
    private float[] buffer;
    private int index = 0;

    public float Sum {
        get {
            return sum;
        }
        private set {
            this.sum = value;
        }
    }

    public MovingAverage(int bufSize) {
        buffer = new float[bufSize];
    }

    public float Push(float val) {
        float ret = buffer[index];
        buffer[index] = val;
        sum -= ret;
        index++;
        if (index >= buffer.Length) {
            index = 0;
        }
        return ret;
    }
}
