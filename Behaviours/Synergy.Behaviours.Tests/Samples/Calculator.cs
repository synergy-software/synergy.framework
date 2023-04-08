namespace Synergy.Behaviours.Tests.Samples;

public class Calculator
{
    public int FirstNumber { get; set; }

    public int SecondNumber { get; set; }

    public int Add()
    {
        return this.FirstNumber + this.SecondNumber;
    }

    public int Subtract()
    {
        return this.FirstNumber - this.SecondNumber;
    }

    public int Divide()
    {
        if (this.FirstNumber == 0 || this.SecondNumber == 0)
        {
            return 0;
        }
        else
        {
            return this.FirstNumber / this.SecondNumber;
        }
    }

    public int Multiply()
    {
        return this.FirstNumber * this.SecondNumber;
    }
}