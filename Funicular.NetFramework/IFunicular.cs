#define CONTRACTS_FULL
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funicular
{
	public interface IFunicular
	{
		IList<FunicularState> FunicularStates { get; }
		int Capacity { get; }
		int NumberOfPassangers { get; }
		void OpenTheDoor();
		void CloseTheDoor();
		void MoveUpwards();
		void MoveDownstairs();
		void ActivateEmergency();
		void DeactivationEmergency();
		void BoardPassengers(int n);
		void StopMoving();
	}
}
