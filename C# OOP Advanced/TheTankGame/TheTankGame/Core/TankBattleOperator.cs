namespace TheTankGame.Core
{
    using System;

    using Contracts;
    using Entities.Vehicles.Contracts;

    public class TankBattleOperator : IBattleOperator
    {
        public string Battle(IVehicle attacker, IVehicle target)
        {
            double attackerWeight = attacker.Weight;
            long attackerAttack = attacker.Attack;
            long attackerDefense = attacker.Defense;
            long attackerHitPoints = attacker.HitPoints;

            double targetWeight = target.Weight;
            long targetAttack = target.Attack;
            long targetDefense = target.Defense;
            long targetHitPoints = target.HitPoints;

            bool isAttackerTurn = true;
            bool isSomeoneDead = this.IsDead(attackerHitPoints) || this.IsDead(targetHitPoints);

            while (!isSomeoneDead)
            {
                if (isAttackerTurn)
                {
                    targetHitPoints -= (long)Math.Max(0, Math.Ceiling(attackerAttack - (targetDefense + (targetWeight / 2))));
                    isAttackerTurn = false;
                }
                else
                {
                    attackerHitPoints -= (long)Math.Max(0, Math.Ceiling(targetAttack - (attackerDefense + (attackerWeight / 2))));
                    isAttackerTurn = true;
                }

                isSomeoneDead = this.IsDead(attackerHitPoints) || this.IsDead(targetHitPoints);
            }

            string result = this.IsDead(attackerHitPoints) ?
                target.Model :
                attacker.Model; 

            return result;
        }

        private bool IsDead(long hitPoints)
        {
            return hitPoints <= 0;
        }
    }
}