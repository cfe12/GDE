﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Interfaces;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Static Camera trigger.</summary>
    [FutureProofing("2.2")]
    [ObjectID(TriggerType.StaticCamera)]
    public class StaticCameraTrigger : Trigger, IHasDuration, IHasEasing, IHasTargetGroupID
    {
        private short targetGroupID;
        private float duration = 0.5f, easingRate;

        /// <summary>The Object ID of the Static Camera trigger.</summary>
        public override int ObjectID => (int)TriggerType.StaticCamera;

        /// <summary>The duration of the trigger's effect.</summary>
        [ObjectStringMappable(ObjectParameter.Duration)]
        public double Duration
        {
            get => duration;
            set => duration = (float)value;
        }
        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetGroupID)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The easing of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Easing)]
        public Easing Easing { get; set; }
        /// <summary>The Move Y of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.EasingRate)]
        public double EasingRate
        {
            get => easingRate;
            set => easingRate = (float)value;
        }
        /// <summary>The Exit Static property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ExitStatic)]
        public bool ExitStatic
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        /// <summary>The Target Pos coordinates property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetPosCoordinates)]
        public TargetPosCoordinates TargetPosCoordinates { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="StaticCameraTrigger"/> class.</summary>
        public StaticCameraTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="StaticCameraTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="exitStatic">The Exit Static property of the trigger.</param>
        public StaticCameraTrigger(double duration, int targetGroupID, bool exitStatic = false, TargetPosCoordinates coordinates = TargetPosCoordinates.Both)
             : base()
        {
            Duration = duration;
            TargetGroupID = targetGroupID;
            ExitStatic = exitStatic;
            TargetPosCoordinates = coordinates;
        }

        /// <summary>Returns a clone of this <seealso cref="StaticCameraTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new StaticCameraTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as StaticCameraTrigger;
            c.targetGroupID = targetGroupID;
            c.duration = duration;
            c.Easing = Easing;
            c.easingRate = easingRate;
            c.TargetPosCoordinates = TargetPosCoordinates;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as StaticCameraTrigger;
            return base.EqualsInherited(other)
                && targetGroupID == z.targetGroupID
                && duration == z.duration
                && Easing == z.Easing
                && easingRate == z.easingRate
                && TargetPosCoordinates == z.TargetPosCoordinates;
        }
    }
}
