using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship;

public class DuplicateMatchdayException : Exception
{
    public DuplicateMatchdayException(string message) : base(message)
    {
    }
}
