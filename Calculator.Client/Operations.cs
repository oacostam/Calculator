using System.ComponentModel;

namespace Calculator.Client
{
    public enum Operations
    {
        [Description("Addition")]
        Addition = 1,
        [Description("Subtraction")]
        Subtraction,
        [Description("Multiplication")]
        Multiplication,
        [Description("Division")]
        Division,
        [Description("Square Root")]
        SquareRoot,
        [Description("Recover exiting Op")]
        RecoverOp
    }
}
