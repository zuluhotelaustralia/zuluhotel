using System;
using System.Collections.Generic;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Gumps;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Items
{
    public class Bandage : Item
    {
        public static int Range = 1;

        public static int AmountToHeal = 1;
        
        public static int AmountToCure = 2;
        
        public static int AmountToRes = 5;

        public override double DefaultWeight => 0.1;


        [Constructible]
        public Bandage() : this(1)
        {
        }


        [Constructible]
        public Bandage(int amount) : base(0xE21)
        {
            Stackable = true;
            Amount = amount;
        }

        [Constructible]
        public Bandage(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(GetWorldLocation(), Range))
            {
                from.RevealingAction();

                from.SendSuccessMessage(500948); // Who will you use the bandages on?

                from.Target = new InternalTarget(this);
            }
            else
            {
                from.SendFailureMessage(500295); // You are too far away to do that.
            }
        }

        private class InternalTarget : Target
        {
            private readonly Bandage m_Bandage;

            public InternalTarget(Bandage bandage) : base(Bandage.Range, false, TargetFlags.Beneficial)
            {
                m_Bandage = bandage;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Bandage.Deleted)
                    return;

                if (targeted is Mobile targetedMobile)
                {
                    var amountToConsume = AmountToHeal;
                    if (!targetedMobile.Alive)
                        amountToConsume = AmountToRes;
                    else if (targetedMobile.Poisoned)
                        amountToConsume = AmountToCure;

                    if (m_Bandage.Amount < amountToConsume)
                    {
                        from.SendFailureMessage("You don't have enough bandages.");
                        return;
                    }
                    
                    if (from.InRange(m_Bandage.GetWorldLocation(), Bandage.Range))
                    {
                        if (BandageContext.BeginHeal(from, targetedMobile) != null)
                            m_Bandage.Consume(amountToConsume);
                    }
                    else
                        from.SendFailureMessage(500295); // You are too far away to do that.
                }
                else
                    from.SendFailureMessage(500970); // Bandages can not be used on that.
            }
        }
    }

    public class BandageContext
    {
        public static int ResDifficulty = 105;

        public static int PointsMultiplier = 15;
        public Mobile Healer { get; }

        public Mobile Patient { get; }

        public int Slips { get; set; }

        public Timer Timer { get; private set; }

        public void Slip()
        {
            Healer.SendFailureMessage(500961); // Your fingers slip!
            ++Slips;
        }

        public BandageContext(Mobile healer, Mobile patient, TimeSpan delay)
        {
            Healer = healer;
            Patient = patient;

            Timer = new InternalTimer(this, delay);
            Timer.Start();
        }

        public void StopHeal()
        {
            m_Table.Remove(Healer);

            if (Timer != null)
                Timer.Stop();

            Timer = null;
        }

        private static readonly Dictionary<Mobile, BandageContext> m_Table = new();

        public static BandageContext GetContext(Mobile healer)
        {
            m_Table.TryGetValue(healer, out var bc);
            return bc;
        }

        public static SkillName GetPrimarySkill(Mobile m)
        {
            if (!m.Player && (m.Body.IsMonster || m.Body.IsAnimal))
                return SkillName.Veterinary;
            return SkillName.Healing;
        }

        public static SkillName GetSecondarySkill(Mobile m)
        {
            if (!m.Player && (m.Body.IsMonster || m.Body.IsAnimal))
                return SkillName.AnimalLore;
            return SkillName.Anatomy;
        }

        public void EndHeal()
        {
            StopHeal();

            int healerNumber = -1, patientNumber = -1;
            var playSound = true;

            var primarySkill = GetPrimarySkill(Patient);
            var secondarySkill = GetSecondarySkill(Patient);

            if (!Healer.Alive)
            {
                healerNumber = 500962; // You were unable to finish your work before you died.
                patientNumber = -1;
                playSound = false;
            }
            else if (!Healer.InRange(Patient, Bandage.Range))
            {
                healerNumber = 500963; // You did not stay close enough to heal your target.
                patientNumber = -1;
                playSound = false;
            }
            else if (!Patient.Alive)
            {
                var healing = Healer.Skills[primarySkill].Value;
                var anatomy = Healer.Skills[secondarySkill].Value;
                var difficulty = ResDifficulty;
                var points = difficulty * PointsMultiplier;

                if (healing >= 90.0 && anatomy >= 90.0 && Healer.ShilCheckSkill(primarySkill, difficulty, points))
                {
                    if (Patient.Map == null || !Patient.Map.CanFit(Patient.Location, 16, false, false))
                    {
                        healerNumber = 501042; // Target can not be resurrected at that location.
                        patientNumber = 502391; // Thou can not be resurrected there!
                    }
                    else
                    {
                        healerNumber = 500965; // You are able to resurrect your patient.
                        patientNumber = -1;

                        Patient.PlaySound(0x214);
                        Patient.FixedEffect(0x376A, 10, 16);

                        Patient.CloseGump<ResurrectGump>();
                        Patient.SendGump(new ResurrectGump(Patient, Healer));
                    }
                }
                else
                {
                    healerNumber = 500966; // You are unable to resurrect your patient.
                    patientNumber = -1;
                }
            }
            else if (Patient.Poisoned)
            {
                Healer.SendSuccessMessage(500969); // You finish applying the bandages.

                var poisonLevel = Patient.Poison.Level;
                var difficulty = poisonLevel * 20 + 15;
                var points = difficulty * PointsMultiplier;

                if (Healer.ShilCheckSkill(primarySkill, difficulty, points))
                {
                    if (Patient.CurePoison(Healer))
                    {
                        healerNumber = Healer == Patient ? -1 : 1010058; // You have cured the target of all poisons.
                        patientNumber = 1010059; // You have been cured of all poisons.
                    }
                    else
                    {
                        healerNumber = -1;
                        patientNumber = -1;
                    }
                }
                else
                {
                    healerNumber = 1010060; // You have failed to cure your target!
                    patientNumber = -1;
                }
            }
            else if (Patient.Hits == Patient.HitsMax)
            {
                healerNumber = 500967; // You heal what little damage your patient had.
                patientNumber = -1;
            }
            else
            {
                patientNumber = -1;
                
                var difficulty = Patient.HitsMax - Patient.Hits;
                difficulty = Math.Min(130, difficulty);

                var points = difficulty * PointsMultiplier;

                if (Healer.ShilCheckSkill(primarySkill, difficulty, points))
                {
                    healerNumber = 500969; // You finish applying the bandages.

                    var primary = Healer.Skills[primarySkill].Value / 10;
                    var secondary = Healer.Skills[secondarySkill].Value / 5;
                    var toHeal = new DiceRoll($"{primary}d4+{primary}").Roll() + new DiceRoll("1d8+2").Roll() + secondary;

                    Healer.FireHook(h => h.OnHeal(Healer, Patient, this, ref toHeal));

                    var count = 10;
                    count -= Slips;
                    count = Math.Max(count, 1);

                    toHeal = toHeal * count / 10;

                    if (toHeal < 1)
                    {
                        toHeal = 1;
                        healerNumber = 500968; // You apply the bandages, but they barely help.
                    }

                    Patient.Heal((int) toHeal, Healer, false);
                }
                else
                {
                    healerNumber = 500968; // You apply the bandages, but they barely help.
                    playSound = false;
                }
            }

            if (healerNumber != -1)
                Healer.SendSuccessMessage(healerNumber);

            if (patientNumber != -1)
                Patient.SendSuccessMessage(patientNumber);

            if (playSound)
                Patient.PlaySound(0x57);
        }

        private class InternalTimer : Timer
        {
            private readonly BandageContext m_Context;

            public InternalTimer(BandageContext context, TimeSpan delay) : base(delay)
            {
                m_Context = context;
            }

            protected override void OnTick()
            {
                m_Context.EndHeal();
            }
        }

        public static BandageContext BeginHeal(Mobile healer, Mobile patient)
        {
            var context = GetContext(healer);
            
            if (!patient.Poisoned && patient.Hits == patient.HitsMax)
            {
                healer.SendFailureMessage(500955); // That being is not damaged!
            }
            else if (!patient.Alive && (patient.Map == null || !patient.Map.CanFit(patient.Location, 16, false, false)))
            {
                healer.SendFailureMessage(501042); // Target cannot be resurrected at that location.
            }
            else if (context?.Timer?.Running != null && context.Timer.Running)
            {
                healer.SendFailureMessage("You are still busy healing.");
            }
            else if (healer.CanBeBeneficial(patient, true, true))
            {
                healer.DoBeneficial(patient);

                var onSelf = healer == patient;
                
                var healDelay = patient.Alive ? 5.0 : 10.0;

                context?.StopHeal();

                context = new BandageContext(healer, patient, TimeSpan.FromSeconds(healDelay));

                m_Table[healer] = context;

                if (!onSelf)
                    patient.SendLocalizedMessage(1008078, false, healer.Name); //  : Attempting to heal you.

                healer.SendSuccessMessage(500956); // You begin applying the bandages.
                return context;
            }

            return null;
        }
    }
}