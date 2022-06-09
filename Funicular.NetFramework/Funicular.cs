#define CONTRACTS_FULL
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funicular
{
    public class Funicular : IFunicular
    {
        public IList<FunicularState> FunicularStates { get; }

        public int Capacity { get; }

        public int NumberOfPassangers { get; internal set; }

        public Funicular(int capacity)
        {
            this.FunicularStates = new List<FunicularState>();
            FunicularStates.Add(FunicularState.DorsClosed);
            this.Capacity = capacity;
            NumberOfPassangers = 0;
        }

        [ContractInvariantMethod] // Інваріант, що кількість пасажирів невід'ємна
        private void ValidateNumberOfPassangers()
        {
            Contract.Invariant(NumberOfPassangers >= 0);
        }

        [ContractInvariantMethod] // Інваріант, що фунікулер знаходиться хоч в одному стані
        private void ValidateFunicularStates()
        {
            Contract.Invariant(FunicularStates.Count > 0);
        }

        public void ActivateEmergency()
        {
            Contract.Requires(FunicularStates.Contains(FunicularState.DorsClosed)); // передумова того що двері зачинені
            Contract.Requires(FunicularStates.Contains(FunicularState.MovingDown)   // передумова того що фунікулер у русі
                || FunicularStates.Contains(FunicularState.MovingUp));

            FunicularStates.Remove(FunicularState.MovingDown);
            FunicularStates.Remove(FunicularState.MovingUp);
            FunicularStates.Add(FunicularState.EmergencyBrakeActivated);
        }

        public void BoardPassengers(int n)
        {
            Contract.Requires(FunicularStates.Contains(FunicularState.DorsOpen)); //Передумова на те що фунікулер має відчинені двері
            Contract.Requires(NumberOfPassangers + n <= Capacity); // передумова на кількість пасажирів відповідно до місткості фунікулеру
            Contract.Requires(NumberOfPassangers + n >= 0);
            NumberOfPassangers += n;

            FunicularStates.Add(FunicularState.PassangersBoarding);
        }

        public void CloseTheDoor()
        {
            Contract.Requires(FunicularStates.Contains(FunicularState.DorsOpen)); //Передумова двері мають бути відкриті
            Contract.Requires(FunicularStates.Contains(FunicularState.MovingStoped)); //Повинна бути зупинка

            FunicularStates.Remove(FunicularState.PassangersBoarding);
            FunicularStates.Remove(FunicularState.DorsOpen);
            FunicularStates.Add(FunicularState.DorsClosed);
        }

        public void DeactivationEmergency()
        {
            Contract.Requires(FunicularStates.Contains(FunicularState.DorsClosed)); //Передумова двері мають бути зачинені
            Contract.Requires(FunicularStates.Contains(FunicularState.EmergencyBrakeActivated)); //Передумова аварійне гальмо увімкнене

            FunicularStates.Remove(FunicularState.EmergencyBrakeActivated);
            FunicularStates.Add(FunicularState.EmergencyBrakeDeactivated);
        }

        public void MoveDownstairs()
        {
            Contract.Requires(FunicularStates.Contains(FunicularState.MovingStoped) //Передумова зупинка або деактивація аварійних гальм 
                || FunicularStates.Contains(FunicularState.EmergencyBrakeDeactivated));
            Contract.Requires(FunicularStates.Contains(FunicularState.DorsClosed)); //Передумова двері мають бути зачинені

            FunicularStates.Remove(FunicularState.MovingStoped);
            FunicularStates.Remove(FunicularState.EmergencyBrakeDeactivated);
            FunicularStates.Add(FunicularState.MovingUp);
        }

        public void MoveUpwards()
        {
            Contract.Requires(FunicularStates.Contains(FunicularState.MovingStoped)   //Передумова зупинка або деактивація аварійних гальм
                || FunicularStates.Contains(FunicularState.EmergencyBrakeDeactivated));
            Contract.Requires(FunicularStates.Contains(FunicularState.DorsClosed)); //Передумова двері мають бути зачинені

            FunicularStates.Remove(FunicularState.MovingStoped);
            FunicularStates.Remove(FunicularState.EmergencyBrakeDeactivated);
            FunicularStates.Add(FunicularState.MovingUp);
        }

        public void StopMoving() {
            Contract.Requires(FunicularStates.Contains(FunicularState.MovingUp) //Передумова рух вгору чи вниз
                || FunicularStates.Contains(FunicularState.MovingDown));
            Contract.Requires(!FunicularStates.Contains(FunicularState.EmergencyBrakeActivated)); //Передумова аварійні гальма не активовані

            FunicularStates.Remove(FunicularState.MovingUp);
            FunicularStates.Remove(FunicularState.MovingDown);
        }

        public void OpenTheDoor()
        {
            Contract.Requires(FunicularStates.Contains(FunicularState.DorsClosed)); //Передумова того що двері зачинено 
            Contract.Requires(FunicularStates.Contains(FunicularState.MovingStoped)); //Передумова того що рух зупинено

            Contract.Requires(!FunicularStates.Contains(FunicularState.EmergencyBrakeActivated)); //Аварійні гальна не активовано

            FunicularStates.Remove(FunicularState.DorsClosed);
            FunicularStates.Add(FunicularState.MovingStoped);
            FunicularStates.Add(FunicularState.DorsOpen);
        }

    }
}
