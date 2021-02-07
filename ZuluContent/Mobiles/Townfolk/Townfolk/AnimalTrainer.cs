using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;
using static Server.Configurations.MessageHueConfiguration;

namespace Server.Mobiles
{
    public class AnimalTrainer : BaseVendor
    {
        private List<SBInfo> m_SBInfos = new List<SBInfo>();

        protected override List<SBInfo> SBInfos
        {
            get { return m_SBInfos; }
        }


        [Constructible]
        public AnimalTrainer() : base("the Animal Trainer")
        {
            SetSkill(SkillName.AnimalLore, 64.0, 100.0);
            SetSkill(SkillName.AnimalTaming, 90.0, 100.0);
            SetSkill(SkillName.Veterinary, 65.0, 88.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBAnimalTrainer());
        }

        public override VendorShoeType ShoeType
        {
            get { return Female ? VendorShoeType.ThighBoots : VendorShoeType.Boots; }
        }

        public override int GetShoeHue()
        {
            return 0;
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(Utility.RandomBool() ? (Item) new QuarterStaff() : (Item) new ShepherdsCrook());
        }

        private class StableTarget : Target
        {
            private AnimalTrainer m_Trainer;

            public StableTarget(AnimalTrainer trainer) : base(12, false, TargetFlags.None)
            {
                m_Trainer = trainer;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is BaseCreature)
                    m_Trainer.EndStable(from, (BaseCreature) targeted);
                else if (targeted == from)
                    m_Trainer.SayTo(from, 502672); // HA HA HA! Sorry, I am not an inn.
                else
                    m_Trainer.SayTo(from, 1048053); // You can't stable that!
            }
        }

        public void BeginStable(Mobile from)
        {
            if (Deleted || !from.CheckAlive())
                return;

            from.SendAsciiMessage(MessageSuccessHue, "Which pet would you like to Stable?");

            from.Target = new StableTarget(this);
        }

        public void EndStable(Mobile from, BaseCreature pet)
        {
            if (Deleted || !from.CheckAlive())
                return;

            if (pet.Body.IsHuman)
            {
                SayTo(from, 502672); // HA HA HA! Sorry, I am not an inn.
            }
            else if (!pet.Controlled)
            {
                SayTo(from, 1048053); // You can't stable that!
            }
            else if (pet.ControlMaster != from)
            {
                SayTo(from, 1042562); // You do not own that pet!
            }
            else if (pet.Summoned)
            {
                SayTo(from, 502673); // I can not stable summoned creatures.
            }
            else if ((pet is PackLlama || pet is PackHorse) && pet.Backpack != null && pet.Backpack.Items.Count > 0)
            {
                SayTo(from, 1042563); // You need to unload your pet.
            }
            else if (pet.Combatant != null && pet.InRange(pet.Combatant, 12) && pet.Map == pet.Combatant.Map)
            {
                SayTo(from, 1042564); // I'm sorry.  Your pet seems to be busy.
            }
            else
            {
                var amount = pet.Str * 2;
                Container bank = from.FindBankNoCreate();

                SayTo(from, true, $"I charge {amount} to take care of that {pet.Name}.");

                if (@from.Backpack != null && @from.Backpack.ConsumeTotal(typeof(Gold), amount) ||
                    bank != null && bank.ConsumeTotal(typeof(Gold), amount))
                {
                    pet.ControlTarget = null;
                    pet.ControlOrder = OrderType.Stay;
                    pet.Internalize();

                    pet.SetControlMaster(null);
                    pet.SummonMaster = null;

                    pet.IsStabled = true;
                    pet.StabledBy = from;

                    var petClaimTicket = new PetClaimTicket {Name = $"Pet claim ticket - Name: {pet.Name}"};
                    petClaimTicket.Stabled = pet;

                    var cont = from.Backpack;
                    if (cont == null || !cont.TryDropItem(from, petClaimTicket, false))
                        petClaimTicket.MoveToWorld(from.Location, from.Map);

                    SayTo(from, true, $"Keep this ticket and give it to me when you want {pet.Name} back.");
                }
                else
                {
                    SayTo(from, 502677); // But thou hast not the funds in thy bank account!
                }
            }
        }

        public void Claim(Mobile from, PetClaimTicket petClaimTicket)
        {
            if (Deleted || !from.CheckAlive())
                return;

            var pet = petClaimTicket.Stabled;

            if (pet == null || pet.Deleted)
            {
                pet.IsStabled = false;
                pet.StabledBy = null;
                petClaimTicket.Stabled = null;
                return;
            }

            DoClaim(from, pet, petClaimTicket);

            SayTo(from, 1042559); // Here you go... and good day to you!
        }

        private void DoClaim(Mobile from, BaseCreature pet, PetClaimTicket petClaimTicket)
        {
            pet.SetControlMaster(from);

            pet.ControlTarget = from;
            if (pet.CheckControlChance(from))
                pet.ControlOrder = OrderType.Follow;

            pet.MoveToWorld(from.Location, from.Map);

            pet.IsStabled = false;
            pet.StabledBy = null;

            petClaimTicket.Stabled = null;
            petClaimTicket.Delete();
        }

        public override bool HandlesOnSpeech(Mobile from)
        {
            return true;
        }

        public override void OnSpeech(SpeechEventArgs e)
        {
            if (!e.Handled && e.HasKeyword(0x0008)) // *stable*
            {
                e.Handled = true;

                BeginStable(e.Mobile);
            }
            else
            {
                base.OnSpeech(e);
            }
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (dropped is PetClaimTicket petClaimTicket)
            {
                Claim(from, petClaimTicket);
            }
            else
            {
                PublicOverheadMessage(MessageType.Regular, 0x3B2, 500607); // I'm not interested in that.
                return false;
            }

            return base.OnDragDrop(from, dropped);
        }

        [Constructible]
        public AnimalTrainer(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}