#define CONTRACTS_FULL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funicular
{
	public enum FunicularState
	{
		MovingStoped,
		MovingDown,
		MovingUp,
		PassangersBoarding,
		DorsOpen,
		DorsClosed,
		EmergencyBrakeActivated,
		EmergencyBrakeDeactivated
	}
}
