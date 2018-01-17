using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Media;
using System.Windows.Media;

namespace MapLibrary
{
    public interface ClientInterface
    {
        bool isActive();
        Color getAlarmState();
    }
}
